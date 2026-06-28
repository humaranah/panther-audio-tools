using Serilog;

namespace PantherAudioTools.CLI;

public class FileHelper
{
    public void DeleteEmptyFolders(string rootPath)
    {
        foreach (var dirPath in Directory.GetDirectories(rootPath))
        {
            try
            {
                DeleteEmptyFolders(dirPath);
                if (Directory.Exists(dirPath) &&
                    Directory.GetFileSystemEntries(dirPath).Length == 0)
                {
                    Log.Information(@"Removing empty directory: ""{Path}""", dirPath);
                    Directory.Delete(dirPath);
                }
            }
            catch (Exception ex)
            {
                Log.Warning(@"Cannot delete folder: ""{Path}""; Reason: {Reason}",
                    dirPath, ex.Message);
            }
        }
    }
}
