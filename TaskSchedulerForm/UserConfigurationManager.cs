using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    public class UserConfigurationManager
    {
        // Ładuje konfigurację/preferencje użytkowników do pliku JSON
        public AppConfiguration LoadConfiguration()
        {
            string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
            string configFilePath = Path.Combine(appDataFolder, "config.json");

            try
            {
                if (File.Exists(configFilePath))
                {
                    string json = File.ReadAllText(configFilePath);
                    return JsonSerializer.Deserialize<AppConfiguration>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy wczytywaniu konfiguracji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Tworzy domyślną konfigurację i ją zapisuje
            AppConfiguration defaultConfig = new AppConfiguration
            {
                SelectedFolderPath = appDataFolder,
                IsAppStartChecked = false
            };

            try
            {
                string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(configFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy tworzeniu domyślnej konfiguracji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return defaultConfig;
        }

        // Zapisuje konfigurację użytkownika do pliku JSON
        public void SaveConfiguration(string selectedPathText, bool isAppStartCheckedBool)
        {
            try
            {
                AppConfiguration config = new AppConfiguration
                {
                    SelectedFolderPath = selectedPathText,
                    IsAppStartChecked = isAppStartCheckedBool
                };

                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
                string configFilePath = Path.Combine(appDataFolder, "config.json");

                if (!Directory.Exists(appDataFolder))
                {
                    Directory.CreateDirectory(appDataFolder);
                }

                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(configFilePath, json);

                if (isAppStartCheckedBool)
                {
                    StartupManager.CreateShortcutInStartup();
                }
                else
                {
                    StartupManager.RemoveShortcutFromStartup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
