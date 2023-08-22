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
        private readonly UserConfigurationManager _configManager;

        private readonly ITaskDAO _taskDAO;
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
        public Form1(UserConfigurationManager configManager, ITaskDAO taskDAO)
        {
            _configManager = configManager;
            _taskDAO = taskDAO;
            //MessageBox.Show("configManager: " + configManager.LoadConfiguration());
            AppConfiguration config = configManager.LoadConfiguration();

            InitializeComponent();

            //Zape³nia liczbami ComboBoxy dla wyboru godziny i minut
            for (int i = 0; i < 24; i++)
            {
                cmbHours.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
            {
                cmbMinutes.Items.Add(i);
            }

            cmbHours.Validating += cmbHours_Validating;
            cmbMinutes.Validating += cmbMinutes_Validating;

            LoadConfiguration(config);
            LoadTaskData();
            checkIfAppStartChecked();

        }

        //Wyœwietla komunikat je¿eli u¿ytkownik wprowadzi³ z³y format godziny oraz utrzymuje focus na polu
        private void cmbHours_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(cmbHours.Text, out int hour) || hour < 0 || hour > 23)
            {
                MessageBox.Show("WprowadŸ prawid³ow¹ godzinê (0-23).", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // Blokuje mo¿liwoœæ zmiany focusu z pola
            }
        }
        //Wyœwietla komunikat je¿eli u¿ytkownik wprowadzi³ z³y format minut oraz utrzymuje focus na polu
        private void cmbMinutes_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(cmbMinutes.Text, out int minute) || minute < 0 || minute > 59)
            {
                MessageBox.Show("WprowadŸ prawid³ow¹ minutê (0-59).", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // Blokuje mo¿liwoœæ zmiany focusu z pola
            }
        }


        //Sprawdza pola i dodaje nowe zadanie
        private void button1_Click(object sender, EventArgs e)
        {
            // Pobiera dane wejœciowe u¿ytkownika z kontrolek TextBox
            string eventName = txtEventName.Text;
            string targetApplication = txtTargetApplication.Text;
            DateTime selectedDate = dateTimePicker.Value;

            // Pobiera wybran¹ godzinê i minutê z ComboBox'ów
            int selectedHour = int.TryParse(cmbHours.Text, out int hour) ? hour : -1;
            int selectedMinute = int.TryParse(cmbMinutes.Text, out int minute) ? minute : -1;

            // Sprawdza poprawnoœæ pól wejœciowych
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(targetApplication))
            {
                MessageBox.Show("Proszê wype³niæ wszystkie wymagane pola.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sprawdza czy zosta³y wybrana godzina i minuty
            if (selectedHour == -1 || selectedMinute == -1)
            {
                MessageBox.Show("Wybierz prawid³ow¹ datê i godzinê.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // £¹czy datê i godzinê dla wygody porównywania z DateTime
            DateTime targetDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedHour, selectedMinute, 0);

            if (targetDateTime <= DateTime.Now)
            {
                MessageBox.Show("Wybierz przysz³¹ datê i godzinê.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sprawdza, czy wybrana œcie¿ka folderu jest dostêpna dla u¿ytkownika
            if (!FolderUtils.CanAccessFolder(selectedFolderPath))
            {
                MessageBox.Show("Brak uprawnieñ do zapisu w tym folderze. Uruchom aplikacjê z prawami administratora lub zmieñ folder zapisu.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddTask(eventName, targetApplication, targetDateTime, false, this.taskType);
        }

        //Dodaje nowe zadanie do listy zadañ
        public void AddTask(string eventName, string targetApplication, DateTime targetDateTime, bool eventHasPassed, TaskType type)
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
                    lblDynamic.Text = $"Zadanie: {eventName} nie uruchomi³o siê: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Usuñ";
                    btnDynamic.BackColor = Color.FromArgb(214, 144, 144);
                }
                else
                {
                    lblDynamic.ForeColor = Color.FromArgb(250, 250, 250);
                    lblDynamic.Text = $"Zadanie: {eventName} uruchomi siê: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Anuluj";

                    // Tworzy timer tylko je¿eli zadanie nie jest przedawnione
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


                // Dostosowuje uk³ad UI
                foreach (TaskControls taskControl in taskControlsList)
                {
                    taskControl.Label.Width = activeTasksPanel.Width - 120;
                    taskControl.Button.Left = taskControl.Label.Width + 20;
                }

                Top += 60;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ problem z zapisem danych zadania: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Anuluje lub usuwa dane zadanie
        private void btnDynamicTask_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            TaskControls taskToRemove = taskControlsList.Find(task => task.Button == clickedButton);

            if (taskToRemove != null)
            {
                if(taskToRemove.Timer != null)
                {
                    taskToRemove.Timer.Stop();
                    taskToRemove.Timer.Dispose();
                }

                taskControlsList.Remove(taskToRemove);
                activeTasksPanel.Controls.Remove(taskToRemove.Label);
                activeTasksPanel.Controls.Remove(taskToRemove.Button);
                Top -= 60;
                SaveTaskData();
            }
        }


        //Wykonuje siê, gdy up³ynie czas timera
        //TODO: do ewentualnego refactoru ze wzglêdu na powtórzenia kodu
        private void TaskTimer_Elapsed(TaskControls taskControls, object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                DateTime scheduledTime = GetScheduledTimeFromLabelText(taskControls.Label.Text);
                TaskType taskType = taskControls.TaskInfo.Type;

                if (scheduledTime <= currentTime)
                {
                    if (taskType == TaskType.OneTime)
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
                            MessageBox.Show("Nie uda³o siê uruchomiæ zaplanowanego procesu: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else if (taskType != TaskType.OneTime)
                    {
                        // Startuje proces zadania codziennego lub cotygodniowego
                    
                            try
                            {
                                string processPath = taskControls.TargetPath;
                                Process.Start(processPath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Nie uda³o siê uruchomiæ zaplanowanego procesu: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            // Aktualizacja dnia zadania codziennego
                            DateTime newScheduledTime;

                            if (taskType == TaskType.Daily)
                            {
                                newScheduledTime = scheduledTime.AddDays(1);
                            }
                            else if (taskType == TaskType.Weekly)
                            {
                                newScheduledTime = scheduledTime.AddDays(7);
                            }
                            else
                            {
                                return;
                            }


                            // Zmienia harmonogram istniej¹cego zadania
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
                                taskControls.Label.Text = $"Zadanie: {taskControls.TaskInfo.EventName} uruchomi siê: {newScheduledTime.ToString()}";
                                taskControls.Button.Text = "Anuluj";
                            }));

                            // Aktualizacja pliku json
                            SaveTaskData();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nieznany b³¹d: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Parsuje/wy³uskuje datê z tekstu
        private DateTime GetScheduledTimeFromLabelText(string labelText)
        {
            int startIndex = labelText.IndexOf("uruchomi siê: ") + "uruchomi siê: ".Length;

            if (startIndex < "uruchomi siê: ".Length)
            {
                startIndex = labelText.IndexOf("nie uruchomi³o siê: ") + "nie uruchomi³o siê: ".Length;
            }

            string timeString = labelText.Substring(startIndex);
            return DateTime.Parse(timeString);
        }

        //Ukrywa aplikacjê do zasobnika systemowego(system tray)
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Ukrywa formularz w zasobniku systemowym zamiast na pasku zadañ
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        //Pokazuje aplikacjê po dwukrotnym klikniêciu ikony w zasobniku systemowym
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // Zapisuje dane zadania do pliku JSON
        private void SaveTaskData()
        {
            List<TaskInfo> taskInfos = taskControlsList.Select(tc => tc.TaskInfo).ToList();
            _taskDAO.SaveTasks(taskInfos);
        }



        // £aduje dane zadañ z pliku JSON
        private void LoadTaskData()
        {
            List<TaskInfo> taskInfos = _taskDAO.LoadTaskData(this.selectedFolderPath);

            if (taskInfos != null && taskInfos.Count > 0)
            {
                foreach (var taskInfo in taskInfos)
                {
                    bool eventHasPassed = taskInfo.TargetDateTime <= DateTime.Now;

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
            else
            {
                return;
            }
        }

        // £aduje konfiguracjê/preferencje u¿ytkowników
        private void LoadConfiguration(AppConfiguration config)
        {
            try
            {
                if (config.SelectedFolderPath != null)
                {
                    selectedFolderPath = config.SelectedFolderPath;
                    isAppStartChecked = config.IsAppStartChecked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ b³¹d przy wczytywaniu konfiguracji: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Pokazuje modal z ustawieniami do wyboru/zmiany
        private void accessibilityBtn_Click(object sender, EventArgs e)
        {
            AccessibilityForm accessibilityForm = new AccessibilityForm(this, this._configManager);
            accessibilityForm.ShowDialog();
        }

        // Pozwala u¿ytkownikowi wybraæ folder i plik exe do uruchomienia
        private void changePathBtn_Click(object sender, EventArgs e)
        {
            // TODO: obserwowany jest nietypowy, 2-krotny, jednorazowy skok pamiêci po w³¹czeniu tej funkcji. 
            // Do przyjrzenia siê, potencjalny wyciek pamiêci lub nieporawne usuwanie z pamiêci po wybraniu folderu.
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