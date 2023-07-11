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
            ToggleOnButton.Location = new Point(12, 136);
            ToggleOnButton.Name = "ToggleOnButton";
            ToggleOnButton.Size = new Size(160, 23);
            ToggleOnButton.TabIndex = 0;
            ToggleOnButton.Text = "Start (Ctrl+Tab)";
            ToggleOnButton.UseVisualStyleBackColor = true;
            ToggleOnButton.Click += ToggleOnButton_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(80, 66);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(92, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 170, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1, 68);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
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
            label2.Location = new Point(66, 118);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 3;
            label2.Text = "Bean: F8";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(184, 171);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(ToggleOnButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
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