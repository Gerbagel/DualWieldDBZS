#[path = "click.rs"]
mod click;

use click::do_click;
use eframe::egui;
use egui_taffy::{taffy, tui, TuiBuilderLogic};
use enigo::{Enigo, Settings};
use std::num::NonZeroUsize;
use std::sync::{
    atomic::{AtomicBool, AtomicU64, Ordering},
    Arc,
};
use std::thread;
use std::time::Duration;

pub struct DualWieldDBZSApp
{
    ms: u64,
    ms_interval: Arc<AtomicU64>,
    is_clicking: Arc<AtomicBool>,
}

impl DualWieldDBZSApp
{
    pub fn new(cc: &eframe::CreationContext<'_>) -> Self
    {
        let ms = 50;
        let ms_interval = Arc::new(AtomicU64::new(ms));
        let worker_ms = ms_interval.clone();

        let is_clicking = Arc::new(AtomicBool::new(false));
        let worker_flag = is_clicking.clone();

        // style start
        let mut visuals = egui::Visuals::dark();
        visuals.selection.bg_fill = egui::Color32::from_rgb(255, 0, 0);
        visuals.widgets.active.corner_radius = egui::CornerRadius::ZERO;
        visuals.widgets.hovered.corner_radius = egui::CornerRadius::ZERO;
        visuals.widgets.noninteractive.corner_radius = egui::CornerRadius::ZERO;
        visuals.widgets.inactive.corner_radius = egui::CornerRadius::ZERO;
        cc.egui_ctx.set_visuals(visuals);

        cc.egui_ctx.all_styles_mut(|style| {
            style.wrap_mode = Some(egui::TextWrapMode::Extend);
        });
        cc.egui_ctx.options_mut(|options| {
            options.max_passes = NonZeroUsize::new(2).unwrap();
        });
        // style end

        thread::spawn(move || {
            let mut enigo = Enigo::new(&Settings::default()).unwrap();

            loop
            {
                let interval = worker_ms.load(Ordering::Relaxed);
                if worker_flag.load(Ordering::Relaxed)
                {
                    do_click(&mut enigo);
                    thread::sleep(Duration::from_millis(interval));
                }
                else
                {
                    thread::sleep(Duration::from_millis(50));
                }
            }
        });

        Self {
            ms,
            ms_interval,
            is_clicking,
        }
    }
}

impl eframe::App for DualWieldDBZSApp
{
    fn ui(&mut self, ui: &mut egui::Ui, _frame: &mut eframe::Frame)
    {
        egui::CentralPanel::default().show(ui, |ui| {
            tui(ui, "main_panel")
                .reserve_available_space()
                .style(taffy::Style {
                    size: taffy::prelude::percent(1.0),
                    align_items: Some(taffy::AlignItems::Center),
                    justify_content: Some(taffy::JustifyContent::Center),
                    ..Default::default()
                })
                .show(|tui| {
                    let active = self.is_clicking.load(Ordering::Relaxed);
                    let button_text = if active
                    {
                        "Stop (Ctrl + Tab)"
                    }
                    else
                    {
                        "Start (Ctrl + Tab)"
                    };

                    tui.ui(|ui| {
                        let slider = ui.add(
                            egui::Slider::new(&mut self.ms, 1..=1000).text("Click interval (ms)"),
                        );
                        if slider.changed()
                        {
                            self.ms_interval.store(self.ms, Ordering::Relaxed);
                        }

                        if ui.button(button_text).clicked()
                        {
                            self.is_clicking.store(!active, Ordering::Relaxed);
                        }
                    }); // ui
                }); // show
        }); // CentralPanel
    }
}
