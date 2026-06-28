using PantherAudioTools.Core;

namespace PantherAudioTools.MusicSorter;

public class MusicSorterModule : IModule
{
    public string Name => "MusicSorter";

    public string Title => "Music Sorter";

    public string Description => "Sort and organize music files based on their metadata.";

    public void Initialize()
    {
        throw new NotImplementedException();
    }
}
