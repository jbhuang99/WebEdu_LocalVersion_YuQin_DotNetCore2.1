using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using EEGMonitor;

const string url = "http://127.0.0.1:7788";
var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.WebHost.UseUrls(url);
builder.Services.AddControllers();
builder.Services.AddSingleton<EegService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<EegService>());
var app = builder.Build();

app.MapControllers();

await app.StartAsync();
if (!args.Contains("--no-browser", StringComparer.OrdinalIgnoreCase))
{
    try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); } catch { }
}
await app.WaitForShutdownAsync();

public sealed class EegService : BackgroundService
{
    private readonly object _gate = new();
    private readonly TgamParser _parser = new();
    private readonly List<HistoryPoint> _history = [];
    private SerialPort? _serial;
    private string? _portName;
    private string? _error;
    private long _frames;
    private long _sequence;
    private int? _signal;
    private int? _attention;
    private int? _meditation;
    private int[]? _bands;
    private DateTimeOffset? _updatedAt;
    private MeasurementSession? _session;
    private LearningEvent? _activeEvent;
    private readonly List<int[]> _baselineCapture = [];
    private bool _capturingBaseline;
    private bool _sessionExported;

    public bool Connect(string portName)
    {
        lock (_gate)
        {
            if (_session is { EndedAt: null }) { _error = "请先结束当前测量，再切换设备"; return false; }
            ClosePort();
            try
            {
                _parser.Reset();
                _history.Clear();
                _frames = 0;
                _sequence++;
                _serial = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One)
                {
                    ReadTimeout = 50,
                    ReadBufferSize = 4096
                };
                _serial.Open();
                _portName = portName;
                _error = null;
                return true;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ClosePort();
                return false;
            }
        }
    }

    public void Disconnect()
    {
        lock (_gate)
        {
            ClosePort();
            _error = null;
            _sequence++;
        }
    }

    public EegSnapshot Snapshot()
    {
        lock (_gate)
        {
            return new EegSnapshot(
                _serial?.IsOpen == true, _portName, 9600, _error, _frames, _sequence,
                _signal, _attention, _meditation, _bands is null ? null : (int[])_bands.Clone(),
                _updatedAt, _history.Select(x => new HistoryPoint(x.Sequence, (int[])x.Bands.Clone())).ToArray(),
                _session?.Id, _session?.StartedAt, _session?.EndedAt, _session?.Samples.Count ?? 0,
                _activeEvent?.Id, _activeEvent?.Title, _session?.Events.Count ?? 0,
                _session is null ? [] : SessionAnalysis.Summarize(_session),
                _session?.Baseline, _capturingBaseline, _baselineCapture.Count,
                _session is null ? [] : SessionAnalysis.RecentEvents(_session));
        }
    }

    public bool StartSession()
    {
        lock (_gate)
        {
            if (_serial?.IsOpen != true || _session is { EndedAt: null }
                || (_session?.Samples.Count > 0 && !_sessionExported))
            {
                _error = "请先下载上一轮 JSON 或 CSV，再开始新测量";
                return false;
            }
            _session = new MeasurementSession(Guid.NewGuid().ToString("N"), DateTimeOffset.Now, _portName ?? "", 9600);
            _activeEvent = null;
            _capturingBaseline = false;
            _baselineCapture.Clear();
            _sessionExported = false;
            _error = null;
            _sequence++;
            return true;
        }
    }

    public bool StopSession()
    {
        lock (_gate)
        {
            if (_session is not { EndedAt: null } || _activeEvent != null || _capturingBaseline) return false;
            _session.EndedAt = DateTimeOffset.Now;
            _sequence++;
            return true;
        }
    }

    public bool StartEvent(EventStart input)
    {
        lock (_gate)
        {
            if (_session is not { EndedAt: null } || _activeEvent != null || _capturingBaseline || string.IsNullOrWhiteSpace(input.Title)
                || input.Layers is null || input.Layers.Length == 0 || input.Layers.Any(x => x is < 1 or > 5)) return false;
            _activeEvent = new LearningEvent(Guid.NewGuid().ToString("N"), input.Title.Trim(),
                input.Layers.Distinct().Order().ToArray(), DateTimeOffset.Now);
            _session.Events.Add(_activeEvent);
            _sequence++;
            return true;
        }
    }

    public bool StartBaseline()
    {
        lock (_gate)
        {
            if (_session is not { EndedAt: null } || _activeEvent != null || _capturingBaseline) return false;
            _baselineCapture.Clear();
            _capturingBaseline = true;
            _sequence++;
            return true;
        }
    }

    public void CancelBaseline()
    {
        lock (_gate) { _capturingBaseline = false; _baselineCapture.Clear(); _sequence++; }
    }

    public bool StopEvent(EventFinish input)
    {
        lock (_gate)
        {
            if (_activeEvent == null || _session is not { EndedAt: null }
                || input.Completion is < 0 or > 100 || input.Accuracy is < 0 or > 100
                || input.Errors < 0) return false;
            _activeEvent.EndedAt = DateTimeOffset.Now;
            _activeEvent.Completion = input.Completion;
            _activeEvent.Accuracy = input.Accuracy;
            _activeEvent.Errors = input.Errors;
            _activeEvent.BehaviorPass = input.BehaviorPass;
            _activeEvent.Notes = input.Notes?.Trim() ?? "";
            _activeEvent = null;
            _sequence++;
            return true;
        }
    }

    public byte[] ExportJson()
    {
        lock (_gate)
        {
            if (_session is null) return [];
            var data = new
            {
                schemaVersion = 1,
                source = "TGAM/ThinkGear serial; device-provided band powers; 9600 baud",
                warning = "Exploratory educational research only. No raw waveform, multichannel MEQI, trained classifier, or validated learning-state diagnosis.",
                session = _session,
                layerStates = SessionAnalysis.Summarize(_session)
            };
            var bytes = JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true });
            _sessionExported = true;
            return bytes;
        }
    }

    public byte[] ExportCsv()
    {
        lock (_gate)
        {
            if (_session is null) return [];
            var sb = new StringBuilder();
            sb.AppendLine("session_id,time,frame,poor_signal,attention_raw,meditation_raw,delta,theta,low_alpha,high_alpha,low_beta,high_beta,low_gamma,mid_gamma,event_id,event_title,layers,behavior_pass,completion_percent,accuracy_percent,errors,event_notes");
            foreach (var s in _session.Samples)
            {
                var e = _session.Events.FirstOrDefault(x => x.StartedAt <= s.Time && (x.EndedAt ?? DateTimeOffset.MaxValue) >= s.Time);
                var fields = new object?[] { _session.Id, s.Time.ToString("O"), s.Frame, s.SignalQuality, s.Attention, s.Meditation,
                    s.Bands?[0], s.Bands?[1], s.Bands?[2], s.Bands?[3], s.Bands?[4], s.Bands?[5], s.Bands?[6], s.Bands?[7],
                    e?.Id, e?.Title, e is null ? null : string.Join("|", e.Layers), e?.BehaviorPass, e?.Completion, e?.Accuracy, e?.Errors, e?.Notes };
                sb.AppendLine(string.Join(",", fields.Select(Csv)));
            }
            _sessionExported = true;
            return new UTF8Encoding(true).GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
        }
    }

    private static string Csv(object? value)
    {
        var text = value?.ToString() ?? "";
        return text.Contains('"') || text.Contains(',') || text.Contains('\n') || text.Contains('\r')
            ? '"' + text.Replace("\"", "\"\"") + '"' : text;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Connect("COM5");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                lock (_gate)
                {
                    if (_serial?.IsOpen == true)
                    {
                        var count = _serial.BytesToRead;
                        if (count > 0)
                        {
                            var bytes = new byte[count];
                            _serial.Read(bytes, 0, count);
                            foreach (var sample in _parser.Feed(bytes)) Apply(sample);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lock (_gate) { _error = ex.Message; ClosePort(); _sequence++; }
            }
            await Task.Delay(50, stoppingToken);
        }
    }

    private void Apply(TgamSample sample)
    {
        _frames++;
        _sequence++;
        _updatedAt = DateTimeOffset.Now;
        if (sample.SignalQuality is not null) _signal = sample.SignalQuality;
        if (sample.Attention is not null) _attention = sample.Attention;
        if (sample.Meditation is not null) _meditation = sample.Meditation;
        if (sample.Bands is not null)
        {
            _bands = sample.Bands;
            _history.Add(new HistoryPoint(_sequence, (int[])sample.Bands.Clone()));
            if (_history.Count > 90) _history.RemoveAt(0);
        }
        if (_session is { EndedAt: null })
            _session.Samples.Add(new EegRecord(_updatedAt.Value, _frames, sample.SignalQuality,
                sample.Attention, sample.Meditation, sample.Bands is null ? null : (int[])sample.Bands.Clone()));
        if (_capturingBaseline && sample.SignalQuality is >= 0 and <= 50 && sample.Bands is not null)
        {
            _baselineCapture.Add((int[])sample.Bands.Clone());
            if (_baselineCapture.Count >= 15 && _session != null)
            {
                var means = Enumerable.Range(0, 8).Select(i => _baselineCapture.Average(x => x[i])).ToArray();
                _session.Baseline = new EegBaseline(DateTimeOffset.Now, _baselineCapture.Count, means);
                _capturingBaseline = false;
                _baselineCapture.Clear();
            }
        }
    }

    private void ClosePort()
    {
        try { if (_serial?.IsOpen == true) _serial.Close(); } catch { }
        _serial?.Dispose();
        _serial = null;
        _portName = null;
    }

    public override void Dispose() { lock (_gate) ClosePort(); base.Dispose(); }
}

public sealed record EegSnapshot(
    bool Connected, string? Port, int Baud, string? Error, long Frames, long Sequence,
    int? SignalQuality, int? Attention, int? Meditation, int[]? Bands,
    DateTimeOffset? UpdatedAt, HistoryPoint[] History, string? SessionId,
    DateTimeOffset? SessionStartedAt, DateTimeOffset? SessionEndedAt, int SessionSamples,
    string? ActiveEventId, string? ActiveEventTitle, int EventCount, LayerSummary[] LayerStates,
    EegBaseline? Baseline, bool CapturingBaseline, int BaselineProgress, EventSummary[] RecentEvents);
public sealed record HistoryPoint(long Sequence, int[] Bands);
internal sealed record TgamSample(int? SignalQuality, int? Attention, int? Meditation, int[]? Bands);

internal sealed class TgamParser
{
    private readonly List<byte> _buffer = [];
    public void Reset() => _buffer.Clear();

    public IEnumerable<TgamSample> Feed(byte[] data)
    {
        _buffer.AddRange(data);
        var samples = new List<TgamSample>();
        while (true)
        {
            var sync = FindSync();
            if (sync < 0) { if (_buffer.Count > 1) _buffer.RemoveRange(0, _buffer.Count - 1); break; }
            if (sync > 0) _buffer.RemoveRange(0, sync);
            if (_buffer.Count < 4) break;
            var length = _buffer[2];
            if (length > 169) { _buffer.RemoveAt(0); continue; }
            var packetLength = length + 4;
            if (_buffer.Count < packetLength) break;
            var sum = 0;
            for (var i = 0; i < length; i++) sum = (sum + _buffer[3 + i]) & 255;
            if (((255 - sum) & 255) == _buffer[3 + length])
                samples.Add(ParsePayload(_buffer.GetRange(3, length)));
            _buffer.RemoveRange(0, packetLength);
        }
        return samples;
    }

    private int FindSync()
    {
        for (var i = 0; i < _buffer.Count - 1; i++)
            if (_buffer[i] == 0xAA && _buffer[i + 1] == 0xAA) return i;
        return -1;
    }

    private static TgamSample ParsePayload(List<byte> payload)
    {
        int? signal = null, attention = null, meditation = null;
        int[]? bands = null;
        for (var i = 0; i < payload.Count;)
        {
            while (i < payload.Count && payload[i] == 0x55) i++;
            if (i >= payload.Count) break;
            var code = payload[i++];
            if (code < 0x80)
            {
                if (i >= payload.Count) break;
                var value = payload[i++];
                if (code == 0x02) signal = value;
                else if (code == 0x04) attention = value;
                else if (code == 0x05) meditation = value;
            }
            else
            {
                if (i >= payload.Count) break;
                var length = payload[i++];
                if (i + length > payload.Count) break;
                if (code == 0x83 && length == 24)
                {
                    bands = new int[8];
                    for (var band = 0; band < 8; band++)
                    {
                        var p = i + band * 3;
                        bands[band] = payload[p] * 65536 + payload[p + 1] * 256 + payload[p + 2];
                    }
                }
                i += length;
            }
        }
        return new TgamSample(signal, attention, meditation, bands);
    }
}
