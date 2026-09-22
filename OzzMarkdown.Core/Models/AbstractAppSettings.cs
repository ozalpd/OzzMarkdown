using System.Text.Json;
using static System.Environment;

namespace OzzMarkdown.Core.Models
{
    public abstract class AbstractAppSettings
    {
        /// <summary>
        /// Gets or sets the BCP-47 culture name used for the application UI (e.g. <c>"en-US"</c>, <c>"tr-TR"</c>).
        /// </summary>
        /// <remarks>When empty, the operating system's current culture is used.</remarks>
        public string UiCulture { get; set; } = string.Empty;

        public string SelectedTheme { get; set; } = "Light";

        /// <summary>
        /// Most recently used files, most recent first.
        /// </summary>
        public List<string> RecentFiles { get; set; } = new();

        /// <summary>
        /// Adds <paramref name="filePath"/> to the top of <see cref="RecentFiles"/>, removing any
        /// existing entry (case-insensitive) and trimming the list to <paramref name="maxCount"/> items.
        /// </summary>
        public void AddRecentFile(string filePath, int maxCount = 10)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            RecentFiles.RemoveAll(f => string.Equals(f, filePath, StringComparison.OrdinalIgnoreCase));
            RecentFiles.Insert(0, filePath);

            if (RecentFiles.Count > maxCount)
            {
                RecentFiles.RemoveRange(maxCount, RecentFiles.Count - maxCount);
            }
        }

        public abstract string GetSettingsFolderName();

        protected static string GetSettingsFilePath(string folderName, string settingsFileName)
        {
            var folderPath = Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), folderName);
            Directory.CreateDirectory(folderPath);

            return Path.Combine(folderPath, settingsFileName);
        }

        public void Save(string folderName, string settingsFileName)
        {
            var settingsFilePath = GetSettingsFilePath(folderName, settingsFileName);
            var settingsJson = JsonSerializer.Serialize(this, GetType(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, settingsJson);
        }
    }
}
