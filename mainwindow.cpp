#include "mainwindow.h"
#include "./ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    setWindowFlags(Qt::Window | Qt::MSWindowsFixedSizeDialogHint);
    this->setFixedSize(250, 210);
    ui->setupUi(this);

    m_pClickClass = new ClickClass((u_int*) this->winId());
}

MainWindow::~MainWindow()
{
    delete ui;
    delete m_pClickClass;
}
