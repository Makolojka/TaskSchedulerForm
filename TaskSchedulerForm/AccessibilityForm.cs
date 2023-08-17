using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TaskSchedulerForm
{
    public partial class AccessibilityForm : Form
    {

        private bool isAppStartChecked;
        private Form1 mainForm;
        private string selectedFolderPath;
        public AccessibilityForm(Form1 form1Instance)
        {
            InitializeComponent();
            mainForm = form1Instance;
            selectedFolderPath = mainForm.SelectedFolderPath;
            isAppStartChecked = mainForm.IsAppStartChecked;
            textBox1.Text = selectedFolderPath;

            if (isAppStartChecked)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.isAppStartChecked = true;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.isAppStartChecked = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void changePathBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    if (FolderUtils.CanAccessFolder(selectedFolder))
                    {
                        textBox1.Text = selectedFolder;
                    }
                    else
                    {
                        MessageBox.Show("Brak uprawnień do zapisu w tym folderze.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            this.Close();
        }

        private void SaveConfiguration()
        {
            try
            {
                mainForm.SelectedFolderPath = textBox1.Text;
                if (isAppStartChecked)
                {
                    mainForm.IsAppStartChecked = true;
                }
                else
                {
                    mainForm.IsAppStartChecked = false;
                }

                AppConfiguration config = new AppConfiguration
                {
                    SelectedFolderPath = textBox1.Text,
                    IsAppStartChecked = isAppStartChecked
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
