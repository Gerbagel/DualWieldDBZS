
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;
using System.Xml.Linq;

namespace DBZSDualWieldAva.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public string CancelKeysString
        {
            get
            {
                return UserSettings.Instance.CancelKeys;
            }
            set
            {
                UserSettings.Instance.CancelKeys = value;
            }
        }
        public ReactiveCommand<Window, Unit> SaveAndExitCommand { get; private set; }

        public SettingsViewModel() 
        {
            SaveAndExitCommand = ReactiveCommand.Create<Window>(this.SaveAndExit);
        }

        public void SaveAndExit(Window window)
        {
            UserSettings.Instance.SaveSettings();
            if (window != null)
                window.Close();
        }
    }
}
