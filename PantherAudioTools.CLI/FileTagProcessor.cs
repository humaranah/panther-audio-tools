using System.Text;

namespace PantherAudioTools.CLI;

public class FileTagProcessor
{
    public string GetTargetFilePath(TagLib.File file, string basePath)
    {
        var albumArtist = GetArtist(file).FixEncoding().SanitizePathString();
        var album = GetAlbum(file).FixEncoding().SanitizePathString();
        var title = GetTitle(file).FixEncoding().SanitizePathString();
        var disc = file.Tag.Disc;
        var track = file.Tag.Track;
        var extension = Path.GetExtension(file.Name);

        var fileSb = new StringBuilder();
        if (disc > 0) fileSb.Append($"D{disc:D2} - ");
        if (track > 0) fileSb.Append($"{track:D2} - ");
        fileSb.Append(title);
        fileSb.Append(extension);

        var newFileName = fileSb.ToString();
        var newDirectory = Path.Combine(basePath, albumArtist, album);

        Directory.CreateDirectory(newDirectory);
        return Path.Combine(newDirectory, newFileName);
    }

    private string GetArtist(TagLib.File file)
    {
        if (file.Tag.AlbumArtists is not null && file.Tag.AlbumArtists.Length > 0)
            return string.Join(", ", file.Tag.AlbumArtists);
        if (file.Tag.Performers is not null && file.Tag.Performers.Length > 0)
            return string.Join (", ", file.Tag.Performers);
        return "Unknown Artist";
    }

    private string GetAlbum(TagLib.File file)
    {
        if (!string.IsNullOrWhiteSpace(file.Tag.Album))
            return file.Tag.Album;
        return "Unknown Album";
    }

    private string GetTitle(TagLib.File file)
    {
        if (!string.IsNullOrWhiteSpace(file.Tag.Title))
            return file.Tag.Title;
        return Path.GetFileNameWithoutExtension(file.Name);
    }
}
