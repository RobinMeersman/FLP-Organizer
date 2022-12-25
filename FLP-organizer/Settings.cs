using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLP_organizer
{
    public partial class Settings : Form
    {
        private Main _m;
        private bool changed = false;
        public Settings(Main m)
        {
            _m = m;
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.projectFolder;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();

            if (!string.IsNullOrEmpty(f.SelectedPath) && f.SelectedPath != Properties.Settings.Default.projectFolder)
            {
                changed = true;
                textBox1.Text = f.SelectedPath;
                Properties.Settings.Default.projectFolder = f.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(changed) _m.ChangeDirTree();
            this.Hide();
        }
    }
}
