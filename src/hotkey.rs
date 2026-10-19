use eframe::egui;
use global_hotkey::{
    hotkey::{Code, HotKey, Modifiers},
    GlobalHotKeyEvent, GlobalHotKeyManager, HotKeyState,
};
use std::sync::{
    atomic::{AtomicBool, AtomicU32, Ordering},
    Arc,
};

pub struct HotkeyController
{
    manager: Option<GlobalHotKeyManager>,
    current_hotkey: HotKey,
    current_id: Arc<AtomicU32>,
    display_name: String,
    is_recording: bool,
}

impl HotkeyController
{
    pub fn new(is_clicking: Arc<AtomicBool>, egui_ctx: egui::Context) -> Self
    {
        // Default hotkey: Ctrl + Tab
        let default_hotkey = HotKey::new(Some(Modifiers::CONTROL), Code::Tab);
        let current_id = Arc::new(AtomicU32::new(default_hotkey.id()));
        let display_name = format_hotkey(&default_hotkey);

        let manager = GlobalHotKeyManager::new().ok();
        if let Some(ref m) = manager
        {
            let _ = m.register(default_hotkey);
        }

        let is_clicking_handler = is_clicking;
        let id_handler = current_id.clone();
        GlobalHotKeyEvent::set_event_handler(Some(move |event: GlobalHotKeyEvent| {
            if event.state == HotKeyState::Pressed
                && event.id == id_handler.load(Ordering::Relaxed)
            {
                let active = is_clicking_handler.load(Ordering::Relaxed);
                is_clicking_handler.store(!active, Ordering::Relaxed);
                egui_ctx.request_repaint();
            }
        }));

        Self
        {
            manager,
            current_hotkey: default_hotkey,
            current_id,
            display_name,
            is_recording: false,
        }
    }

    pub fn display_name(&self) -> &str
    {
        &self.display_name
    }

    pub fn is_recording(&self) -> bool
    {
        self.is_recording
    }

    pub fn toggle_recording(&mut self)
    {
        self.is_recording = !self.is_recording;
    }

    pub fn process_input(&mut self, input: &egui::InputState)
    {
        if !self.is_recording
        {
            return;
        }

        if input.key_pressed(egui::Key::Escape)
        {
            self.is_recording = false;
            return;
        }

        for event in &input.events
        {
            if let egui::Event::Key {
                key,
                pressed: true,
                modifiers,
                ..
            } = event
            {
                if let Some(code) = egui_key_to_code(*key)
                {
                    let mut mods = Modifiers::empty();
                    if modifiers.ctrl
                    {
                        mods |= Modifiers::CONTROL;
                    }
                    if modifiers.alt
                    {
                        mods |= Modifiers::ALT;
                    }
                    if modifiers.shift
                    {
                        mods |= Modifiers::SHIFT;
                    }
                    if modifiers.mac_cmd
                    {
                        mods |= Modifiers::META;
                    }

                    let new_hotkey = HotKey::new(
                        if mods.is_empty() { None } else { Some(mods) },
                        code,
                    );

                    if let Some(ref manager) = self.manager
                    {
                        let _ = manager.unregister(self.current_hotkey);
                        if manager.register(new_hotkey).is_ok()
                        {
                            self.current_hotkey = new_hotkey;
                            self.current_id.store(new_hotkey.id(), Ordering::Relaxed);
                            self.display_name = format_hotkey(&new_hotkey);
                        }
                    }
                    self.is_recording = false;
                    break;
                }
            }
        }
    }
}

fn format_hotkey(hotkey: &HotKey) -> String
{
    let mut parts = Vec::new();
    if hotkey.mods.contains(Modifiers::CONTROL)
    {
        parts.push("Ctrl");
    }
    if hotkey.mods.contains(Modifiers::ALT)
    {
        parts.push("Alt");
    }
    if hotkey.mods.contains(Modifiers::SHIFT)
    {
        parts.push("Shift");
    }
    if hotkey.mods.contains(Modifiers::META)
    {
        parts.push("Win");
    }

    let key_name = match hotkey.key
    {
        Code::Tab => "Tab",
        Code::Space => "Space",
        Code::Enter => "Enter",
        Code::Backspace => "Backspace",
        Code::Delete => "Delete",
        Code::Insert => "Insert",
        Code::Home => "Home",
        Code::End => "End",
        Code::PageUp => "PageUp",
        Code::PageDown => "PageDown",
        Code::ArrowDown => "Down",
        Code::ArrowLeft => "Left",
        Code::ArrowRight => "Right",
        Code::ArrowUp => "Up",
        Code::F1 => "F1",
        Code::F2 => "F2",
        Code::F3 => "F3",
        Code::F4 => "F4",
        Code::F5 => "F5",
        Code::F6 => "F6",
        Code::F7 => "F7",
        Code::F8 => "F8",
        Code::F9 => "F9",
        Code::F10 => "F10",
        Code::F11 => "F11",
        Code::F12 => "F12",
        Code::KeyA => "A",
        Code::KeyB => "B",
        Code::KeyC => "C",
        Code::KeyD => "D",
        Code::KeyE => "E",
        Code::KeyF => "F",
        Code::KeyG => "G",
        Code::KeyH => "H",
        Code::KeyI => "I",
        Code::KeyJ => "J",
        Code::KeyK => "K",
        Code::KeyL => "L",
        Code::KeyM => "M",
        Code::KeyN => "N",
        Code::KeyO => "O",
        Code::KeyP => "P",
        Code::KeyQ => "Q",
        Code::KeyR => "R",
        Code::KeyS => "S",
        Code::KeyT => "T",
        Code::KeyU => "U",
        Code::KeyV => "V",
        Code::KeyW => "W",
        Code::KeyX => "X",
        Code::KeyY => "Y",
        Code::KeyZ => "Z",
        Code::Digit0 => "0",
        Code::Digit1 => "1",
        Code::Digit2 => "2",
        Code::Digit3 => "3",
        Code::Digit4 => "4",
        Code::Digit5 => "5",
        Code::Digit6 => "6",
        Code::Digit7 => "7",
        Code::Digit8 => "8",
        Code::Digit9 => "9",
        _ => "Key",
    };
    parts.push(key_name);
    parts.join(" + ")
}

fn egui_key_to_code(key: egui::Key) -> Option<Code>
{
    match key
    {
        egui::Key::Tab => Some(Code::Tab),
        egui::Key::Space => Some(Code::Space),
        egui::Key::Enter => Some(Code::Enter),
        egui::Key::Backspace => Some(Code::Backspace),
        egui::Key::Delete => Some(Code::Delete),
        egui::Key::Insert => Some(Code::Insert),
        egui::Key::Home => Some(Code::Home),
        egui::Key::End => Some(Code::End),
        egui::Key::PageUp => Some(Code::PageUp),
        egui::Key::PageDown => Some(Code::PageDown),
        egui::Key::ArrowDown => Some(Code::ArrowDown),
        egui::Key::ArrowLeft => Some(Code::ArrowLeft),
        egui::Key::ArrowRight => Some(Code::ArrowRight),
        egui::Key::ArrowUp => Some(Code::ArrowUp),
        egui::Key::F1 => Some(Code::F1),
        egui::Key::F2 => Some(Code::F2),
        egui::Key::F3 => Some(Code::F3),
        egui::Key::F4 => Some(Code::F4),
        egui::Key::F5 => Some(Code::F5),
        egui::Key::F6 => Some(Code::F6),
        egui::Key::F7 => Some(Code::F7),
        egui::Key::F8 => Some(Code::F8),
        egui::Key::F9 => Some(Code::F9),
        egui::Key::F10 => Some(Code::F10),
        egui::Key::F11 => Some(Code::F11),
        egui::Key::F12 => Some(Code::F12),
        egui::Key::A => Some(Code::KeyA),
        egui::Key::B => Some(Code::KeyB),
        egui::Key::C => Some(Code::KeyC),
        egui::Key::D => Some(Code::KeyD),
        egui::Key::E => Some(Code::KeyE),
        egui::Key::F => Some(Code::KeyF),
        egui::Key::G => Some(Code::KeyG),
        egui::Key::H => Some(Code::KeyH),
        egui::Key::I => Some(Code::KeyI),
        egui::Key::J => Some(Code::KeyJ),
        egui::Key::K => Some(Code::KeyK),
        egui::Key::L => Some(Code::KeyL),
        egui::Key::M => Some(Code::KeyM),
        egui::Key::N => Some(Code::KeyN),
        egui::Key::O => Some(Code::KeyO),
        egui::Key::P => Some(Code::KeyP),
        egui::Key::Q => Some(Code::KeyQ),
        egui::Key::R => Some(Code::KeyR),
        egui::Key::S => Some(Code::KeyS),
        egui::Key::T => Some(Code::KeyT),
        egui::Key::U => Some(Code::KeyU),
        egui::Key::V => Some(Code::KeyV),
        egui::Key::W => Some(Code::KeyW),
        egui::Key::X => Some(Code::KeyX),
        egui::Key::Y => Some(Code::KeyY),
        egui::Key::Z => Some(Code::KeyZ),
        egui::Key::Num0 => Some(Code::Digit0),
        egui::Key::Num1 => Some(Code::Digit1),
        egui::Key::Num2 => Some(Code::Digit2),
        egui::Key::Num3 => Some(Code::Digit3),
        egui::Key::Num4 => Some(Code::Digit4),
        egui::Key::Num5 => Some(Code::Digit5),
        egui::Key::Num6 => Some(Code::Digit6),
        egui::Key::Num7 => Some(Code::Digit7),
        egui::Key::Num8 => Some(Code::Digit8),
        egui::Key::Num9 => Some(Code::Digit9),
        _ => None,
    }
}
