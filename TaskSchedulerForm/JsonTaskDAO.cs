using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    public class JsonTaskDAO : ITaskDAO
    {
        private readonly string _jsonFilePath;
        public JsonTaskDAO(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public void SaveTasks(List<TaskInfo> taskInfos)
        {
            try
            {
                string json = JsonSerializer.Serialize(taskInfos, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                string jsonFilePath = Path.Combine(_jsonFilePath, "taskData.json");

                string directoryPath = Path.GetDirectoryName(jsonFilePath);

                // Sprawdź, czy katalog jest dostępny
                if (PermissionCheck(directoryPath))
                {
                    // Jeśli folder nie istnieje, spróbuj go utworzyć
                    if (!Directory.Exists(directoryPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Nie można utworzyć folderu. Błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // Zapisz dane do pliku JSON
                    File.WriteAllText(jsonFilePath, json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił nieznany problem z zapisem danych zadania: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<TaskInfo> LoadTaskData(string selectedFolderPath)
        {
            try
            {
                string jsonFileName = "taskData.json";
                string jsonFilePath = Path.Combine(selectedFolderPath, jsonFileName);

                if (PermissionCheck(selectedFolderPath))
                {
                    //Jeżeli plik z zadaniami istnieje zczytuje dane
                    if (File.Exists(jsonFilePath))
                    {
                        string json = File.ReadAllText(jsonFilePath);
                        List<TaskInfo> taskInfos = JsonSerializer.Deserialize<List<TaskInfo>>(json);

                        if (taskInfos != null)
                        {
                            return taskInfos;
                        }
                    }
                }
                else
                {
                    return new List<TaskInfo>(); // Zwróć pustą listę, gdy sprawdzanie uprawnień nie powiedzie się
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił problem ze wczytaniem danych zadania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new List<TaskInfo>(); // Zwróć pustą listę, gdy pjawi się wyjątek
        }

        public bool PermissionCheck(string selectedFolderPath)
        {
            //Sprawdza czy folder istnieje, jeżeli nie istnieje tworzy go.
            if (!Directory.Exists(selectedFolderPath))
            {
                Directory.CreateDirectory(selectedFolderPath);
                return true;
            }

            // Sprawdza, czy wybrana ścieżka folderu jest dostępna dla użytkownika
            if (!FolderUtils.CanAccessFolder(selectedFolderPath))
            {
                MessageBox.Show("Brak uprawnień do zapisu w tym folderze. Uruchom aplikację z prawami administratora lub zmień folder zapisu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
