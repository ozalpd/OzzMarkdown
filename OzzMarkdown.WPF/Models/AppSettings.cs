using OzzWpf.Core.Models;
using System.IO;
using System.Text.Json;

namespace OzzMarkdown.WPF.Models;

public class AppSettings : AbstractAppSettings
{
    private static AppSettings? _instance;
    private static readonly object _syncRoot = new();

    private static string settingsFolderName = "OzzMarkdown";
    private static string settingsFileName = "wpfsettings.json";


    public static AppSettings GetAppSettings()
    {
        if (_instance is not null)
        {
            return _instance;
        }

        lock (_syncRoot)
        {
            if (_instance is not null)
            {
                return _instance;
            }

            var settingsFilePath = GetSettingsFilePath(settingsFolderName, settingsFileName);
            if (File.Exists(settingsFilePath))
            {
                var settingsJson = File.ReadAllText(settingsFilePath);
                if (!string.IsNullOrWhiteSpace(settingsJson))
                {
                    try
                    {
                        _instance = JsonSerializer.Deserialize<AppSettings>(settingsJson);
                    }
                    catch (JsonException)
                    {
                    }
                    catch (NotSupportedException)
                    {
                    }
                }
            }

            _instance ??= new AppSettings();
            return _instance;
        }
    }

    public override string GetSettingsFolderName() => settingsFolderName;

    public void Save()
    {
        Save(settingsFolderName, settingsFileName);
    }
}