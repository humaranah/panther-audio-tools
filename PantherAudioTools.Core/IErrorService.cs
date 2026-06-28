using PantherAudioTools.Core.Models;

namespace PantherAudioTools.Core;

public interface IErrorService
{
    event EventHandler<ErrorInfo> OnError;
    void ReportError(ErrorInfo errorInfo);
}
