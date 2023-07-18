
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Media;

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
        public bool IsMuted
        {
            get
            {
                return UserSettings.Instance.IsMuted;
            }
            set
            {
                UserSettings.Instance.IsMuted = value;
            }
        }
        public ReactiveCommand<Window, Unit> SaveAndExitCommand { get; private set; }

        public SettingsViewModel() 
        {
            SaveAndExitCommand = ReactiveCommand.Create<Window>(this.SaveAndExit);
        }

        public void SaveAndExit(Window window)
        {
            playClickSound();
            UserSettings.Instance.SaveSettings();
            if (window != null)
                window.Close();
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
}
