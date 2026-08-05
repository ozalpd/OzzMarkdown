using System.Windows;

namespace OzzMarkdown.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string? filePathToOpen = e.Args.Length > 0 ? e.Args[0] : null;
            var mainWindow = new MainWindow(filePathToOpen);
            MainWindow = mainWindow;
            mainWindow.Show();
        }
    }

}
