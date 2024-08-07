#ifndef HOTKEYEVENTFILTER_H
#define HOTKEYEVENTFILTER_H

#include <QAbstractNativeEventFilter>
#include <QDebug>

class HotkeyEventFilter : public QAbstractNativeEventFilter
{
public:
    bool nativeEventFilter(const QByteArray &eventType, void *message, qintptr* result) override;

    std::function<void()> sendToggle;
    std::function<void()> sendBean;

    void defineFunctions(std::function<void()> onToggle, std::function<void()> onBean);

    bool wasToggled = false;
};

#endif // HOTKEYEVENTFILTER_H
