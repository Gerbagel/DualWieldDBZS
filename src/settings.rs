use super::hotkey::HotkeyController;
use eframe::egui;

pub struct SettingsWindow
{
    pub is_open: bool,
}

impl SettingsWindow
{
    pub fn new() -> Self
    {
        Self { is_open: false }
    }

    pub fn toggle(&mut self)
    {
        self.is_open = !self.is_open;
    }

    pub fn show(&mut self, ctx: &egui::Context, hotkey: &mut HotkeyController)
    {
        if !self.is_open
        {
            return;
        }

        egui::Window::new("Settings")
            .open(&mut self.is_open)
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
    }
}
