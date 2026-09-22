namespace EEGMonitor;

public sealed record EventStart(string Title, int[] Layers);
public sealed record EventFinish(double? Completion, double? Accuracy, int Errors, bool? BehaviorPass, string? Notes);
internal sealed record EegRecord(DateTimeOffset Time, long Frame, int? SignalQuality,
    int? Attention, int? Meditation, int[]? Bands);
public sealed record EegBaseline(DateTimeOffset CapturedAt, int ValidSamples, double[] MeanBands);

internal sealed class MeasurementSession(string id, DateTimeOffset startedAt, string port, int baud)
{
    public string Id { get; } = id;
    public DateTimeOffset StartedAt { get; } = startedAt;
    public DateTimeOffset? EndedAt { get; set; }
    public string Port { get; } = port;
    public int Baud { get; } = baud;
    public List<EegRecord> Samples { get; } = [];
    public List<LearningEvent> Events { get; } = [];
    public EegBaseline? Baseline { get; set; }
}

internal sealed class LearningEvent(string id, string title, int[] layers, DateTimeOffset startedAt)
{
    public string Id { get; } = id;
    public string Title { get; } = title;
    public int[] Layers { get; } = layers;
    public DateTimeOffset StartedAt { get; } = startedAt;
    public DateTimeOffset? EndedAt { get; set; }
    public double? Completion { get; set; }
    public double? Accuracy { get; set; }
    public int Errors { get; set; }
    public bool? BehaviorPass { get; set; }
    public string Notes { get; set; } = "";
}

public sealed record LayerSummary(int Layer, string Name, string State, int ValidEvents,
    int PassingInWindow, string Reason);
public sealed record EventSummary(string Id, string Title, int[] Layers, int TotalFrames, int GoodFrames,
    double[]? MeanBands, double[]? RelativeBands, double[]? BaselineChangePercent,
    bool? BehaviorPass, DateTimeOffset? EndedAt);

internal static class SessionAnalysis
{
    private static readonly string[] Names = ["实践层", "技术层", "科学层", "人文层", "哲学层"];
    public static LayerSummary[] Summarize(MeasurementSession session)
    {
        var result = new LayerSummary[5];
        for (var layer = 1; layer <= 5; layer++)
        {
            var relevant = session.Events.Where(e => e.EndedAt != null && e.Layers.Contains(layer))
                .Select(e => new { Event = e, Samples = session.Samples.Where(s => s.Time >= e.StartedAt && s.Time <= e.EndedAt).ToArray() })
                .Where(x => x.Samples.Count(s => s.SignalQuality is >= 0 and <= 50 && s.Bands != null) >= 3)
                .TakeLast(5).ToArray();
            var passed = relevant.Count(x => x.Event.BehaviorPass == true);
            var state = relevant.Length < 5 || relevant.Any(x => x.Event.BehaviorPass == null)
                ? "未知" : passed >= 4 ? "稳定" : passed >= 2 ? "观察" : "不稳定";
            var reason = relevant.Length < 5 ? "需要至少 5 个有效且有行为评价的事件"
                : relevant.Any(x => x.Event.BehaviorPass == null) ? "存在未评价的行为记录"
                : "基于人工行为达标记录；不是脑电推断的认知能力";
            result[layer - 1] = new LayerSummary(layer, Names[layer - 1], state, relevant.Length, passed, reason);
        }
        return result;
    }

    public static EventSummary[] RecentEvents(MeasurementSession session) => session.Events
        .TakeLast(8).Reverse().Select(e =>
        {
            var frames = session.Samples.Where(s => s.Time >= e.StartedAt && s.Time <= (e.EndedAt ?? DateTimeOffset.MaxValue)).ToArray();
            var good = frames.Where(s => s.SignalQuality is >= 0 and <= 50 && s.Bands != null).ToArray();
            double[]? means = good.Length == 0 ? null : Enumerable.Range(0, 8).Select(i => good.Average(x => x.Bands![i])).ToArray();
            var total = means?.Sum() ?? 0;
            double[]? relative = means == null || total == 0 ? null : means.Select(x => x / total * 100).ToArray();
            double[]? change = means == null || session.Baseline == null ? null
                : means.Select((x, i) => session.Baseline.MeanBands[i] == 0 ? 0
                    : (x / session.Baseline.MeanBands[i] - 1) * 100).ToArray();
            return new EventSummary(e.Id, e.Title, e.Layers, frames.Length, good.Length,
                means, relative, change, e.BehaviorPass, e.EndedAt);
        }).ToArray();
}
