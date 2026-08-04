using OzzMarkdown.Core.Models;

namespace OzzMarkdown.WPF.Models;

internal class ReleaseSource : IReleaseSource
{
    public string CurrentVersion => AppVersion.Version;

    public string RepositoryName => "OzzMarkdown";

    public string RepositoryApiUrl => "https://api.github.com/repos/ozalpd/OzzMarkdown";

    public string RepositoryUrl => "https://github.com/ozalpd/OzzMarkdown";
}
