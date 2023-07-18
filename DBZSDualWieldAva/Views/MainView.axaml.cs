using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Platform;
using DBZSDualWieldAva.ViewModels;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Win32;
using Avalonia.Controls.Embedding;
using System.Windows.Forms.VisualStyles;
using System;

namespace DBZSDualWieldAva.Views;

public partial class MainView : UserControl
{
    IBrush hoverColor = new SolidColorBrush(new Color(255, 100, 100, 255), 0.45);
    public MainView()
    {
        InitializeComponent();

        // I have bastardised MVVM. I'm sorry.
        StartStopButton.Bind(Button.ContentProperty, ClickClass.Instance.ButtonSrc);
    }

    public void StartButton_Click(object sender, RoutedEventArgs e)
    {
        ClickClass.Instance.toggleTheThing();
    }

    public void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ShowSettingsDialog(desktop.MainWindow!);
        }
    }

    public void PointerEnter(object sender, PointerEventArgs e)
    {
        Button? button = sender as Button;
        button!.Background = hoverColor;
    }

    public void PointerExit(object sender, PointerEventArgs e)
    {
        Button? button = sender as Button;
        button!.Background = Brushes.Transparent;
    }

    public void ShowSettingsDialog(Window owner)
    {
        var window = new SettingsWindow();
        // Set window position to offset
        window.Position = new PixelPoint(owner.Position.X + 50, owner.Position.Y - 20);

        window.ShowDialog(owner);
    }
}
