namespace TaskSchedulerForm
{
    partial class AccessibilityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessibilityForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label3 = new Label();
            textBox1 = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            panel1 = new Panel();
            btnApply = new Button();
            changePathBtn = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.FromArgb(241, 100, 0);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(534, 10);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.Desktop;
            label2.Location = new Point(29, 32);
            label2.Name = "label2";
            label2.Size = new Size(432, 35);
            label2.TabIndex = 1;
            label2.Text = "Folder zapisu zadań";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.FromArgb(241, 100, 0);
            flowLayoutPanel2.Dock = DockStyle.Bottom;
            flowLayoutPanel2.Location = new Point(0, 543);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(534, 10);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Desktop;
            label3.Location = new Point(29, 118);
            label3.Name = "label3";
            label3.Size = new Size(432, 35);
            label3.TabIndex = 3;
            label3.Text = "Uruchom aplikację przy starcie systemu";
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(32, 70);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(391, 27);
            textBox1.TabIndex = 4;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(95, 24);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.Text = "Włączone";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(3, 33);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(102, 24);
            radioButton2.TabIndex = 7;
            radioButton2.Text = "Wyłączone";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(radioButton2);
            panel1.Location = new Point(32, 156);
            panel1.Name = "panel1";
            panel1.Size = new Size(473, 69);
            panel1.TabIndex = 8;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(241, 100, 0);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(170, 495);
            btnApply.Margin = new Padding(4);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(178, 41);
            btnApply.TabIndex = 9;
            btnApply.Text = "Zapisz zmiany";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // changePathBtn
            // 
            changePathBtn.BackColor = Color.FromArgb(241, 100, 0);
            changePathBtn.FlatAppearance.BorderSize = 0;
            changePathBtn.FlatStyle = FlatStyle.Flat;
            changePathBtn.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            changePathBtn.ForeColor = Color.White;
            changePathBtn.Location = new Point(430, 70);
            changePathBtn.Margin = new Padding(4);
            changePathBtn.Name = "changePathBtn";
            changePathBtn.Size = new Size(75, 27);
            changePathBtn.TabIndex = 10;
            changePathBtn.Text = "Zmień";
            changePathBtn.UseVisualStyleBackColor = false;
            changePathBtn.Click += changePathBtn_Click;
            // 
            // AccessibilityForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(534, 553);
            Controls.Add(changePathBtn);
            Controls.Add(btnApply);
            Controls.Add(panel1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(label2);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(552, 600);
            MinimizeBox = false;
            MinimumSize = new Size(552, 600);
            Name = "AccessibilityForm";
            Text = "Ustawienia";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label3;
        private TextBox textBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Panel panel1;
        private Button btnApply;
        private Button changePathBtn;
    }
}