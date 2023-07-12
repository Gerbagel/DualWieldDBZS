namespace DualWieldDBZS
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
            ToggleOnButton = new Button();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            ClickTimer = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // ToggleOnButton
            // 
            ToggleOnButton.BackColor = Color.FromArgb(153, 153, 153);
            ToggleOnButton.BackgroundImage = (Image)resources.GetObject("ToggleOnButton.BackgroundImage");
            ToggleOnButton.FlatAppearance.BorderColor = Color.Black;
            ToggleOnButton.FlatAppearance.MouseOverBackColor = Color.White;
            ToggleOnButton.FlatStyle = FlatStyle.Popup;
            ToggleOnButton.Location = new Point(13, 158);
            ToggleOnButton.Margin = new Padding(4, 3, 4, 3);
            ToggleOnButton.Name = "ToggleOnButton";
            ToggleOnButton.Size = new Size(211, 28);
            ToggleOnButton.TabIndex = 0;
            ToggleOnButton.Text = "Start (Ctrl+Tab)";
            ToggleOnButton.UseVisualStyleBackColor = false;
            ToggleOnButton.Click += ToggleOnButton_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.BackColor = SystemColors.InfoText;
            numericUpDown1.ForeColor = SystemColors.ControlLight;
            numericUpDown1.Location = new Point(126, 46);
            numericUpDown1.Margin = new Padding(4, 3, 4, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(98, 19);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 170, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(13, 48);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 13);
            label1.TabIndex = 2;
            label1.Text = "Interval (ms)";
            // 
            // ClickTimer
            // 
            ClickTimer.Tick += ClickTimer_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = SystemColors.ControlLight;
            label2.Location = new Point(86, 128);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(66, 13);
            label2.TabIndex = 3;
            label2.Text = "Bean: F8";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(237, 198);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(ToggleOnButton);
            Font = new Font("Minecraft", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Dual Wield";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ToggleOnButton;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private System.Windows.Forms.Timer ClickTimer;
        private Label label2;
    }
}