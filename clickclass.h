#ifndef CLICKCLASS_H
#define CLICKCLASS_H

#include <windows.h>
#include <QHash>
#include <QObject>

#define TOGGLE_HOTKEY_ID 1
#define BEAN_HOTKEY_ID 2

class ClickClass : public QObject
{
    Q_OBJECT

private:
    void populateMap();
    HWND windowHandle;
    float clickDelay = 170; // milliseconds

public:
    ClickClass(u_int* handle);
    ~ClickClass();

public slots:
    void onToggle();
    void onBean();
};

#endif // CLICKCLASS_H
