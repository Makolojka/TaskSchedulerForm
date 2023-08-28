using System.Windows.Forms;

namespace TaskSchedulerForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnSchedule = new Button();
            label1 = new Label();
            eventCreatorGroup = new GroupBox();
            panel2 = new Panel();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label7 = new Label();
            changePathBtn = new Button();
            dotSeparator = new Label();
            cmbMinutes = new ComboBox();
            label5 = new Label();
            cmbHours = new ComboBox();
            dateTimePicker = new DateTimePicker();
            label3 = new Label();
            txtTargetApplication = new TextBox();
            txtEventName = new TextBox();
            label2 = new Label();
            label4 = new Label();
            activeTasks = new GroupBox();
            activeTasksPanel = new FlowLayoutPanel();
            notifyIcon1 = new NotifyIcon(components);
            panel1 = new Panel();
            accessibilityBtn = new Button();
            label6 = new Label();
            eventCreatorGroup.SuspendLayout();
            panel2.SuspendLayout();
            activeTasks.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSchedule
            // 
            btnSchedule.BackColor = Color.FromArgb(241, 100, 0);
            btnSchedule.FlatAppearance.BorderSize = 0;
            btnSchedule.FlatStyle = FlatStyle.Flat;
            btnSchedule.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSchedule.ForeColor = Color.White;
            btnSchedule.Location = new Point(36, 620);
            btnSchedule.Margin = new Padding(4);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(420, 55);
            btnSchedule.TabIndex = 0;
            btnSchedule.Text = "Utwórz zadanie";
            btnSchedule.UseVisualStyleBackColor = false;
            btnSchedule.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 148);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(181, 28);
            label1.TabIndex = 1;
            label1.Text = "Ścieżka do aplikacji";
            // 
            // eventCreatorGroup
            // 
            eventCreatorGroup.Controls.Add(panel2);
            eventCreatorGroup.Controls.Add(label7);
            eventCreatorGroup.Controls.Add(changePathBtn);
            eventCreatorGroup.Controls.Add(dotSeparator);
            eventCreatorGroup.Controls.Add(cmbMinutes);
            eventCreatorGroup.Controls.Add(label5);
            eventCreatorGroup.Controls.Add(cmbHours);
            eventCreatorGroup.Controls.Add(dateTimePicker);
            eventCreatorGroup.Controls.Add(btnSchedule);
            eventCreatorGroup.Controls.Add(label3);
            eventCreatorGroup.Controls.Add(txtTargetApplication);
            eventCreatorGroup.Controls.Add(txtEventName);
            eventCreatorGroup.Controls.Add(label1);
            eventCreatorGroup.Controls.Add(label2);
            eventCreatorGroup.Location = new Point(33, 115);
            eventCreatorGroup.Margin = new Padding(4);
            eventCreatorGroup.Name = "eventCreatorGroup";
            eventCreatorGroup.Padding = new Padding(4);
            eventCreatorGroup.Size = new Size(494, 692);
            eventCreatorGroup.TabIndex = 2;
            eventCreatorGroup.TabStop = false;
            eventCreatorGroup.Text = "Utwórz zadanie";
            // 
            // panel2
            // 
            panel2.Controls.Add(radioButton3);
            panel2.Controls.Add(radioButton2);
            panel2.Controls.Add(radioButton1);
            panel2.Location = new Point(36, 493);
            panel2.Name = "panel2";
            panel2.Size = new Size(420, 120);
            panel2.TabIndex = 13;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(0, 79);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(126, 32);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "Co tydzień";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(0, 41);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(130, 32);
            radioButton2.TabIndex = 1;
            radioButton2.Text = "Codziennie";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(0, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(114, 32);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Jeden raz";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 462);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(327, 28);
            label7.TabIndex = 12;
            label7.Text = "Kiedy ma być uruchamiane zadanie?";
            // 
            // changePathBtn
            // 
            changePathBtn.BackColor = Color.FromArgb(241, 100, 0);
            changePathBtn.FlatAppearance.BorderSize = 0;
            changePathBtn.FlatStyle = FlatStyle.Flat;
            changePathBtn.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            changePathBtn.ForeColor = Color.White;
            changePathBtn.Location = new Point(394, 180);
            changePathBtn.Margin = new Padding(4);
            changePathBtn.Name = "changePathBtn";
            changePathBtn.Size = new Size(75, 34);
            changePathBtn.TabIndex = 11;
            changePathBtn.Text = "Wybierz";
            changePathBtn.UseVisualStyleBackColor = false;
            changePathBtn.Click += changePathBtn_Click;
            // 
            // dotSeparator
            // 
            dotSeparator.Location = new Point(90, 385);
            dotSeparator.Name = "dotSeparator";
            dotSeparator.Size = new Size(16, 36);
            dotSeparator.TabIndex = 9;
            dotSeparator.Text = ":";
            // 
            // cmbMinutes
            // 
            cmbMinutes.FormattingEnabled = true;
            cmbMinutes.Location = new Point(113, 385);
            cmbMinutes.Margin = new Padding(4);
            cmbMinutes.Name = "cmbMinutes";
            cmbMinutes.Size = new Size(60, 36);
            cmbMinutes.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 353);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(154, 28);
            label5.TabIndex = 7;
            label5.Text = "Godzina/minuty";
            // 
            // cmbHours
            // 
            cmbHours.FormattingEnabled = true;
            cmbHours.Location = new Point(23, 385);
            cmbHours.Margin = new Padding(4);
            cmbHours.Name = "cmbHours";
            cmbHours.Size = new Size(60, 36);
            cmbHours.TabIndex = 5;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(23, 283);
            dateTimePicker.Margin = new Padding(4);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(446, 34);
            dateTimePicker.TabIndex = 6;
            dateTimePicker.Value = new DateTime(2023, 8, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 251);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(177, 28);
            label3.TabIndex = 5;
            label3.Text = "Data uruchomienia";
            // 
            // txtTargetApplication
            // 
            txtTargetApplication.Location = new Point(23, 180);
            txtTargetApplication.Margin = new Padding(4);
            txtTargetApplication.Name = "txtTargetApplication";
            txtTargetApplication.Size = new Size(363, 34);
            txtTargetApplication.TabIndex = 3;
            // 
            // txtEventName
            // 
            txtEventName.BackColor = SystemColors.Window;
            txtEventName.ForeColor = SystemColors.WindowText;
            txtEventName.Location = new Point(23, 80);
            txtEventName.Margin = new Padding(4);
            txtEventName.MaxLength = 14;
            txtEventName.Name = "txtEventName";
            txtEventName.Size = new Size(446, 34);
            txtEventName.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 48);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(142, 28);
            label2.TabIndex = 0;
            label2.Text = "Nazwa zadania";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(33, 58);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(161, 32);
            label4.TabIndex = 3;
            label4.Text = "Kreator zadań";
            // 
            // activeTasks
            // 
            activeTasks.Controls.Add(activeTasksPanel);
            activeTasks.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            activeTasks.Location = new Point(565, 115);
            activeTasks.Margin = new Padding(4);
            activeTasks.Name = "activeTasks";
            activeTasks.Padding = new Padding(4);
            activeTasks.Size = new Size(804, 692);
            activeTasks.TabIndex = 4;
            activeTasks.TabStop = false;
            activeTasks.Text = "Aktywne zadania";
            // 
            // activeTasksPanel
            // 
            activeTasksPanel.AutoScroll = true;
            activeTasksPanel.Location = new Point(8, 40);
            activeTasksPanel.Margin = new Padding(4);
            activeTasksPanel.Name = "activeTasksPanel";
            activeTasksPanel.Size = new Size(788, 635);
            activeTasksPanel.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipText = "Aplikacja działa w tle.";
            notifyIcon1.BalloonTipTitle = "Powiadomienie";
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Harmonogram MK";
            notifyIcon1.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(241, 100, 0);
            panel1.Controls.Add(accessibilityBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1382, 40);
            panel1.TabIndex = 5;
            // 
            // accessibilityBtn
            // 
            accessibilityBtn.BackColor = Color.FromArgb(241, 100, 0);
            accessibilityBtn.FlatAppearance.BorderSize = 0;
            accessibilityBtn.FlatStyle = FlatStyle.Flat;
            accessibilityBtn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            accessibilityBtn.ForeColor = Color.White;
            accessibilityBtn.Location = new Point(0, 0);
            accessibilityBtn.Margin = new Padding(5, 10, 5, 10);
            accessibilityBtn.Name = "accessibilityBtn";
            accessibilityBtn.Size = new Size(116, 40);
            accessibilityBtn.TabIndex = 0;
            accessibilityBtn.Text = "Ustawienia";
            accessibilityBtn.UseVisualStyleBackColor = false;
            accessibilityBtn.Click += accessibilityBtn_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(565, 58);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(170, 32);
            label6.TabIndex = 6;
            label6.Text = "Podgląd zadań";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1382, 853);
            Controls.Add(label6);
            Controls.Add(panel1);
            Controls.Add(activeTasks);
            Controls.Add(label4);
            Controls.Add(eventCreatorGroup);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(1400, 900);
            Name = "Form1";
            Text = "Harmonogram zadań";
            SizeChanged += Form1_SizeChanged;
            eventCreatorGroup.ResumeLayout(false);
            eventCreatorGroup.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            activeTasks.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSchedule;
        private Label label1;
        private GroupBox eventCreatorGroup;
        private Label label2;
        private TextBox txtEventName;
        private Label label3;
        private TextBox txtTargetApplication;
        private DateTimePicker dateTimePicker;
        private Label label4;
        private GroupBox activeTasks;
        private ComboBox cmbHours;
        private Label label5;
        private ComboBox cmbMinutes;
        private FlowLayoutPanel activeTasksPanel;
        private Label dotSeparator;
        private NotifyIcon notifyIcon1;

        private Panel panel1;
        private Button accessibilityBtn;
        private Button changePathBtn;
        private Label label6;
        private Label label7;
        private Panel panel2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private RadioButton radioButton3;
    }
}