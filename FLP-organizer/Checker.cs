using System;
using System.Windows.Forms;

namespace FLP_organizer
{
    public static class Checker
    {
        public static bool Check(TreeView tree)
        {
            if(tree == null || tree.SelectedNode == null) return false;

            //check if selected node is a direct child of the root node of the tree
            return tree.SelectedNode.Parent == tree.Nodes[0];
        }
    }
}