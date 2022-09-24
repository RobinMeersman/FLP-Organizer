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
        private string _selected;

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

            // if the projectfolder is a new folder, no items will be present so we can return out faster
            if (start.GetDirectories().Length <= 0) return;

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
            tree.SelectedNode = tree.Nodes[0].Nodes[0];
            _selected = tree.SelectedNode.Text;
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
            _selected = tree.SelectedNode != null ? tree.SelectedNode.Text : "";
            
            // add check if selected item is a "root" node
            try
            {
                //check if selected node is direct child of root
                if (_selected.Length == 0 && !Checker.Check(tree)) return;

                Directory.Delete(_root.FullName + "\\" + _selected, true);
                ReloadTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(_selected + " can not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selected = tree.SelectedNode != null ? tree.SelectedNode.Text : "";

            try
            {
                if (_selected.Length == 0 && !Checker.Check(tree)) return;

                if (!Directory.Exists(_root.FullName + "\\" + _selected))
                {
                    MessageBox.Show(_selected + " does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                new Rename(this).Show();                
            } catch (Exception ex)
            {
                MessageBox.Show(_selected + " can not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(ex.Message);
            }
        }

        public void SetNewFolder(string name)
        {
            if (name == null || name == "") return;

            //BUG!!!!
            RenameFolder(_root.FullName + "\\" + _selected, _root.FullName + "\\" + name);
        }

        private void RenameFolder(string from, string to)
        {
            Directory.Move(from, to);
        }
    }
}
