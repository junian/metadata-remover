using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetadataRemover.WinFormsApp.Services
{
    static class ConsoleHelper
    {

        #region Private Stuff

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetConsoleProcessList(uint[] ProcessList, uint ProcessCount);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion Private Stuff

        #region Public Stuff

        /// <summary>
        /// Returns true if application is the sole owner of the current console.
        /// </summary>
        public static bool IsSoleConsoleOwner
        {
            get
            {
                uint[] procIds = new uint[4];
                uint count = GetConsoleProcessList(procIds, (uint)procIds.Length);
                return count <= 1;
            }
        }

        /// <summary>
        /// If applicaiton is the sole console owner, prompts the user to press
        /// any key before returning - presumably for the application to exit.
        /// </summary>
        public static void PromptAndWaitIfSoleConsole()
        {
            if (IsSoleConsoleOwner)
            {
                var oldColor = Console.ForegroundColor;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press any key to exit.");
                Console.ForegroundColor = oldColor;
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Brings the console to the front
        /// </summary>
        public static void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }

        #endregion Public Stuff
    }
}
