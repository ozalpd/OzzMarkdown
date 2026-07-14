using OzzMarkdown.WPF.Models;
using OzzMarkdown.WPF.ViewModels;
using OzzWpf.Core.Controls;
using OzzWpf.Core.Models;
using System.Windows;
using System.Windows.Data;

namespace OzzMarkdown.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AppSettings _appSettings = AppSettings.GetAppSettings();
    private readonly MarkdownViewer _markdownViewer;

    public MainWindow()
    {
        InitializeComponent();

        _markdownViewer = new MarkdownViewer(_appSettings);
        MarkdownViewerHost.Content = _markdownViewer;

        SourceInitialized += MainWindow_SourceInitialized;
        Closing += MainWindow_Closing;
    }

    private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
    {
        SourceInitialized -= MainWindow_SourceInitialized;
        Title = $"Ozz Markdown - v{AppVersion.Version}";

        var viewModel = new MainViewModel();
        DataContext = viewModel;

        _markdownViewer.SetBinding(
            MarkdownViewer.MarkdownContentProperty,
            new Binding(nameof(MainViewModel.MarkdownContent)) { Source = viewModel });

        _appSettings.MainWindowPosition.SetWindowPositions(this);
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        _appSettings.MainWindowPosition.GetWindowPositions(this);
        _appSettings.Save();
    }
}
