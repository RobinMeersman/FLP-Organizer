using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FLP_organizer
{
    public partial class Main : Form
    {
        private DirectoryInfo _root = new DirectoryInfo(Properties.Settings.Default.projectFolder);

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(700, 500);

            //load folderstructure in treeview:
            loadDirectory(_root);
        }

        private void loadDirectory(DirectoryInfo start)
        {
            foreach(DirectoryInfo dir in start.GetDirectories())
            {
                TreeNode nodes = new TreeNode(dir.Name);
                try
                {
                    nodes.Nodes.Add(dir.GetDirectories().First().Name);
                    foreach (FileInfo f in dir.GetDirectories().First().GetFiles())
                    {
                        nodes.Nodes[0].Nodes.Add(f.Name);
                    }
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        nodes.Nodes.Add(file.Name);
                    }
                } catch(UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                treeView1.Nodes.Add(nodes);
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProject addProject = new AddProject(this);
            addProject.Show();
        }

        public void reloadTreeView()
        {
            treeView1.Nodes.Clear();
            loadDirectory(_root);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reloadTreeView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = treeView1.SelectedNode.Text;

            // add check if selected item is a "root" node
            try
            {

                //todo: when folder is not empty: error will be thrown
                Directory.Delete(_root.FullName + "\\" + selected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
