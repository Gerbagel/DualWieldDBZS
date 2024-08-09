#include "mainwindow.h"

#include <QApplication>
#include <QLocale>
#include <QTranslator>

#include "hotkeyeventfilter.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QTranslator translator;
    const QStringList uiLanguages = QLocale::system().uiLanguages();
    for (const QString &locale : uiLanguages) {
        const QString baseName = "DualWieldDBZS_" + QLocale(locale).name();
        if (translator.load(":/i18n/" + baseName)) {
            a.installTranslator(&translator);
            break;
        }
    }

    HotkeyEventFilter *eventFilter = new HotkeyEventFilter;
    a.installNativeEventFilter(eventFilter);

    MainWindow w(eventFilter);

    w.show();
    return a.exec();
}
