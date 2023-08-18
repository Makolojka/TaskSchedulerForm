using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TaskSchedulerForm
{
    public partial class Form1 : Form
    {
        private static List<TaskControls> taskControlsList = new List<TaskControls>();
        private string selectedFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
        private bool isAppStartChecked = true;
        private TaskType taskType = TaskType.OneTime;
        int Top = 50;
        public string SelectedFolderPath
        {
            get { return selectedFolderPath; }
            set { selectedFolderPath = value; }
        }

        public bool IsAppStartChecked
        {
            get { return isAppStartChecked; }
            set { isAppStartChecked = value; }
        }
        public Form1()
        {
            InitializeComponent();

            //Zape�nia liczbami ComboBoxy dla wyboru godziny i minut
            for (int i = 0; i < 24; i++)
            {
                cmbHours.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
            {
                cmbMinutes.Items.Add(i);
            }

            LoadConfiguration();
            LoadTaskData();
            checkIfAppStartChecked();

        }

        //Sprawdza pola i dodaje nowe zadanie
        private void button1_Click(object sender, EventArgs e)
        {
            // Pobiera dane wej�ciowe u�ytkownika z kontrolek TextBox
            string eventName = txtEventName.Text;
            string targetApplication = txtTargetApplication.Text;
            DateTime selectedDate = dateTimePicker.Value;

            // Pobiera wybran� godzin� i minut� z ComboBox'�w
            int selectedHour = cmbHours.SelectedItem != null ? (int)cmbHours.SelectedItem : -1;
            int selectedMinute = cmbMinutes.SelectedItem != null ? (int)cmbMinutes.SelectedItem : -1;

            // Sprawdza poprawno�� p�l wej�ciowych
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(targetApplication))
            {
                MessageBox.Show("Prosz� wype�ni� wszystkie wymagane pola.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sprawdza czy zosta�y wybrana godzina i minuty
            if (selectedHour == -1 || selectedMinute == -1)
            {
                MessageBox.Show("Wybierz prawid�ow� dat� i godzin�.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ��czy dat� i godzin� dla wygody por�wnywania z DateTime
            DateTime targetDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedHour, selectedMinute, 0);

            if (targetDateTime <= DateTime.Now)
            {
                MessageBox.Show("Wybierz przysz�� dat� i godzin�.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sprawdza, czy wybrana �cie�ka folderu jest dost�pna dla u�ytkownika
            if (!FolderUtils.CanAccessFolder(selectedFolderPath))
            {
                MessageBox.Show("Brak uprawnie� do zapisu w tym folderze. Uruchom aplikacj� z prawami administratora lub zmie� folder zapisu.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddTask(eventName, targetApplication, targetDateTime, false, this.taskType);
        }

        //Dodaje nowe zadanie do listy zada�
        private void AddTask(string eventName, string targetApplication, DateTime targetDateTime, bool eventHasPassed, TaskType type)
        {
            try
            {
                Label lblDynamic = new Label();
                lblDynamic.BackColor = Color.FromArgb(72, 78, 82);
                lblDynamic.Padding = new Padding(10, 5, 10, 5);
                lblDynamic.Margin = new Padding(5, 10, 5, 10);
                lblDynamic.MinimumSize = new Size(650, 45);
                lblDynamic.MaximumSize = new Size(650, 45);
                lblDynamic.AutoSize = true;

                Button btnDynamic = new Button();
                btnDynamic.Size = new Size(100, 45);
                btnDynamic.Margin = new Padding(5, 10, 5, 10);
                btnDynamic.BackColor = Color.FromArgb(72, 78, 82);
                btnDynamic.ForeColor = Color.White;
                btnDynamic.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnDynamic.FlatStyle = FlatStyle.Flat;
                btnDynamic.FlatAppearance.BorderSize = 0;
                btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                btnDynamic.Click += btnDynamicTask_Click;

                TaskInfo taskInfo = new TaskInfo
                {
                    EventName = eventName,
                    TargetApplication = targetApplication,
                    TargetDateTime = targetDateTime,
                    Type = type
                };

                TaskControls taskControls = new TaskControls
                {
                    Label = lblDynamic,
                    Button = btnDynamic,
                    TargetPath = targetApplication,
                    TaskInfo = taskInfo
                };

                if (eventHasPassed)
                {
                    lblDynamic.BackColor = Color.FromArgb(214, 144, 144);
                    lblDynamic.ForeColor = Color.White;
                    lblDynamic.Text = $"Zadanie: {eventName} nie uruchomi�o si�: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Usu�";
                    btnDynamic.BackColor = Color.FromArgb(214, 144, 144);
                }
                else
                {
                    lblDynamic.ForeColor = Color.FromArgb(250, 250, 250);
                    lblDynamic.Text = $"Zadanie: {eventName} uruchomi si�: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Anuluj";

                    // Tworzy timer tylko je�eli zadanie nie jest przedawnione
                    taskControls.Timer = new System.Timers.Timer()
                    {
                        Interval = (targetDateTime - DateTime.Now).TotalMilliseconds
                    };
                    taskControls.Timer.Elapsed += (s, ev) => TaskTimer_Elapsed(taskControls, s, ev);
                    taskControls.Timer.Start();
                }

                taskControlsList.Add(taskControls);

                activeTasksPanel.Controls.Add(lblDynamic);
                activeTasksPanel.Controls.Add(btnDynamic);

                SaveTaskData();


                // Dostosowuje uk�ad UI
                foreach (TaskControls taskControl in taskControlsList)
                {
                    taskControl.Label.Width = activeTasksPanel.Width - 120;
                    taskControl.Button.Left = taskControl.Label.Width + 20;
                }

                Top += 60;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� problem z zapisem danych zadania: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Anuluje lub usuwa dane zadanie
        private void btnDynamicTask_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            TaskControls taskToRemove = taskControlsList.Find(task => task.Button == clickedButton);

            if (taskToRemove != null)
            {
                taskToRemove.Timer.Stop();
                taskToRemove.Timer.Dispose();

                taskControlsList.Remove(taskToRemove);
                activeTasksPanel.Controls.Remove(taskToRemove.Label);
                activeTasksPanel.Controls.Remove(taskToRemove.Button);
                Top -= 60;
                SaveTaskData();
            }
        }


        //Wykonuje si�, gdy up�ynie czas timera
        private void TaskTimer_Elapsed(TaskControls taskControls, object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DateTime scheduledTime = GetScheduledTimeFromLabelText(taskControls.Label.Text);
            TaskType taskType = taskControls.TaskInfo.Type;
            MessageBox.Show("Type zadania:"+taskControls.TaskInfo.Type);
            if (taskType == TaskType.OneTime)
            {
                if (scheduledTime <= currentTime)
                {
                    taskControls.Timer.Stop();
                    taskControls.Timer.Dispose();

                    // Startuje proces jednorazowy
                    try
                    {
                        string processPath = taskControls.TargetPath;
                        Process.Start(processPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Nie uda�o si� uruchomi� zaplanowanego procesu: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Usuwa zadanie
                    taskControlsList.Remove(taskControls);
                    activeTasksPanel.Invoke(new Action(() =>
                    {
                        activeTasksPanel.Controls.Remove(taskControls.Label);
                        activeTasksPanel.Controls.Remove(taskControls.Button);
                        Top -= 60;
                    }));

                    // Aktualizacja pliku json
                    SaveTaskData();
                }
            }
            else if (taskType == TaskType.Daily)
            {
                if (scheduledTime.Date == currentTime.Date && scheduledTime.Hour == currentTime.Hour && scheduledTime.Minute == currentTime.Minute)
                {
                    // Startuje proces zadania codziennego
                    try
                    {
                        try
                        {
                            string processPath = taskControls.TargetPath;
                            Process.Start(processPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Nie uda�o si� uruchomi� zaplanowanego procesu: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Aktualizacja dnia zadania codziennego
                        DateTime newScheduledTime = scheduledTime.AddDays(1);

                        // Zmienia harmonogram istniej�cego zadania
                        taskControls.TaskInfo.TargetDateTime = newScheduledTime;
                        taskControls.Timer.Stop();
                        taskControls.Timer.Dispose();
                        taskControls.Timer = new System.Timers.Timer
                        {
                            Interval = (newScheduledTime - DateTime.Now).TotalMilliseconds,
                            AutoReset = false
                        };
                        taskControls.Timer.Elapsed += (s, ev) => TaskTimer_Elapsed(taskControls, s, ev);
                        taskControls.Timer.Start();

                        // Aktualizacja UI
                        activeTasksPanel.Invoke(new Action(() =>
                        {
                            taskControls.Label.Text = $"Zadanie: {taskControls.TaskInfo.EventName} uruchomi si�: {newScheduledTime.ToString()}";
                            taskControls.Button.Text = "Anuluj";
                        }));

                        // Aktualizacja pliku json
                        SaveTaskData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Nieznany b��d: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (taskType == TaskType.Weekly)
            {
                if (scheduledTime.Date <= currentTime.Date)
                {
                    // Startuje proces zadania cotygodniowego
                    try
                    {
                        try
                        {
                            string processPath = taskControls.TargetPath;
                            Process.Start(processPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Nie uda�o si� uruchomi� zaplanowanego procesu: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Aktualizacja dnia zadania cotygodniowego
                        DateTime newScheduledTime = scheduledTime.AddDays(7);

                        // Zmienia harmonogram istniej�cego zadania
                        taskControls.TaskInfo.TargetDateTime = newScheduledTime;
                        taskControls.Timer.Stop();
                        taskControls.Timer.Dispose();
                        taskControls.Timer = new System.Timers.Timer
                        {
                            Interval = (newScheduledTime - DateTime.Now).TotalMilliseconds,
                            AutoReset = false
                        };
                        taskControls.Timer.Elapsed += (s, ev) => TaskTimer_Elapsed(taskControls, s, ev);
                        taskControls.Timer.Start();

                        // Aktualizacja UI
                        activeTasksPanel.Invoke(new Action(() =>
                        {
                            taskControls.Label.Text = $"Zadanie: {taskControls.TaskInfo.EventName} uruchomi si�: {newScheduledTime.ToString()}";
                            taskControls.Button.Text = "Anuluj";
                        }));

                        // Aktualizacja pliku json
                        SaveTaskData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Nieznany b��d: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Parsuje/wy�uskuje dat� z tekstu
        private DateTime GetScheduledTimeFromLabelText(string labelText)
        {
            int startIndex = labelText.IndexOf("uruchomi si�: ") + "uruchomi si�: ".Length;

            if (startIndex < "uruchomi si�: ".Length)
            {
                startIndex = labelText.IndexOf("nie uruchomi�o si�: ") + "nie uruchomi�o si�: ".Length;
            }

            string timeString = labelText.Substring(startIndex);
            return DateTime.Parse(timeString);
        }

        //Ukrywa aplikacj� do zasobnika systemowego(system tray)
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Ukrywa formularz w zasobniku systemowym zamiast na pasku zada�
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        //Pokazuje aplikacj� po dwukrotnym klikni�ciu ikony w zasobniku systemowym
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // Zapisuje dane zadania do pliku JSON
        private void SaveTaskData()
        {
            try
            {
                List<TaskInfo> taskInfos = taskControlsList.Select(tc => tc.TaskInfo).ToList();
                string json = JsonSerializer.Serialize(taskInfos, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                string jsonFileName = "taskData.json";
                string jsonFilePath = Path.Combine(selectedFolderPath, jsonFileName);

                if (!FolderUtils.CanAccessFolder(selectedFolderPath))
                {
                    MessageBox.Show("Brak uprawnie� do zapisu w tym folderze. Uruchom aplikacj� z prawami administratora lub zmie� folder zapisu.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� nieznany problem z zapisem danych zadania: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // �aduje dane zada� z pliku JSON
        private void LoadTaskData()
        {
            try
            {
                string jsonFileName = "taskData.json";
                string jsonFilePath = Path.Combine(selectedFolderPath, jsonFileName);

                //Sprawdza czy folder istnieje, je�eli nie istnieje tworzy go.
                if (!Directory.Exists(selectedFolderPath))
                {
                    Directory.CreateDirectory(selectedFolderPath);
                }

                // Sprawdza, czy wybrana �cie�ka folderu jest dost�pna dla u�ytkownika
                if (!FolderUtils.CanAccessFolder(selectedFolderPath))
                {
                    MessageBox.Show("Brak uprawnie� do zapisu w tym folderze. Uruchom aplikacj� z prawami administratora lub zmie� folder zapisu.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Je�eli plik z zadaniami istnieje zczytuje dane
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    List<TaskInfo> taskInfos = JsonSerializer.Deserialize<List<TaskInfo>>(json);

                    if (taskInfos != null)
                    {
                        foreach (var taskInfo in taskInfos)
                        {
                            bool eventHasPassed = taskInfo.TargetDateTime <= DateTime.Now;

                            // Dodatkowe sprawdzenie czy zadanie si� przedawni�o i jest typu Daily
                            if (eventHasPassed && taskInfo.Type == TaskType.Daily)
                            {
                                eventHasPassed = false;

                                DateTime newScheduledTime = taskInfo.TargetDateTime.AddDays(1);
                                AddTask(taskInfo.EventName, taskInfo.TargetApplication, newScheduledTime, eventHasPassed, taskInfo.Type);
                            }
                            else if (eventHasPassed && taskInfo.Type == TaskType.Weekly)
                            {
                                eventHasPassed = false;

                                DateTime newScheduledTime = taskInfo.TargetDateTime.AddDays(7);
                                AddTask(taskInfo.EventName, taskInfo.TargetApplication, newScheduledTime, eventHasPassed, taskInfo.Type);
                            }
                            else
                            {
                                AddTask(taskInfo.EventName, taskInfo.TargetApplication, taskInfo.TargetDateTime, eventHasPassed, taskInfo.Type);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� problem ze wczytaniem danych zadania: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // �aduje konfiguracj�/preferencje u�ytkownik�w
        private void LoadConfiguration()
        {
            try
            {
                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
                string configFilePath = Path.Combine(appDataFolder, "config.json");

                //Je�eli plik z konfiguracj� istnieje zczytuje dane
                if (File.Exists(configFilePath))
                {
                    string json = File.ReadAllText(configFilePath);
                    AppConfiguration config = JsonSerializer.Deserialize<AppConfiguration>(json);

                    if (config.SelectedFolderPath != null)
                    {
                        selectedFolderPath = config.SelectedFolderPath;
                        isAppStartChecked = config.IsAppStartChecked;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� b��d przy wczytywaniu konfiguracji: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Pokazuje modal z ustawieniami do wyboru/zmiany
        private void accessibilityBtn_Click(object sender, EventArgs e)
        {
            AccessibilityForm accessibilityForm = new AccessibilityForm(this);
            accessibilityForm.ShowDialog();
        }

        // Pozwala u�ytkownikowi wybra� folder i plik exe do uruchomienia
        private void changePathBtn_Click(object sender, EventArgs e)
        {
            // TODO: obserwowany jest nietypowy, 2-krotny, jednorazowy skok pami�ci po w��czeniu tej funkcji. 
            // Do przyjrzenia si�, potencjalny wyciek pami�ci lub nieporawne usuwanie z pami�ci po wybraniu folderu.
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.Multiselect = false;

                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    txtTargetApplication.Text = selectedFilePath;
                }
            }
        }

        private void checkIfAppStartChecked()
        {
            if (isAppStartChecked)
            {
                StartupManager.CreateShortcutInStartup();
            }
            else
            {
                StartupManager.RemoveShortcutFromStartup();
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.taskType = TaskType.OneTime;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.taskType = TaskType.Daily;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.taskType = TaskType.Weekly;
            }
        }
    }
}