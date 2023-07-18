using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Win32;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Reactive.Subjects;
using System.Xml;
using Avalonia.Rendering;

namespace DBZSDualWieldAva
{
    public sealed class ClickClass : Form
    {
        private static ClickClass instance = null;
        private static readonly object padlock = new object();
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

        // Windows mouse_events
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwextrainfo);
        // Enum so I don't have to remember the codes
        public enum mouseeventflags
        {
            LeftDown = 0x02,
            LeftUp = 0x04,
            RightDown = 0x08,
            RightUp = 0x10,
        }

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

        int toggleId;
        int beanId;

        private static Dictionary<Keys, bool> keyDict = new Dictionary<Keys, bool>();
        private string keysString;

        CancellationTokenSource cts;

        public Subject<string> ButtonSrc = new Subject<string>();
        public bool stop = true;
        bool send1 = true;

        private ClickClass()
        {
            keysString = UserSettings.Instance.CancelKeys;

            cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ClickTimerThread), cts.Token);

            // Register hotkeys 
            toggleId = 1;
            bool toggleHotkeyRegistered = RegisterHotKey(
                Handle, toggleId, 0x0002, Keys.Tab.GetHashCode());

#if DEBUG
            if (toggleHotkeyRegistered)
            {
                Debug.WriteLine("ToggleKey Success!");
            }
            else
            {
                Debug.WriteLine("ToggleKey failed");
                Debug.WriteLine(GetLastError());
            }
#endif

            beanId = 2;
            bool beanKeyRegistered = RegisterHotKey(
                Handle, beanId, 0x0000, Keys.F8.GetHashCode());

#if DEBUG
            if (beanKeyRegistered)
            {
                Debug.WriteLine("BeanKey success!");
            } else
            {
                Debug.WriteLine("BeanKey failed");
                Debug.WriteLine(GetLastError());
            }
#endif

            // Get which keys cancel the script
            string[] keysStringArray = keysString.Split(", ");
            for (int i = 0; i < keysStringArray.Length; i++)
            {
                Keys key;
                Enum.TryParse(keysStringArray[i], out key);
                keyDict.Add(key, false);
            }

            SetHook();
        }

        ~ClickClass()
        {
            cts.Cancel();
            UserSettings.Instance.SaveSettings();

            // Unregiser hotkeys
            UnregisterHotKey(Handle, toggleId);
            UnregisterHotKey(Handle, beanId);

            UnHook();
        }

        public static ClickClass Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClickClass();
                    }
                    return instance;
                }
            }
        }

        // Functions to both left and right click
        public void leftClick(Point point)
        {
            mouse_event((int)(mouseeventflags.LeftDown | mouseeventflags.LeftUp), point.X, point.Y, 0, 0);
        }

        public void rightClick(Point point)
        {
            mouse_event((int)(mouseeventflags.RightDown | mouseeventflags.RightUp), point.X, point.Y, 0, 0);
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

                if (keyDict.ContainsKey(k))
                    keyDict[k] = true;
                else
                {
                    for (int i = 0; i < keyDict.Count; i++)
                    {
                        keyDict[keyDict.ElementAt(i).Key] = false;
                    }
                }
            }

            return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }

        public void toggleTheThing()
        {
            if (keysString != UserSettings.Instance.CancelKeys)
            {
                // If the list has been updated, clear and re-parse
                keysString = UserSettings.Instance.CancelKeys;
                string[] keysStringArray = keysString.Split(", ");
                keyDict.Clear();
                for (int i = 0; i < keysStringArray.Length; i++)
                {
                    Keys Keys;
                    Enum.TryParse(keysStringArray[i], out Keys);
                    keyDict.Add(Keys, false);
                }
            }

            stop = !stop;
            ButtonSrc.OnNext(stop ? "Start (Ctrl+Tab)" : "Stop (Ctrl+Tab)");
        }

        private void bean()
        {
            if (!stop)
            {
                toggleTheThing();

                Thread.Sleep(30);

                SendKeys.SendWait("9");

                Thread.Sleep(30);

                rightClick(Cursor.Position);

                Thread.Sleep(30);

                toggleTheThing();
            }
        }

        private void ClickTimerThread(object? obj)
        {
            if (obj is null)
                return;

            CancellationToken token = (CancellationToken)obj;
            bool wasToggled = false;

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                if (stop)
                {
                    // Sleep set time here, to avoid lag
                    Thread.Sleep(500); // Extra sleepytime :)
                    continue;
                }

                foreach (var keyValuePair in keyDict)
                {
                    if (keyValuePair.Value)
                    {
                        toggleTheThing();
                        wasToggled = true;
                    }
                }

                // God this is a mess
                if (wasToggled)
                {
                    wasToggled = false;
                    continue;
                }

                SendKeys.SendWait(send1 ? "1" : "2");
                leftClick(Cursor.Position);
                send1 = !send1;
                Thread.Sleep(UserSettings.Instance.Interval);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                if (id == 1)
                {
#if DEBUG
                    Debug.WriteLine("Toggled");
#endif
                    toggleTheThing();
                }
                if (id == 2)
                {
#if DEBUG
                    Debug.WriteLine("Beaned");
#endif
                    bean();
                }
            }

            base.WndProc(ref m);
        }
    }
}