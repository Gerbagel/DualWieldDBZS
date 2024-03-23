using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace DBZSDualWieldAva
{
    public class SettingsStruct
    {
        public string CancelKeys { get; set; } = "";
        public int ClickInterval { get; set; } = 0;

        public bool IsMuted { get; set; } = false;
    }
    public class UserSettings
    {
        private static string ConfigFilePath = "UserConfig.json";
        private static UserSettings instance = null;
        private static readonly object padlock = new object();
        private SettingsStruct settings = new SettingsStruct();

        private bool IsLoaded = false;

        ~UserSettings() 
        {
            SaveSettings();
        }

        public static UserSettings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserSettings();
                    }
                    return instance;
                }
            }
        }

        public int Interval
        {
            get 
            {
                if (!IsLoaded)
                {
                    LoadSettings();
                }
                return settings.ClickInterval;
            }
            set { settings.ClickInterval = value; }
        }

        public string CancelKeys
        {
            get
            {
                if (!IsLoaded)
                {
                    LoadSettings();
                }
                return settings.CancelKeys;
            }
            set { settings.CancelKeys = value; }
        }

        public bool IsMuted
        {
            get
            {
                if (!IsLoaded)
                {
                    LoadSettings();
                }
                return settings.IsMuted;
            }
            set{ settings.IsMuted = value; }
        }

        public void SaveSettings()
        {
            if (File.Exists(ConfigFilePath))
            {
                FileStream fs = File.OpenWrite(ConfigFilePath);
                JsonSerializer.Serialize(fs, settings);
                fs.Dispose();
            } else
            {
                FileStream fs = File.Create(ConfigFilePath);
                JsonSerializer.Serialize(fs, settings);
                fs.Dispose();
            }
        }

        public void LoadSettings()
        { 
            if (File.Exists(ConfigFilePath))
            {
                FileStream file = File.OpenRead(ConfigFilePath);
                try
                {
                    SettingsStruct? sStruct = JsonSerializer.Deserialize<SettingsStruct>(file);
                    file.Dispose();

                    Interval = sStruct!.ClickInterval;
                    CancelKeys = sStruct!.CancelKeys;
                    IsMuted = sStruct!.IsMuted;
                    IsLoaded = true;
                }
                catch (Exception ex)
                {
                    file.Dispose();
                    Debug.WriteLine(ex.Message + ": " + ex.StackTrace);
                    File.Delete(ConfigFilePath);
                    LoadSettings();
                }
            } else
            {
                Interval = 170;
                CancelKeys = "E, Escape, V, L, K, X";
                IsMuted = false;
                SaveSettings();
            }
        }
    }
}
