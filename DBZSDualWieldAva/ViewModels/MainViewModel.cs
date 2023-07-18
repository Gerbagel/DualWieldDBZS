using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Microsoft.VisualBasic;
using ReactiveUI;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBZSDualWieldAva.ViewModels;

public class MainViewModel : ViewModelBase
{
    public int IntervalInMs
    {
        get
        {
            return UserSettings.Instance.Interval;
        }
        set
        {
            UserSettings.Instance.Interval = value;
        }
    }

    public MainViewModel()
    {
        UserSettings.Instance.LoadSettings();
    }
}
