#ifndef CLICKCLASS_H
#define CLICKCLASS_H

#include <windows.h>
#include <QHash>
#include <QObject>
#include <QThread>

#define TOGGLE_HOTKEY_ID 1
#define BEAN_HOTKEY_ID 2

// forward declare
class ClickClass;

class ClickWorker : public QThread
{
    Q_OBJECT
    void run() override;

private:
    ClickClass* parent;
    bool stopThread = true;

public slots:
    void stopThreadPeacefully();

public:
    ClickWorker(ClickClass* parent);
signals:
    void changeButtonText(QString str);

};

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

    ClickWorker* worker;
    bool clickerOn = false;

signals:
    void stopThread();
public slots:
    void onToggle();
    void onBean();
};

#endif // CLICKCLASS_H
