#include "mainwindow.h"
#include "./ui_mainwindow.h"

#include "usersettings.h"

MainWindow::MainWindow(HotkeyEventFilter* eventFilter, QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    setWindowFlags(Qt::Window | Qt::MSWindowsFixedSizeDialogHint);
    this->setFixedSize(250, 210);
    ui->setupUi(this);

    m_pClickClass = new ClickClass((u_int*) this->winId());

    connect(m_pClickClass->worker, &ClickWorker::changeButtonText, this, &MainWindow::changeButtonText);

    eventFilter->defineFunctions([m_pClickClass = m_pClickClass]()
    {
        m_pClickClass->onToggle();
    },
    [m_pClickClass = m_pClickClass]()
    {
        m_pClickClass->onBean();
    });

    connect(ui->toggleButton, &QPushButton::clicked, this, [m_pClickClass = m_pClickClass]()
    {
        m_pClickClass->onToggle();
    });

    connect(ui->intervalSpinBox, &QSpinBox::valueChanged, this, [ui = ui]()
    {
        UserSettings::instance()->setInterval((float) ui->intervalSpinBox->value());
    });

    ui->intervalSpinBox->setValue(UserSettings::instance()->getInterval());

    // todo settings button and menu
}

MainWindow::~MainWindow()
{
    delete ui;
    delete m_pClickClass;
}

void MainWindow::changeButtonText(QString str)
{
    ui->toggleButton->setText(str);
}
