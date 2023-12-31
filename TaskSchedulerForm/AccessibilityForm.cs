﻿using System;
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
        private readonly UserConfigurationManager _configManager;
        public AccessibilityForm(Form1 form1Instance, UserConfigurationManager _configManager)
        {
            this._configManager = _configManager;
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

        // Umożliwia wybór folderu
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

        // Zapisuje konfigurację użytkownika
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

                _configManager.SaveConfiguration(textBox1.Text, this.isAppStartChecked);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
