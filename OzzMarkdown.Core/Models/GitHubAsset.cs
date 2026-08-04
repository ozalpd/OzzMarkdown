using System.Text.Json.Serialization;

namespace OzzMarkdown.Core.Models;

public class GitHubAsset
{
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("browser_download_url")]
    public string BrowserDownloadUrl { get; set; } = string.Empty;
}

