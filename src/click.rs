use enigo::{Button, Direction, Enigo, Mouse};

pub fn do_click(enigo: &mut Enigo)
{
    enigo.button(Button::Left, Direction::Click).unwrap();
}
