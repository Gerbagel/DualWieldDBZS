namespace DualWieldDBZS
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            CancelKeysInput = new TextBox();
            SaveSettingsButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // CancelKeysInput
            // 
            CancelKeysInput.BackColor = Color.Black;
            CancelKeysInput.ForeColor = Color.White;
            CancelKeysInput.Location = new Point(12, 32);
            CancelKeysInput.Name = "CancelKeysInput";
            CancelKeysInput.Size = new Size(194, 19);
            CancelKeysInput.TabIndex = 2;
            CancelKeysInput.Text = "E, Escape, V, L, K, X";
            // 
            // SaveSettingsButton
            // 
            SaveSettingsButton.BackColor = Color.FromArgb(153, 153, 153);
            SaveSettingsButton.BackgroundImage = (Image)resources.GetObject("SaveSettingsButton.BackgroundImage");
            SaveSettingsButton.FlatAppearance.BorderColor = Color.Black;
            SaveSettingsButton.FlatAppearance.MouseOverBackColor = Color.White;
            SaveSettingsButton.FlatStyle = FlatStyle.Popup;
            SaveSettingsButton.ForeColor = SystemColors.ControlLightLight;
            SaveSettingsButton.Location = new Point(13, 120);
            SaveSettingsButton.Margin = new Padding(4, 3, 4, 3);
            SaveSettingsButton.Name = "SaveSettingsButton";
            SaveSettingsButton.Size = new Size(193, 28);
            SaveSettingsButton.TabIndex = 4;
            SaveSettingsButton.Text = "Save and exit";
            SaveSettingsButton.UseVisualStyleBackColor = false;
            SaveSettingsButton.Click += SaveSettingsButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(97, 13);
            label1.TabIndex = 5;
            label1.Text = "Cancel keys:";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(9F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(218, 160);
            Controls.Add(label1);
            Controls.Add(SaveSettingsButton);
            Controls.Add(CancelKeysInput);
            Font = new Font("Minecraft", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            Text = "Settings";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox CancelKeysInput;
        private Button SaveSettingsButton;
        private Label label1;
    }
}