using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLP_organizer
{
    public partial class AddProject : Form
    {
        private Main _main;

        public AddProject(Main m)
        {
            _main = m;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            storeAndReload();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) 
            {
                storeAndReload();
            }
        }

        private void storeAndReload()
        {
            string nDir = Properties.Settings.Default.projectFolder + "\\" + textBox1.Text.Trim();
            if (!Directory.Exists(nDir))
            {
                Directory.CreateDirectory(nDir);
                Directory.CreateDirectory(nDir + "\\YT");
            }
            _main.reloadTreeView();
            Hide();
        }
    }
}
