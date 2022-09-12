using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace FLP_organizer
{
    public partial class Main : Form
    {
        private readonly DirectoryInfo _root = new DirectoryInfo(Properties.Settings.Default.projectFolder);

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
            TreeNode root = new TreeNode(Properties.Settings.Default.projectFolder);
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
                root.Nodes.Add(nodes);
            }
            tree.Nodes.Add(root);
            root.Expand();
        }

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProject addProject = new AddProject(this);
            addProject.Show();
        }

        public void ReloadTreeView()
        {
            tree.Nodes.Clear();
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
            string selected = tree.SelectedNode.Text;
            
            // add check if selected item is a "root" node
            try
            {
                //check if selected node is direct child of root
                if (!Checker.Check(tree)) return;

                //todo: when folder is not empty: error will be thrown
                Directory.Delete(_root.FullName + "\\" + selected, true);
                ReloadTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(selected + " can not be deleted", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
