using System;
using System.Drawing;
using System.Linq;
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
            LoadDirectory(_root);
        }

        private void LoadDirectory(DirectoryInfo start)
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

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProject addProject = new AddProject(this);
            addProject.Show();
        }

        public void ReloadTreeView()
        {
            treeView1.Nodes.Clear();
            LoadDirectory(_root);
        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadTreeView();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = treeView1.SelectedNode.Text;
            
            // add check if selected item is a "root" node
            try
            {
                //check if selected node is direct child of root
                if (!Checker.Check(treeView1)) return;



                //todo: when folder is not empty: error will be thrown
                Directory.Delete(_root.FullName + "\\" + selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(selected + " can not be deleted", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
