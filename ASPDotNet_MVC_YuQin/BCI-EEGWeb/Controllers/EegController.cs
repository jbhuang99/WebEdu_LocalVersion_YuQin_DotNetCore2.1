using System.IO.Ports;
using System.Reflection;
using EEGMonitor;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public sealed class EegController(EegService eeg) : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("BCI-EEGWeb.wwwroot.index.html")
            ?? throw new InvalidOperationException("网页资源缺失");
        using var reader = new StreamReader(stream);
        return Content(reader.ReadToEnd(), "text/html; charset=utf-8");
    }

    [HttpGet("api/status")]
    public IActionResult GetStatus() => Ok(eeg.Snapshot());

    [HttpGet("api/ports")]
    public IActionResult GetPorts() => Ok(SerialPort.GetPortNames().OrderBy(PortNumber));

    [HttpPost("api/connect/{port}")]
    public IActionResult Connect(string port) => eeg.Connect(port)
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/disconnect")]
    public IActionResult Disconnect()
    {
        eeg.Disconnect();
        return Ok(eeg.Snapshot());
    }

    [HttpPost("api/session/start")]
    public IActionResult StartSession() => eeg.StartSession()
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/session/stop")]
    public IActionResult StopSession() => eeg.StopSession()
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/event/start")]
    public IActionResult StartEvent([FromBody] EventStart input) => eeg.StartEvent(input)
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/event/stop")]
    public IActionResult StopEvent([FromBody] EventFinish input) => eeg.StopEvent(input)
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/baseline/start")]
    public IActionResult StartBaseline() => eeg.StartBaseline()
        ? Ok(eeg.Snapshot())
        : BadRequest(eeg.Snapshot());

    [HttpPost("api/baseline/cancel")]
    public IActionResult CancelBaseline()
    {
        eeg.CancelBaseline();
        return Ok(eeg.Snapshot());
    }

    [HttpGet("api/export/json")]
    public IActionResult ExportJson() => File(eeg.ExportJson(), "application/json", "EEG-session.json");

    [HttpGet("api/export/csv")]
    public IActionResult ExportCsv() => File(eeg.ExportCsv(), "text/csv", "EEG-session.csv");

    private static int PortNumber(string value) =>
        int.TryParse(value.AsSpan(3), out var number) ? number : int.MaxValue;
}
