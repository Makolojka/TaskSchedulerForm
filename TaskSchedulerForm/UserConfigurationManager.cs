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
            try
            {
                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
                string configFilePath = Path.Combine(appDataFolder, "config.json");

                if (File.Exists(configFilePath))
                {
                    string json = File.ReadAllText(configFilePath);
                    AppConfiguration config = JsonSerializer.Deserialize<AppConfiguration>(json);
                    return config;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy wczytywaniu konfiguracji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
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
