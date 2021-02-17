using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLK.AspNetCore.Lab
{
    public partial class Program
    {
        // Methods
        public static void Main(string[] args)
        {
            // Setting
            Program.ShowConsole();

            // Run
            CLK.AspNetCore.Application.Run();
        }
    }

    #region Win32 API

    public partial class Program
    {
        // Imports
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int hConsoleHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);


        // Constants
        private const int MF_BYCOMMAND = 0x00000000;

        private const int SC_CLOSE = 0xF060;

        private const int STD_INPUT_HANDLE = -10;

        private const uint ENABLE_QUICK_EDIT_MODE = 0x0040;


        // Methods
        public static void ShowConsole()
        {
            // Window
            var window = Program.GetConsoleWindow();
            if (window == IntPtr.Zero) throw new InvalidOperationException();

            // Show
            Program.ShowWindow(window, 5);

            // Setting
            Program.DisableCloseButton();
            Program.DisableQuickEditMode();
        }

        public static void HideConsole()
        {
            // Window
            var window = Program.GetConsoleWindow();
            if (window == IntPtr.Zero) throw new InvalidOperationException();

            // Hide
            Program.ShowWindow(window, 0);

            // Setting
            Program.DisableCloseButton();
            Program.DisableQuickEditMode();
        }

        private static void DisableCloseButton()
        {
            // Window
            var window = Program.GetConsoleWindow();
            if (window == IntPtr.Zero) throw new InvalidOperationException();

            // Menu
            var menu = Program.GetSystemMenu(window, false);
            if (menu == IntPtr.Zero) throw new InvalidOperationException();

            // Disable
            Program.DeleteMenu(menu, SC_CLOSE, MF_BYCOMMAND);
        }

        private static void DisableQuickEditMode()
        {
            // Window
            IntPtr window = Program.GetStdHandle(STD_INPUT_HANDLE);
            if (window == IntPtr.Zero) throw new InvalidOperationException();

            // ConsoleMode
            uint consoleMode;
            Program.GetConsoleMode(window, out consoleMode);

            // Disable
            consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
            Program.SetConsoleMode(window, consoleMode);
        }
    }

    #endregion
}
