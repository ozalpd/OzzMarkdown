using OzzMarkdown.i18n;

namespace OzzMarkdown.WPF.Services;

/// <summary>
/// <see cref="IFileDialogService"/> implementation backed by <see cref="Microsoft.Win32.OpenFileDialog"/>.
/// </summary>
public class Win32FileDialogService : IFileDialogService
{
    public string? OpenMarkdownFile()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog { Filter = LocalizedStrings.MarkdownFileFilter };
        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
