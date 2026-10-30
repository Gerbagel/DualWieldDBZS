#![windows_subsystem = "windows"]

mod window;

use eframe::egui;
use window::*;

fn main() -> eframe::Result<()>
{
    let options = eframe::NativeOptions {
        viewport: egui::ViewportBuilder::default().with_inner_size([260.0, 140.0]).with_resizable(false),
        ..Default::default()
    };

    eframe::run_native(
        "DBZSDualWield",
        options,
        Box::new(|cc| Ok(Box::new(DualWieldDBZSApp::new(cc)))),
    )
}
