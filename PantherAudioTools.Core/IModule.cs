namespace PantherAudioTools.Core;

public interface IModule
{
    string Name { get; }
    string Title { get; }
    string Description { get; }

    void Initialize();
}
