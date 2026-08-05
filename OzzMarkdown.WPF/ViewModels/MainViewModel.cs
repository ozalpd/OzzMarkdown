using OzzMarkdown.Core.Extensions;
using OzzMarkdown.Core.Models;
using OzzMarkdown.WPF.Models;
using OzzMarkdown.WPF.Services;
using OzzWpf.Core.Commands;
using OzzWpf.Core.Dialogs;
using OzzWpf.Core.ViewModels;
using System.IO;
using System.Windows;

namespace OzzMarkdown.WPF.ViewModels;

public class MainViewModel : AbstractViewModel
{
    private readonly IFileDialogService _fileDialogService;
    private ReleaseSource _releaseSource = new ReleaseSource();


    public MainViewModel() : this(new Win32FileDialogService()) { }

    public MainViewModel(IFileDialogService fileDialogService)
    {
        _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));

        CheckForUpdatesCommand = new RelayCommand(async () => await CheckForUpdatesAsync());
        OpenFileCommand = new RelayCommand(async () => await OpenMarkdownFileAsync());
        ShowAboutCommand = new RelayCommand(ShowAboutDialog);

        _ = Task.Run(CheckForUpdatesAsync);
    }


    public RelayCommand CheckForUpdatesCommand { get; }

    public RelayCommand OpenFileCommand { get; }

    public RelayCommand ShowAboutCommand { get; }

    public GitHubRelease? LatestRelease { get; private set; }

    public string? CurrentFilePath
    {
        get => _currentFilePath;
        set
        {
            if (_currentFilePath != value)
            {
                _currentFilePath = value;
                RaisePropertyChanged(nameof(CurrentFilePath));
            }
        }
    }
    private string? _currentFilePath;

    public bool GenerateToc
    {
        get => _generateToc ?? true;
        set
        {
            if (_generateToc != value)
            {
                _generateToc = value;
                RaisePropertyChanged(nameof(GenerateToc));
            }
        }
    }
    private bool? _generateToc;

    public bool HasNewerVersion
    {
        get => _hasNewerVersion;
        set
        {
            if (_hasNewerVersion != value)
            {
                _hasNewerVersion = value;
                RaisePropertyChanged(nameof(HasNewerVersion));
            }
        }
    }
    private bool _hasNewerVersion;

    public string? MarkdownContent
    {
        get => _markdownContent;
        set
        {
            if (_markdownContent != value)
            {
                _markdownContent = value;
                RaisePropertyChanged(nameof(MarkdownContent));
            }
        }
    }
    private string? _markdownContent;

    private async Task CheckForUpdatesAsync()
    {
        try
        {
            LatestRelease = await _releaseSource.GetGitHubReleaseAsync();
            if (LatestRelease != null && !string.IsNullOrEmpty(LatestRelease.TagName))
            {
                // Compare the current version with the latest release version
                Version currentVersion = new Version(_releaseSource.CurrentVersion);
                Version latestVersion = new Version(LatestRelease.TagName.TrimStart('v'));
                HasNewerVersion = latestVersion > currentVersion;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log them or show a message to the user)
        }
    }

    private async Task OpenMarkdownFileAsync()
    {
        string? filePath = _fileDialogService.OpenMarkdownFile();

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }
        await LoadMarkdownFileAsync(filePath);
    }

    public async Task LoadMarkdownFileAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            return;
        }
        try
        {
            string markdownContent = await File.ReadAllTextAsync(filePath);
            MarkdownContent = markdownContent;
            CurrentFilePath = filePath;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log them or show a message to the user)
        }
    }

    private void ShowAboutDialog()
    {
        var aboutDialog = new AboutDialog(_releaseSource);
        if (Application.Current?.MainWindow != null)
        {
            aboutDialog.Owner = Application.Current.MainWindow;
        }
        aboutDialog.LoadHighResolutionIcon("pack://application:,,,/OzzMarkdown.WPF;component/Assets/icon-M-03.ico");
        aboutDialog.ShowDialog();
    }
}
