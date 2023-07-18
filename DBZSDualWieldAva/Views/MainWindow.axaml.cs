using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using System.Diagnostics;

namespace DBZSDualWieldAva.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Closing += (object? sender, WindowClosingEventArgs args) => UserSettings.Instance.SaveSettings();
    }
}
