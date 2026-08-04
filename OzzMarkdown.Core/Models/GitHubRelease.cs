using System.Text.Json.Serialization;

namespace OzzMarkdown.Core.Models;

public class GitHubRelease
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = string.Empty;

    [JsonPropertyName("published_at")]
    public DateTime? PublishDate {  get; set; }
}
