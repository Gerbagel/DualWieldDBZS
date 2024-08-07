#include "hotkeyeventfilter.h"
#include "clickclass.h"

bool HotkeyEventFilter::nativeEventFilter(const QByteArray &eventType, void *message, qintptr* result)
{
    if (eventType == "windows_generic_MSG")
    {
        MSG *msg = static_cast<MSG *>(message);
        if (msg->message == WM_HOTKEY)
        {
            if (msg->wParam == TOGGLE_HOTKEY_ID)
            {
                if (sendToggle)
                {
                    // it calls the hotkey twice for some reason, so have to check if it's the first call or not
                    if (!wasToggled)
                    {
                        sendToggle();
                        wasToggled = true;
                    }
                    else
                    {
                        wasToggled = false;
                    }
                }
            }
            else if (msg->wParam == BEAN_HOTKEY_ID)
            {
                if (sendBean)
                {
                    // no first call check because it doesn't matter; bean has cooldown
                    sendBean();
                }
            }
        }
    }
    return false;
}

void HotkeyEventFilter::defineFunctions(std::function<void()> onToggle, std::function<void()> onBean)
{
    sendToggle = onToggle;
    sendBean = onBean;
}
