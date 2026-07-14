using OzzMarkdown.Core;
using OzzWpf.Core.Models;

namespace OzzWpf.Core.Controls;

/// <summary>
/// Interaction logic for MarkdownViewer.xaml
/// </summary>
public partial class MarkdownViewer : System.Windows.Controls.UserControl
{
    private readonly string settingsFolderName;
    private readonly string virtualHostName;
    private MarkdownHtmlRenderer mdRenderer;

    public MarkdownViewer(AbstractAppSettings appSettings)
    {
        InitializeComponent();
        AppSettings = appSettings;
        settingsFolderName = AppSettings.GetSettingsFolderName();
        virtualHostName = $"{settingsFolderName.ToLowerInvariant()}.local";

        mdRenderer = new MarkdownHtmlRenderer(virtualHostName, settingsFolderName);
        Loaded += MarkdownViewer_Loaded;
        Unloaded += MarkdownViewer_Unloaded;
    }

    public AbstractAppSettings AppSettings { get; }

    public static readonly System.Windows.DependencyProperty MarkdownContentProperty =
        System.Windows.DependencyProperty.Register(
            nameof(MarkdownContent),
            typeof(string),
            typeof(MarkdownViewer),
            new System.Windows.PropertyMetadata(null, OnMarkdownContentChanged));

    public string? MarkdownContent
    {
        get => (string?)GetValue(MarkdownContentProperty);
        set => SetValue(MarkdownContentProperty, value);
    }

    private static void OnMarkdownContentChanged(System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
    {
        if (d is MarkdownViewer viewer)
        {
            viewer.LoadMarkdown();
        }
    }

    public MarkdownTheme Theme
    {
        set { _markdownTheme = value; }
        get
        {
            if (_markdownTheme == null)
                _markdownTheme = MarkdownThemeProvider.GetTheme(AppSettings.SelectedTheme);
            return _markdownTheme;
        }
    }
    private MarkdownTheme? _markdownTheme;

    private async void MarkdownViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await Browser.EnsureCoreWebView2Async();
        Browser.CoreWebView2.SetVirtualHostNameToFolderMapping(
            virtualHostName,
            mdRenderer.TempFolder,
            Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);

        if (!string.IsNullOrEmpty(MarkdownContent))
        {
            LoadMarkdown();
        }
    }

    private void MarkdownViewer_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {
        mdRenderer.CleanupTempFiles();
    }

    public void LoadMarkdown(MarkdownTheme? theme = null)
    {
        if (theme != null)
            Theme = theme;

        if (Browser.CoreWebView2 == null || string.IsNullOrEmpty(MarkdownContent))
            return;

        string fileUrl = mdRenderer.RenderToTempFileUrl(MarkdownContent, Theme);
        Browser.CoreWebView2.Navigate(fileUrl);
    }

    public void LoadMarkdown(string markdown, MarkdownTheme? theme = null)
    {
        MarkdownContent = markdown;
    }
}