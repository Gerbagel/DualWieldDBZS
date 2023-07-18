using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace DBZSDualWieldAva.Views;

public partial class SettingsView : UserControl
{
    IBrush hoverColor = new SolidColorBrush(new Color(255, 100, 100, 255), 0.45);
    public SettingsView()
    {
        InitializeComponent();
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
}