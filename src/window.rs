#[path = "click.rs"]
mod click;

use click::do_click;
use eframe::egui;
use egui_taffy::{taffy, tui, TuiBuilderLogic};
use enigo::{Enigo, Settings};
use std::num::NonZeroUsize;
use std::sync::{
    atomic::{AtomicBool, Ordering},
    Arc,
};
use std::thread;
use std::time::Duration;

pub struct AutoClickerApp {
    is_clicking: Arc<AtomicBool>,
}

impl AutoClickerApp {
    pub fn new(cc: &eframe::CreationContext<'_>) -> Self {
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

        // Required by egui_taffy: prevent text from collapsing into single-character vertical columns
        cc.egui_ctx.all_styles_mut(|style| {
            style.wrap_mode = Some(egui::TextWrapMode::Extend);
        });
        // Enable egui multi-pass so taffy can accurately calculate intrinsic sizes
        cc.egui_ctx.options_mut(|options| {
            options.max_passes = NonZeroUsize::new(2).unwrap();
        });
        // style end

        thread::spawn(move || {
            let mut enigo = Enigo::new(&Settings::default()).unwrap();

            loop {
                if worker_flag.load(Ordering::Relaxed) {
                    do_click(&mut enigo);
                    thread::sleep(Duration::from_millis(50));
                } else {
                    thread::sleep(Duration::from_millis(50));
                }
            }
        });

        Self { is_clicking }
    }
}

impl eframe::App for AutoClickerApp {
    fn ui(&mut self, ui: &mut egui::Ui, _frame: &mut eframe::Frame) {
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
                    let button_text = if active {
                        "Stop (Ctrl + Tab)"
                    } else {
                        "Start (Ctrl + Tab)"
                    };

                    tui.ui(|ui| {
                        if ui.button(button_text).clicked() {
                            self.is_clicking.store(!active, Ordering::Relaxed);
                        }
                    });
                });
        });
    }
}
