#include "settingsdialog.h"
#include "ui_settingsdialog.h"

#include <QKeySequence>
#include <QMessageBox>
#include "usersettings.h"
#include "clickclass.h"

SettingsDialog::SettingsDialog(QWidget *parent)
    : QDialog(parent)
    , ui(new Ui::SettingsDialog)
{
    ui->setupUi(this);

    connect(ui->muteCheckBox, &QCheckBox::clicked, this, [](bool checked)
    {
        UserSettings::instance()->setMuted(checked);
    });
    connect(ui->saveAndCloseButton, &QPushButton::clicked, this, &SettingsDialog::saveAndClose);

    QString textBoxString;
    for (int i = 0; i < UserSettings::instance()->getKeyList().size(); i++)
    {
        Qt::Key key = UserSettings::instance()->getKeyList().at(i);

        QKeySequence seq(key);
        QString keyStr = seq.toString();
        textBoxString.append(keyStr);
        if (i != UserSettings::instance()->getKeyList().size()-1)
        {
            textBoxString.append(", ");
        }
    }
    ui->textEdit->setPlainText(textBoxString);

    ui->muteCheckBox->setCheckState(UserSettings::instance()->getMuted() ? Qt::Checked : Qt::Unchecked);
}

SettingsDialog::~SettingsDialog()
{
    delete ui;
}

QList<Qt::Key> SettingsDialog::stringToKeyList(QString str)
{
    QList<Qt::Key> output;

    QList<QString> splitList = str.split(", ");

    foreach (QString item, splitList)
    {
        QString simp = item.simplified();
        QKeySequence seq(simp);

        if (seq[0].key() == Qt::Key_unknown)
        {
            QMessageBox::warning(this, "Error", "Item '" + item + "' was not recognizable as key; skipping");
        }
        else
        {
            output.append(seq[0].key());
        }
    }

    return output;
}

void SettingsDialog::saveAndClose()
{
    UserSettings::instance()->setKeyList(stringToKeyList(ui->textEdit->toPlainText()));
    UserSettings::instance()->saveData();
    this->close();
}
