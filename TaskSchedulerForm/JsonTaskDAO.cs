using System;
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
        private Form1 mainForm;

        public JsonTaskDAO(string jsonFilePath, Form1 form1Instance)
        {
            _jsonFilePath = jsonFilePath;
            mainForm = form1Instance;
        }

        public void SaveTasks(List<TaskInfo> taskInfos)
        {
            try
            {
                string json = JsonSerializer.Serialize(taskInfos, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                if (!FolderUtils.CanAccessFolder(Path.GetDirectoryName(_jsonFilePath)))
                {
                    MessageBox.Show("Brak uprawnień do zapisu w tym folderze. Uruchom aplikację z prawami administratora lub zmień folder zapisu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllText(_jsonFilePath, json);
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

                //Sprawdza czy folder istnieje, jeżeli nie istnieje tworzy go.
                if (!Directory.Exists(selectedFolderPath))
                {
                    Directory.CreateDirectory(selectedFolderPath);
                    throw new Exception();
                }

                // Sprawdza, czy wybrana ścieżka folderu jest dostępna dla użytkownika
                if (!FolderUtils.CanAccessFolder(selectedFolderPath))
                {
                    MessageBox.Show("Brak uprawnień do zapisu w tym folderze. Uruchom aplikację z prawami administratora lub zmień folder zapisu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception();
                }

                //Jeżeli plik z zadaniami istnieje zczytuje dane
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    List<TaskInfo> taskInfos = JsonSerializer.Deserialize<List<TaskInfo>>(json);

                    if (taskInfos != null)
                    {
                        return taskInfos;
                    }
                    //Zabezpieczyć i żeby zwracało poprawnie
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił problem ze wczytaniem danych zadania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
