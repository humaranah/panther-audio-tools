using Avalonia.Controls;

namespace PantherAudioTools.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        TransparencyLevelHint =
        [
            WindowTransparencyLevel.Mica,
            WindowTransparencyLevel.AcrylicBlur
        ];
        InitializeComponent();
    }
}
