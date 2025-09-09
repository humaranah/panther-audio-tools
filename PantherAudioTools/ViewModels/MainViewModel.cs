using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PantherAudioTools.ViewModels.Sections;

namespace PantherAudioTools.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView = new HomeViewModel();

    [RelayCommand]
    private void NavigateTo(string? target)
    {
        switch (target)
        {
            case "fileManager":
                CurrentView = new FileManagerViewModel();
                break;
            default:
                CurrentView = new HomeViewModel();
                break;
        }
    }
}
