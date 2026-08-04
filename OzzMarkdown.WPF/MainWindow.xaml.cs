using OzzMarkdown.Core.Models;
using OzzMarkdown.i18n;
using OzzMarkdown.WPF.Models;
using OzzMarkdown.WPF.ViewModels;
using OzzWpf.Core.Controls;
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
    private MainViewModel _viewModel;

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
        _appSettings.MainWindowPosition.SetWindowPositions(this);

        _viewModel = new MainViewModel();
        DataContext = _viewModel;
        _viewModel.PropertyChanged += OnPropertyChanged;

        _markdownViewer.SetBinding(MarkdownViewer.MarkdownContentProperty,
                        new Binding(nameof(MainViewModel.MarkdownContent)) { Source = _viewModel });
        _markdownViewer.SetBinding(MarkdownViewer.GenerateTocProperty,
                        new Binding(nameof(MainViewModel.GenerateToc)) { Source = _viewModel });

    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        _appSettings.MainWindowPosition.GetWindowPositions(this);
        _appSettings.Save();
    }

    private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.HasNewerVersion) && _viewModel.HasNewerVersion)
        {
            var release = _viewModel.LatestRelease;
            var newVersion = string.Format(LocalizedStrings.NewVersionAvailable, release?.TagName);
            var msgResult = MessageBox.Show($"{newVersion}\n\n{LocalizedStrings.WannaSeeRelease}", LocalizedStrings.UpdateAvailable, MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (msgResult == MessageBoxResult.Yes && release != null)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = release.HtmlUrl,
                    UseShellExecute = true
                });
            }
        }
    }
}
