namespace PantherAudioTools.Core.Models;

public class ErrorInfo
{
    public string Message { get; set; } = string.Empty;
    public Exception? Exception { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public bool ShouldNotifyUser { get; set; }
    public bool IsCritical { get; set; }
}