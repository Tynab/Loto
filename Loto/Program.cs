using Loto.Screen;
using System;
using static System.Windows.Forms.Application;

namespace Loto
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            Run(new FrmMain());
        }
    }
}
