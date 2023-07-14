/* I am intensely sorry for my lack of comments */

using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace DualWieldDBZS
{
    public partial class Form1 : Form
    {
        /* DLL Imports */
        // Hotkey registration functions for Ctrl+Tab
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Windows hooks to get keyboard input so it stops when a menu is opened. Sorry custom keybinders!
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Error stuff, useful for debugging
        [DllImport("Kernel32.dll")]
        public static extern uint GetLastError();
        // I have no idea why this is necessary, but it is
        [DllImport("Kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        // Defines
        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;

        private static IntPtr hhook = IntPtr.Zero;

        private LowLevelKeyboardProc _proc = hookProc;

        ClickClass cc;
        int toggleId;
        int beanId;

        static bool isInventoryOpen;
        static bool isPauseOpen;
        static bool isDbcOpen;
        static bool isActionMenuOpen;

        public Form1()
        {
            InitializeComponent();

            // Register hotkeys
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
            Debug.WriteLine(Keys.Control.GetHashCode());

            SetHook();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, toggleId);
            UnregisterHotKey(this.Handle, beanId);

            UnHook();
        }

        bool stop = true;
        bool send1 = true;
        private void ToggleOnButton_Click(object sender, EventArgs e)
        {
            toggleTheThing();
        }

        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }
        
        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys k = (Keys)Marshal.ReadInt32(lParam);
                Debug.WriteLine(k.ToString());

                isInventoryOpen = k == Keys.E;
                isPauseOpen = k == Keys.Escape;
                isDbcOpen = k == Keys.V || k == Keys.L || k == Keys.K;
                isActionMenuOpen = k == Keys.X;
            }

            return CallNextHookEx(hhook, code, (int)wParam, lParam);
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
            if (!stop && (isInventoryOpen || isPauseOpen ||isDbcOpen || isActionMenuOpen))
            {
                toggleTheThing();
                return;
            }

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