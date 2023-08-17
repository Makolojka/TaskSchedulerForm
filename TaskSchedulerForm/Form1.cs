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

namespace TaskSchedulerForm
{
    public partial class Form1 : Form
    {
        private static List<TaskControls> taskControlsList = new List<TaskControls>();
        private string selectedFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");

        private bool isAppStartChecked = true;
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

        }

        //Validates field and adds new task
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the user input from the TextBox controls
            string eventName = txtEventName.Text;
            string targetApplication = txtTargetApplication.Text;
            DateTime selectedDate = dateTimePicker.Value;

            // Get the selected hour and minute from the ComboBoxes
            int selectedHour = cmbHours.SelectedItem != null ? (int)cmbHours.SelectedItem : -1;
            int selectedMinute = cmbMinutes.SelectedItem != null ? (int)cmbMinutes.SelectedItem : -1;

            // Validate input fields
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(targetApplication))
            {
                MessageBox.Show("Proszê wype³niæ wszystkie wymagane pola.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the date and time are selected
            if (selectedHour == -1 || selectedMinute == -1)
            {
                MessageBox.Show("Wybierz prawid³ow¹ datê i godzinê.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Combine date and time
            DateTime targetDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedHour, selectedMinute, 0);

            if (targetDateTime <= DateTime.Now)
            {
                MessageBox.Show("Wybierz przysz³¹ datê i godzinê.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the selectedFolderPath is accessible
            if (!FolderUtils.CanAccessFolder(selectedFolderPath))
            {
                MessageBox.Show("Brak uprawnieñ do zapisu w tym folderze. Uruchom aplikacjê z prawami administratora lub zmieñ folder zapisu.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddTask(eventName, targetApplication, targetDateTime, false);
        }

        //Adds new task to the tasks list
        private void AddTask(string eventName, string targetApplication, DateTime targetDateTime, bool eventHasPassed)
        {
            try
            {
                Label lblDynamic = new Label();
                lblDynamic.BackColor = Color.FromArgb(240, 240, 240);
                //lblDynamic.ForeColor = Color.FromArgb(51, 51, 51);
                lblDynamic.Padding = new Padding(10, 5, 10, 5);
                lblDynamic.Margin = new Padding(5, 10, 5, 10);
                lblDynamic.MinimumSize = new Size(650, 45);
                lblDynamic.MaximumSize = new Size(650, 45);
                lblDynamic.AutoSize = true;
                //lblDynamic.Text = "Zadanie: " + eventName + " uruchomi siê:  " + targetDateTime.ToString();

                Button btnDynamic = new Button();
                btnDynamic.Size = new Size(100, 45);
                btnDynamic.Margin = new Padding(5, 10, 5, 10);
                btnDynamic.BackColor = Color.FromArgb(231, 76, 60);
                btnDynamic.ForeColor = Color.White;
                btnDynamic.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                //btnDynamic.Text = "Anuluj";
                btnDynamic.FlatStyle = FlatStyle.Flat;
                btnDynamic.FlatAppearance.BorderSize = 0;
                btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                btnDynamic.Click += btnDynamicTask_Click;

                if (eventHasPassed)
                {
                    lblDynamic.ForeColor = Color.Gray;
                    lblDynamic.Text = $"Zadanie: {eventName} nie uruchomi³o siê: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Usuñ";

                }
                else
                {
                    lblDynamic.ForeColor = Color.FromArgb(51, 51, 51);
                    lblDynamic.Text = $"Zadanie: {eventName} uruchomi siê: {targetDateTime.ToString()}";
                    btnDynamic.Text = "Anuluj";
                }

                TaskInfo taskInfo = new TaskInfo
                {
                    EventName = eventName,
                    TargetApplication = targetApplication,
                    TargetDateTime = targetDateTime
                };

                TaskControls taskControls = new TaskControls
                {
                    Label = lblDynamic,
                    Button = btnDynamic,
                    Timer = new System.Timers.Timer()
                    {
                        Interval = 5000,
                    },
                    TargetPath = targetApplication,
                    TaskInfo = taskInfo
                };
                taskControls.Timer.Elapsed += (s, ev) => TaskTimer_Elapsed(taskControls, s, ev);
                taskControls.Timer.Start();

                taskControlsList.Add(taskControls);

                activeTasksPanel.Controls.Add(lblDynamic);
                activeTasksPanel.Controls.Add(btnDynamic);

                SaveTaskData();

                // Adjust the layout
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

        //Cancel or remove given task
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


        //Executes when timer elapse
        private void TaskTimer_Elapsed(TaskControls taskControls, object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DateTime scheduledTime = GetScheduledTimeFromLabelText(taskControls.Label.Text);

            if (scheduledTime.Date == currentTime.Date && scheduledTime.Hour == currentTime.Hour && scheduledTime.Minute == currentTime.Minute)
            {
                // Stop the task's timer
                taskControls.Timer.Stop();

                // Start a process
                try
                {
                    string processPath = taskControls.TargetPath;
                    Process.Start(processPath);
                    //MessageBox.Show("Uda³o siê uruchomiæ zaplanowany proces");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie uda³o siê uruchomiæ zaplanowanego procesu: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //MessageBox.Show("Proces" + taskControls.Label.Text + " wystartowa³", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Remove the task
                taskControlsList.Remove(taskControls);
                activeTasksPanel.Invoke(new Action(() =>
                {
                    activeTasksPanel.Controls.Remove(taskControls.Label);
                    activeTasksPanel.Controls.Remove(taskControls.Button);
                    Top -= 60;
                }));
                SaveTaskData();
            }
        }

        //Date parser
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

        //Hides form into system tray when minimized
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Hides form to system tray instead of taskbar
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        //Show form when double clicked on icon in system tray
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // Save task data to a JSON file
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

                // Check if the selectedFolderPath is accessible
                if (!FolderUtils.CanAccessFolder(selectedFolderPath))
                {
                    MessageBox.Show("Brak uprawnieñ do zapisu w tym folderze. Uruchom aplikacjê z prawami administratora lub zmieñ folder zapisu.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ nieznany problem z zapisem danych zadania: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   

        // Load task data from a JSON file
        private void LoadTaskData()
        {
            try
            {
                string jsonFileName = "taskData.json";
                string jsonFilePath = Path.Combine(selectedFolderPath, jsonFileName);
                if (!Directory.Exists(selectedFolderPath))
                {
                    Directory.CreateDirectory(selectedFolderPath);
                }
                    // Check if the selectedFolderPath is accessible
                    if (!FolderUtils.CanAccessFolder(selectedFolderPath))
                {
                    MessageBox.Show("Brak uprawnieñ do zapisu w tym folderze. Uruchom aplikacjê z prawami administratora lub zmieñ folder zapisu.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    List<TaskInfo> taskInfos = JsonSerializer.Deserialize<List<TaskInfo>>(json);

                    if (taskInfos != null)
                    {
                        foreach (var taskInfo in taskInfos)
                        {
                            bool eventHasPassed = taskInfo.TargetDateTime <= DateTime.Now;
                            AddTask(taskInfo.EventName, taskInfo.TargetApplication, taskInfo.TargetDateTime, eventHasPassed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ problem ze wczytaniem danych zadania: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Loads users configuration/preferences
        private void LoadConfiguration()
        {
            try
            {
                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarmonogramMK");
                string configFilePath = Path.Combine(appDataFolder, "config.json");

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
                MessageBox.Show($"Wyst¹pi³ b³¹d przy wczytywaniu konfiguracji: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Shows settings form
        private void accessibilityBtn_Click(object sender, EventArgs e)
        {
            AccessibilityForm accessibilityForm = new AccessibilityForm(this);
            accessibilityForm.ShowDialog();
        }

        //Allows user to choose folder and exe files
        private void changePathBtn_Click(object sender, EventArgs e)
        {
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
    }
}