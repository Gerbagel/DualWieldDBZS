#include "usersettings.h"
#include <QFile>
#include <QJsonObject>
#include <QJsonArray>
#include <QJsonDocument>

UserSettings::UserSettings()
{
    settings = new UserSettingsData;

    if (!loadData())
    {
        settings->muted = false;
        settings->intervalMs = 170;
        settings->keyList.append(Qt::Key_E);
        settings->keyList.append(Qt::Key_Escape);
        settings->keyList.append(Qt::Key_V);
        settings->keyList.append(Qt::Key_L);
        settings->keyList.append(Qt::Key_K);
        settings->keyList.append(Qt::Key_X);
        settings->keyList.append(Qt::Key_T);

        saveData();
    }

    qDebug() << "Muted: " << settings->muted << " | Interval: " << settings->intervalMs << " | keyList:";
    foreach (Qt::Key key, settings->keyList)
    {
        qDebug() << key;
    }
}

bool UserSettings::loadData()
{
    QFile file(FILE_PATH);

    if (!file.open(QIODevice::ReadOnly))
    {
        qWarning() << "Could not open file for reading: " << FILE_PATH;
        return false;
    }

    QByteArray jsonData = file.readAll();
    QJsonDocument jsonDoc(QJsonDocument::fromJson(jsonData));
    QJsonObject jsonObj = jsonDoc.object();

    settings->muted = jsonObj["isMuted"].toBool();
    settings->intervalMs = (float) jsonObj["intervalMs"].toDouble(170);

    QJsonArray jsonArray = jsonObj["keyList"].toArray();
    foreach (auto key, jsonArray)
    {
        settings->keyList.append((Qt::Key) key.toInt());
    }

    return true;
}

bool UserSettings::saveData()
{
    QFile file(FILE_PATH);

    if (!file.open(QIODevice::WriteOnly))
    {
        qWarning() << "Could not open file for writing: " << FILE_PATH;
        return false;
    }

    QJsonObject json;

    json["isMuted"] = settings->muted;
    json["intervalMs"] = settings->intervalMs;

    QJsonArray jsonArray;
    foreach (Qt::Key key, settings->keyList)
    {
        jsonArray.append(static_cast<int>(key));
    }

    json["keyList"] = jsonArray;

    QJsonDocument jsonDoc(json);
    return file.write(jsonDoc.toJson());
}

void UserSettings::setInterval(float interval)
{
    settings->intervalMs = interval;
    saveData();
}

float UserSettings::getInterval()
{
    return settings->intervalMs;
}

QList<Qt::Key> UserSettings::getKeyList()
{
    return settings->keyList;
}
