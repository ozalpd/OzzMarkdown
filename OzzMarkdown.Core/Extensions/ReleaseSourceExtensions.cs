using OzzMarkdown.Core.Models;
using System.Text.Json;

namespace OzzMarkdown.Core.Extensions
{
    public static class ReleaseSourceExtensions
    {
        /// <summary>
        /// Gets the latest GitHub release information from the specified release source.
        /// </summary>
        /// <param name="releaseSource"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static async Task<GitHubRelease?> GetGitHubReleaseAsync(this IReleaseSource releaseSource, string? tagName = null)
        {
            GitHubRelease? release = null;
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.UserAgent.ParseAdd(releaseSource.BuildUserAgent());

                var response = await http.GetAsync(releaseSource.BuildGitHubReleaseUrl(tagName));
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                release = JsonSerializer.Deserialize<GitHubRelease>(json);
            }

            return release;
        }

        private static string BuildGitHubReleaseUrl(this IReleaseSource releaseSource, string? tagName = null)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                tagName = "latest";
            return $"{releaseSource.RepositoryApiUrl}/releases/{tagName}";
        }

        private static string BuildUserAgent(this IReleaseSource releaseSource)
        {
            string currentVersion = string.IsNullOrWhiteSpace(releaseSource.CurrentVersion)
                                  ? "unknown"
                                  : releaseSource.CurrentVersion;

            return $"{releaseSource.RepositoryName}UpdateChecker/{currentVersion}";
        }
    }
}
