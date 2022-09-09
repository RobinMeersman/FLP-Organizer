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
    public partial class Setup : Form
    {

        private string _path;

        public Setup()
        {
            InitializeComponent();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void pathChooser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.ShowDialog();

            if(d.SelectedPath != null)
            {
                label1.Text = d.SelectedPath;
                _path = d.SelectedPath;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.projectFolder = _path;
            Properties.Settings.Default.Save();
            Main m = new Main();
            Hide();
            m.ShowDialog();
            Close();
        }
    }
}
