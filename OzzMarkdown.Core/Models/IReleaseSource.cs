namespace OzzMarkdown.Core.Models;

/// <summary>
/// Provides application release metadata required by release-checking logic.
/// </summary>
public interface IReleaseSource
{
    /// <summary>
    /// Gets the currently installed application version.
    /// </summary>
    string CurrentVersion { get; }

    /// <summary>
    /// Gets the name of the repository.
    /// </summary>
    string RepositoryName { get; }

    /// <summary>
    /// Gets the API endpoint URL for the repository that exposes release data.
    /// </summary>
    string RepositoryApiUrl { get; }

    /// <summary>
    /// Gets the browser-facing repository URL.
    /// </summary>
    string RepositoryUrl { get; }
}
