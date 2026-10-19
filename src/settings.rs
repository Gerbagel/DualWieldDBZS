use super::hotkey::HotkeyController;
use eframe::egui;
use serde::{Deserialize, Serialize};
use std::path::PathBuf;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct AppSettings
{
    pub interval_ms: u64,
    pub hotkey: String,
}

impl Default for AppSettings
{
    fn default() -> Self
    {
        Self {
            interval_ms: 50,
            hotkey: "Control+Tab".to_string(),
        }
    }
}

pub fn get_settings_path() -> PathBuf
{
    #[cfg(target_os = "windows")]
    let base_dir = std::env::var_os("APPDATA").map(PathBuf::from).or_else(|| {
        std::env::var_os("USERPROFILE").map(|p| PathBuf::from(p).join("AppData").join("Roaming"))
    });

    #[cfg(not(target_os = "windows"))]
    let base_dir = std::env::var_os("XDG_CONFIG_HOME")
        .map(PathBuf::from)
        .or_else(|| std::env::var_os("HOME").map(|p| PathBuf::from(p).join(".config")));

    if let Some(dir) = base_dir
    {
        let app_dir = dir.join("DualWieldDBZS");
        let _ = std::fs::create_dir_all(&app_dir);
        app_dir.join("settings.json")
    }
    else
    {
        PathBuf::from("settings.json")
    }
}

impl AppSettings
{
    pub fn load() -> Self
    {
        let path = get_settings_path();
        if let Ok(data) = std::fs::read_to_string(&path)
        {
            if let Ok(settings) = serde_json::from_str(&data)
            {
                return settings;
            }
        }
        Self::default()
    }

    pub fn save(&self)
    {
        let path = get_settings_path();
        if let Ok(json) = serde_json::to_string_pretty(self)
        {
            let _ = std::fs::write(path, json);
        }
    }
}

pub struct SettingsWindow
{
    pub is_open: bool,
    pub settings: AppSettings,
}

impl SettingsWindow
{
    pub fn new(settings: AppSettings) -> Self
    {
        Self {
            is_open: false,
            settings,
        }
    }

    pub fn toggle(&mut self)
    {
        self.is_open = !self.is_open;
    }

    pub fn save(&self)
    {
        self.settings.save();
    }

    pub fn update_interval(&mut self, interval_ms: u64)
    {
        if self.settings.interval_ms != interval_ms
        {
            self.settings.interval_ms = interval_ms;
            self.save();
        }
    }

    pub fn update_hotkey(&mut self, hotkey: String)
    {
        if self.settings.hotkey != hotkey
        {
            self.settings.hotkey = hotkey;
            self.save();
        }
    }

    pub fn show(&mut self, ctx: &egui::Context, hotkey: &mut HotkeyController)
    {
        if !self.is_open
        {
            return;
        }

        let mut is_open = self.is_open;

        egui::Window::new("Settings")
            .open(&mut is_open)
            .resizable(false)
            .collapsible(false)
            .default_size([240.0, 100.0])
            .show(ctx, |ui| {
                ui.vertical(|ui| {
                    ui.add_space(8.0);

                    ui.horizontal(|ui| {
                        ui.label("Toggle Hotkey:");

                        let hotkey_btn_text = if hotkey.is_recording()
                        {
                            "Press keys (Esc to cancel)...".to_string()
                        }
                        else
                        {
                            hotkey.display_name().to_string()
                        };

                        if ui.button(hotkey_btn_text).clicked()
                        {
                            hotkey.toggle_recording();
                        }
                    });

                    ui.add_space(8.0);
                }); // ui
            }); // show

        self.is_open = is_open;
    }
}
