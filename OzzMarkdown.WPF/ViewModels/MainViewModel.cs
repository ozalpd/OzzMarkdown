using OzzMarkdown.WPF.Services;
using OzzWpf.Core.Commands;
using OzzWpf.Core.ViewModels;
using System.IO;

namespace OzzMarkdown.WPF.ViewModels;

public class MainViewModel : AbstractViewModel
{
    private readonly IFileDialogService _fileDialogService;

    public MainViewModel() : this(new Win32FileDialogService()) { }

    public MainViewModel(IFileDialogService fileDialogService)
    {
        _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));
        OpenFileCommand = new RelayCommand(async () => await OpenMarkdownFileAsync());
    }

    public RelayCommand OpenFileCommand { get; }



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

    private async Task OpenMarkdownFileAsync()
    {
        string? filePath = _fileDialogService.OpenMarkdownFile();

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
}
