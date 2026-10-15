mod window;

use eframe::egui;
use window::*;

fn main() -> eframe::Result<()> {
    let options = eframe::NativeOptions {
        viewport: egui::ViewportBuilder::default().with_inner_size([250.0, 150.0]),
        ..Default::default()
    };

    eframe::run_native(
        "DBZSDualWield",
        options,
        Box::new(|cc| Ok(Box::new(AutoClickerApp::new(cc)))),
    )
}
