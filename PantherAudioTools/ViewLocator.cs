using Avalonia.Controls;
using Avalonia.Controls.Templates;
using PantherAudioTools.ViewModels;
using System;

namespace PantherAudioTools;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        var name = param?.GetType().FullName?.Replace("ViewModel", "View");
        var type = Type.GetType(name ?? "");
        return type != null
            ? (Control?)Activator.CreateInstance(type)
            : new TextBlock { Text = "Vista no encontrada" };
    }

    public bool Match(object? data) => data is ViewModelBase;
}
