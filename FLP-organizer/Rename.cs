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
    public partial class Rename : Form
    {
        private readonly Main _m;

        public Rename(Main m)
        {
            _m = m;
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _m.SetNewFolder(textBox1.Text);
        }
    }
}
