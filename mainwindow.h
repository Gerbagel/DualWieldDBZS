#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

#include "clickclass.h"
#include "hotkeyeventfilter.h"

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(HotkeyEventFilter* eventFilter, QWidget *parent = nullptr);
    ~MainWindow();

private:
    Ui::MainWindow *ui;
    ClickClass* m_pClickClass;

private slots:
    void changeButtonText(QString str);
};
#endif // MAINWINDOW_H
