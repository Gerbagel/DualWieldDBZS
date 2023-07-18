using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Windows.Media;

namespace DBZSDualWieldAva.Views;

public partial class MainView : UserControl
{
    IBrush hoverColor = new Avalonia.Media.SolidColorBrush(new Avalonia.Media.Color(255, 100, 100, 255), 0.45);
    public MainView()
    {
        InitializeComponent();

        // I have bastardised MVVM. I'm sorry.
        StartStopButton.Bind(Button.ContentProperty, ClickClass.Instance.ButtonSrc);
    }

    public void StartButton_Click(object sender, RoutedEventArgs e)
    {
        playClickSound();
        ClickClass.Instance.toggleTheThing();
    }

    public void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        playClickSound();
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
        button!.Background = Avalonia.Media.Brushes.Transparent;
    }

    public void ShowSettingsDialog(Window owner)
    {
        var window = new SettingsWindow();
        // Set window position to offset
        window.Position = new PixelPoint(owner.Position.X + 50, owner.Position.Y - 20);

        window.ShowDialog(owner);
    }

    void playClickSound()
    {
        if (UserSettings.Instance.IsMuted)
            return;
        var player = new MediaPlayer();
        player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Assets\click.wav"));
        player.Volume = 0.3f;
        player.Play();
    }
}
