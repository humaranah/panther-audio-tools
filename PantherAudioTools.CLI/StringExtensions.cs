using System.Text;

namespace PantherAudioTools.CLI;

public static class StringExtensions
{
    public static string SanitizePathString(this string input)
    {
        foreach (var invalidChar in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(invalidChar, '_');
        }
        return input;
    }

    public static string FixEncoding(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;
        var bytes = Encoding.Default.GetBytes(input);
        var utf8 = Encoding.UTF8.GetString(bytes);

        var mojibakeCount = utf8.Count(c => c == '�' || c == 'Ç' || c == 'Ã');
        return mojibakeCount > 2 ? input : utf8;
    }
}
