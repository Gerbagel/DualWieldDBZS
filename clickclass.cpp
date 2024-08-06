#include "clickclass.h"

#include <qDebug>

enum mouseeventflags
{
    LeftDown = 0x02,
    LeftUp = 0x04,
    RightDown = 0x08,
    RightUp = 0x10,
};

void leftClick(int x, int y)
{
    mouse_event((int)(mouseeventflags::LeftDown | mouseeventflags::LeftUp), x, y, 0, 0);
}

void rightClick(int x, int y)
{
    mouse_event((int)(mouseeventflags::RightDown | mouseeventflags::RightUp), x, y, 0, 0);
}

// global variables hurt... but it must be done
HHOOK hhook = NULL;
QHash<Qt::Key, bool> keyMap;
QHash<int, Qt::Key> vkToQtKeyMap;

LRESULT CALLBACK LowLevelKeyboardProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode >= 0 && wParam == WM_KEYDOWN)
    {
        KBDLLHOOKSTRUCT* kbdStruct = reinterpret_cast<KBDLLHOOKSTRUCT*>(lParam);
        Qt::Key k = vkToQtKeyMap[kbdStruct->vkCode];

        if (keyMap.contains(k))
        {
            keyMap[k] = true;
        }
        else
        {
            foreach (Qt::Key key, keyMap.keys())
            {
                keyMap[key] = false;
            }
        }
    }

    return CallNextHookEx(hhook, nCode, (int)wParam, lParam);
}

ClickClass::ClickClass(u_int* handle) : windowHandle((HWND) handle)
{
    hhook = NULL;
    keyMap = QHash<Qt::Key, bool>();

    // Add mappings for the alphanumeric keys
    populateMap();

    bool toggleRegistration = RegisterHotKey(windowHandle, TOGGLE_HOTKEY_ID, 0x0002, VK_TAB);

    if (toggleRegistration)
    {
        qDebug() << "Toggle shortcut registration successful!\n";
    }

    bool beanRegistration = RegisterHotKey(windowHandle, BEAN_HOTKEY_ID, 0x0000, VK_F8);

    if (beanRegistration)
    {
        qDebug() << "Bean shortcut registration successful!\n";
    }

    // initiate key map // todo: add JSON settings
    keyMap.insert(Qt::Key_E, false);

    foreach (Qt::Key key, keyMap.keys())
    {
        qDebug() << key << " | " << keyMap[key] << "\n";
    }

    // hook keyboard
    hhook = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardProc, GetModuleHandle(nullptr), 0);

    if (hhook)
    {
        qDebug() << "Successfully hooked keyboard!\n";
    }
    else
    {
        qDebug() << "Failed to hook keyboard\n";
    }
}

ClickClass::~ClickClass()
{
    UnregisterHotKey(windowHandle, TOGGLE_HOTKEY_ID);
    UnregisterHotKey(windowHandle, BEAN_HOTKEY_ID);
    if (hhook)
    {
        UnhookWindowsHookEx(hhook);
    }
}

void ClickClass::populateMap()
{
    // I hate this.
    vkToQtKeyMap[VK_BACK] = Qt::Key_Backspace;
    vkToQtKeyMap[VK_TAB] = Qt::Key_Tab;
    vkToQtKeyMap[VK_RETURN] = Qt::Key_Return;
    vkToQtKeyMap[VK_SHIFT] = Qt::Key_Shift;
    vkToQtKeyMap[VK_CONTROL] = Qt::Key_Control;
    vkToQtKeyMap[VK_MENU] = Qt::Key_Alt;
    vkToQtKeyMap[VK_PAUSE] = Qt::Key_Pause;
    vkToQtKeyMap[VK_CAPITAL] = Qt::Key_CapsLock;
    vkToQtKeyMap[VK_ESCAPE] = Qt::Key_Escape;
    vkToQtKeyMap[VK_SPACE] = Qt::Key_Space;
    vkToQtKeyMap[VK_PRIOR] = Qt::Key_PageUp;
    vkToQtKeyMap[VK_NEXT] = Qt::Key_PageDown;
    vkToQtKeyMap[VK_END] = Qt::Key_End;
    vkToQtKeyMap[VK_HOME] = Qt::Key_Home;
    vkToQtKeyMap[VK_LEFT] = Qt::Key_Left;
    vkToQtKeyMap[VK_UP] = Qt::Key_Up;
    vkToQtKeyMap[VK_RIGHT] = Qt::Key_Right;
    vkToQtKeyMap[VK_DOWN] = Qt::Key_Down;
    vkToQtKeyMap[VK_SNAPSHOT] = Qt::Key_Print;
    vkToQtKeyMap[VK_INSERT] = Qt::Key_Insert;
    vkToQtKeyMap[VK_DELETE] = Qt::Key_Delete;
    vkToQtKeyMap[VK_LWIN] = Qt::Key_Meta; // Windows key
    vkToQtKeyMap[VK_RWIN] = Qt::Key_Meta; // Windows key
    vkToQtKeyMap['A'] = Qt::Key_A;
    vkToQtKeyMap['B'] = Qt::Key_B;
    vkToQtKeyMap['C'] = Qt::Key_C;
    vkToQtKeyMap['D'] = Qt::Key_D;
    vkToQtKeyMap['E'] = Qt::Key_E;
    vkToQtKeyMap['F'] = Qt::Key_F;
    vkToQtKeyMap['G'] = Qt::Key_G;
    vkToQtKeyMap['H'] = Qt::Key_H;
    vkToQtKeyMap['I'] = Qt::Key_I;
    vkToQtKeyMap['J'] = Qt::Key_J;
    vkToQtKeyMap['K'] = Qt::Key_K;
    vkToQtKeyMap['L'] = Qt::Key_L;
    vkToQtKeyMap['M'] = Qt::Key_M;
    vkToQtKeyMap['N'] = Qt::Key_N;
    vkToQtKeyMap['O'] = Qt::Key_O;
    vkToQtKeyMap['P'] = Qt::Key_P;
    vkToQtKeyMap['Q'] = Qt::Key_Q;
    vkToQtKeyMap['R'] = Qt::Key_R;
    vkToQtKeyMap['S'] = Qt::Key_S;
    vkToQtKeyMap['T'] = Qt::Key_T;
    vkToQtKeyMap['U'] = Qt::Key_U;
    vkToQtKeyMap['V'] = Qt::Key_V;
    vkToQtKeyMap['W'] = Qt::Key_W;
    vkToQtKeyMap['X'] = Qt::Key_X;
    vkToQtKeyMap['Y'] = Qt::Key_Y;
    vkToQtKeyMap['Z'] = Qt::Key_Z;
}

void ClickClass::onToggle()
{

}

void ClickClass::onBean()
{

}
