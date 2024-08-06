#ifndef SCALEDBGWIDGET_H
#define SCALEDBGWIDGET_H

#include <QWidget>
#include <QPainter>

class ScaledBgWidget : public QWidget
{
    Q_OBJECT
protected:
    // Override from parent
    void paintEvent(QPaintEvent *event) override
    {
        QPainter painter(this);
        QPixmap pixmap(":/images/assets/background.png");

        pixmap = pixmap.scaled(32, 32);

        qDebug() << pixmap.width() << " | "
                 << pixmap.height() << "\n";

        // Tile the pixmap across the widget
        for (int x = 0; x < width(); x += pixmap.width())
        {
            for (int y = 0; y < height(); y += pixmap.height())
            {
                painter.drawPixmap(x, y, pixmap);
                qDebug() << y << "\n";
            }
        }
    }
public:
    explicit ScaledBgWidget(QWidget *parent = nullptr);

signals:
};

#endif // SCALEDBGWIDGET_H
