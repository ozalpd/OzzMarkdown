using OzzMarkdown.WPF.Models;
using OzzMarkdown.WPF.ViewModels;
using System.Windows;

namespace OzzMarkdown.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AppSettings _appSettings = AppSettings.GetAppSettings();

    public MainWindow()
    {
        InitializeComponent();
        SourceInitialized += MainWindow_SourceInitialized;
        Closing += MainWindow_Closing;
    }

    private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
    {
        SourceInitialized -= MainWindow_SourceInitialized;
        Title = $"Ozz Markdown - v{AppVersion.Version}";
        this.DataContext = new MainViewModel();
        _appSettings.MainWindowPosition.SetWindowPositions(this);
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        _appSettings.MainWindowPosition.GetWindowPositions(this);
        _appSettings.Save();
    }
}
