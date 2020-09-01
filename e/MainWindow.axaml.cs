using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Squirrel;

namespace e
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            CheckUpdate();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private async void CheckUpdate()
        {
            using (var updateManager = new UpdateManager(@"D:\test\aug\31\c\e\bin\Debug"))
            {
                var CurrentVersionText = $"Current version: {updateManager.CurrentlyInstalledVersion()}";
                var releaseEntry = await updateManager.UpdateApp();
                var NewVersionText = $"Update Version: {releaseEntry?.Version.ToString() ?? "No update"}";

            }
        }
    }
}
