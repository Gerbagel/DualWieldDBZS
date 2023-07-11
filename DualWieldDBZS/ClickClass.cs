using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DualWieldDBZS
{
    internal class ClickClass
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwextrainfo);

        public enum mouseeventflags
        {
            LeftDown = 0x02,
            LeftUp = 0x04,
            RightDown = 0x08,
            RightUp = 0x10,
        }

        public ClickClass() {
            // fuck
        }

        public void leftClick(Point point)
        {
            mouse_event((int)(mouseeventflags.LeftDown | mouseeventflags.LeftUp), point.X, point.Y, 0, 0);
        }

        public void rightClick(Point point)
        {
            mouse_event((int)(mouseeventflags.RightDown | mouseeventflags.RightUp), point.X, point.Y, 0, 0);
        }
    }
}
