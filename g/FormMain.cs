using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace g
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            var t = UpdateApp();
        }
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for Squirrel Updates
            var t = UpdateApp();
        }
        public async Task UpdateApp()
        {
            var updatePath = ConfigurationManager.AppSettings["UpdatePathFolder"];
            var packageId = ConfigurationManager.AppSettings["PackageID"];

            using (var mgr = new UpdateManager(updatePath, packageId))//, FrameworkVersion.Net45))
            {
                var updates = await mgr.CheckForUpdate();
                if (updates.ReleasesToApply.Any())
                {
                    var lastVersion = updates.ReleasesToApply.OrderBy(x => x.Version).Last();
                    await mgr.DownloadReleases(new[] { lastVersion });
                    await mgr.ApplyReleases(updates);
                    await mgr.UpdateApp();

                    MessageBox.Show("The application has been updated – please close and restart.");
                }
                else
                {
                    //MessageBox.Show("No Updates are available at this time.");
                }
            }
        }
        public static void OnAppUpdate(Version version)
        {
            // Could use this to do stuff here too.
        }
        public static void OnInitialInstall(Version version)
        {
            var exePath = Assembly.GetEntryAssembly().Location;
            string appName = Path.GetFileName(exePath);

            var updatePath = ConfigurationManager.AppSettings["UpdatePathFolder"];
            var packageId = ConfigurationManager.AppSettings["PackageID"];

            using (var mgr = new UpdateManager(updatePath, packageId)) //, FrameworkVersion.Net45
            {

                // Create Desktop and Start Menu shortcuts
                //mgr.CreateShortcutsForExecutable(appName, DefaultLocations, false);
                //mgr.CreateShortcutsForExecutable(appName, DefaultLocations, false);
            }
        }
        public static void OnAppUninstall(Version version)
        {
            var exePath = Assembly.GetEntryAssembly().Location;
            string appName = Path.GetFileName(exePath);

            var updatePath = ConfigurationManager.AppSettings["UpdatePathFolder"];
            var packageId = ConfigurationManager.AppSettings["PackageID"];

            using (var mgr = new UpdateManager(updatePath, packageId))//, FrameworkVersion.Net45))
            {
                // Remove Desktop and Start Menu shortcuts
                //mgr.RemoveShortcutsForExecutable(appName, DefaultLocations);
                //mgr.RemoveShortcutsForExecutable(appName, DefaultLocations);
            }
        }

    }
}