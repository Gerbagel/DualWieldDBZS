using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWieldDBZS
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CancelKeysString = CancelKeysInput.Text;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            CancelKeysInput.Text = Properties.Settings.Default.CancelKeysString;
        }
    }
}
