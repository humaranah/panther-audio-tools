using Avalonia.Controls;
using Avalonia.Media;
using System;

namespace PantherAudioTools.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        if (OperatingSystem.IsWindows())
        {
            TransparencyLevelHint = [ WindowTransparencyLevel.Mica ];
            Background = Brushes.Transparent;
        }
        InitializeComponent();
    }
}
