using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DualWieldDBZS
{
    public partial class Form1 : Form
    {
        ClickClass cc;
        int toggleId;
        int beanId;
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("Kernel32.dll")]
        public static extern uint GetLastError();
        public Form1()
        {
            InitializeComponent();

            cc = new ClickClass();
            toggleId = 1;
            bool toggleKeyRegistered = RegisterHotKey(
                this.Handle, toggleId, 0x0002, Keys.Tab.GetHashCode());

            if (toggleKeyRegistered)
            {
                Debug.WriteLine("ToggleKey success!");
            } else
            {
                Debug.WriteLine("ToggleKey failed");
                Debug.WriteLine(GetLastError());
            }

            beanId = 2;
            bool beanKeyRegistered = RegisterHotKey(
                this.Handle, beanId, 0x0000, Keys.F8.GetHashCode());

            if (beanKeyRegistered)
            {
                Debug.WriteLine("BeanKey success!");
            } else
            {
                Debug.WriteLine("BeanKey failed");
                Debug.WriteLine(GetLastError());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Debug.WriteLine("Hi :)");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, toggleId);
            UnregisterHotKey(this.Handle, beanId);
        }

        bool stop = true;
        bool send1 = true;
        private void ToggleOnButton_Click(object sender, EventArgs e)
        {
            toggleTheThing();
        }

        private void toggleTheThing()
        {
            stop = !stop;

            ClickTimer.Interval = (int)numericUpDown1.Value;
            ClickTimer.Enabled = true;

            ToggleOnButton.Text = stop ? "Start (Ctrl+Tab)" : "Stop (Ctrl+Tab)";

            if (!stop)
            {
                ClickTimer.Start();
            } else
            {
                ClickTimer.Stop();
            }
        }

        private void bean()
        {
            if (!stop)
            {
                toggleTheThing();

                Thread.Sleep(ClickTimer.Interval / 3);

                SendKeys.SendWait("9");

                Thread.Sleep(ClickTimer.Interval / 3);

                cc.rightClick(new Point(MousePosition.X, MousePosition.Y));

                Thread.Sleep(ClickTimer.Interval / 3);

                toggleTheThing();
            }
        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            SendKeys.SendWait(send1 ? "1" : "2");
            cc.leftClick(new Point(MousePosition.X, MousePosition.Y));

            send1 = !send1;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                if (id == 1)
                {
                    Debug.WriteLine("Toggled");
                    toggleTheThing();
                }
                if (id == 2)
                {
                    Debug.WriteLine("Beaned");
                    bean();
                }
            }

            base.WndProc(ref m);
        }
    }
}