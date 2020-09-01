using System;
using System.Windows.Forms;
using Squirrel;

namespace g
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SquirrelAwareApp.HandleEvents(onAppUpdate: FormMain.OnAppUpdate, onAppUninstall: FormMain.OnAppUninstall, onInitialInstall: FormMain.OnInitialInstall);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
