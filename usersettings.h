#ifndef USERSETTINGS_H
#define USERSETTINGS_H

#include <QList>

#define FILE_PATH "UserSettings.json"

struct UserSettingsData
{
    bool muted;
    float intervalMs;
    QList<Qt::Key> keyList;
};

class UserSettings
{
private:
    UserSettingsData* settings;

public:

    /**
     * Singletons should not be cloneable.
     */
    UserSettings(UserSettings &other) = delete;
    /**
     * Singletons should not be assignable.
     */
    void operator=(const UserSettings &) = delete;

    UserSettings();

    static UserSettings* instance()
    {
        static UserSettings* singleton = new UserSettings;

        return singleton;
    }

    bool loadData();
    bool saveData();

    void setInterval(float interval);
    float getInterval();

    void setMuted(bool muted);
    bool getMuted();

    void setKeyList(QList<Qt::Key> keyList);
    QList<Qt::Key> getKeyList();
};

#endif // USERSETTINGS_H
