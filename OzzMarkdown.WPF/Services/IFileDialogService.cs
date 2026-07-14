namespace OzzMarkdown.WPF.Services;

/// <summary>
/// Abstracts native file-dialog interactions so ViewModels stay decoupled from WPF/Win32 dialog APIs.
/// </summary>
public interface IFileDialogService
{
    /// <summary>
    /// Shows an open-file dialog filtered to Markdown files.
    /// </summary>
    /// <returns>The selected file path, or <c>null</c> if the dialog was cancelled.</returns>
    string? OpenMarkdownFile();
}
