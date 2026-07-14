using System.IO;
using System.Text.Json;
using static System.Environment;

namespace OzzWpf.Core.Models
{
    public abstract class AbstractAppSettings
    {

        /// <summary>
        /// Gets or sets the position and size of the main application window.
        /// </summary>
        public WindowPosition MainWindowPosition { get; set; } = new WindowPosition();

        /// <summary>
        /// Gets or sets the BCP-47 culture name used for the application UI (e.g. <c>"en-US"</c>, <c>"tr-TR"</c>).
        /// </summary>
        /// <remarks>When empty, the operating system's current culture is used.</remarks>
        public string UiCulture { get; set; } = string.Empty;

        public string SelectedTheme { get; set; } = "Light";


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
            var settingsJson = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, settingsJson);
        }
    }
}
