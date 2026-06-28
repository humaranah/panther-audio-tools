using PantherAudioTools.CLI;
using Serilog;
using System.Diagnostics;
using System.Text;
using TagLib;
using File = System.IO.File;

Console.OutputEncoding = Encoding.UTF8;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var fileHelper = new FileHelper();
    var tagProcessor = new FileTagProcessor();

    Console.WriteLine("Enter a file path:");
    Console.Write("> ");
    var path = Console.ReadLine();
    Log.Information(@"Scanning path: ""{Path}""...", path);
    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
    {
        Log.Error("The provided path is invalid or does not exist.");
        return;
    }

    var excludedPaths = new[]
    {
        Path.Combine(path, "Apple Music"),
        Path.Combine(path, "Qobuz")
    };

    var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
        .Where(f => !excludedPaths.Any(x => f.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
        .ToArray();
    Log.Information("Found {FileCount} files", files.Length);

    int processedCount = 0;
    int ignoredCount = 0;
    var unsupportedCount = 0;
    var targetMap = new Dictionary<string, string>();

    var stopwatch = Stopwatch.StartNew();

    foreach (var file in files)
    {
        try
        {
            using var fileInfo = TagLib.File.Create(file);

            var newFilePath = tagProcessor.GetTargetFilePath(fileInfo, path);
            if (targetMap.TryGetValue(newFilePath, out var existingSource))
            {
                Log.Warning("Conflict detected: {NewFilePath} already mapped from {ExistingSource}. Skipping {CurrentFile}.",
                    newFilePath, existingSource, file);
                continue;
            }
            targetMap[newFilePath] = file;
            if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
                processedCount++;
                Log.Information("Moved: {OldFile} -> {NewFile}", file, newFilePath);
            }
            else
            {
                ignoredCount++;
            }
        }
        catch (UnsupportedFormatException)
        {
            unsupportedCount++;
            Log.Warning("Skipping unsupported file: {File}", file);
            continue;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error processing file: {File}", file);
            continue;
        }
    }

    fileHelper.DeleteEmptyFolders(path);

    stopwatch.Stop();
    Log.Information(
        "Process Summary:\n" +
        " - Total files: {TotalCount}\n" +
        " - Processed files: {ProcessedCount}\n" +
        " - Ignored files: {IgnoredCount}" +
        " - Unsupported files: {UnsupportedCount}",
        files.Length, processedCount, ignoredCount, unsupportedCount);
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred");
}
finally
{
    await Log.CloseAndFlushAsync();
}
