using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeGenerator;

namespace KRR
{
    public partial class TreeForm : Form
    {
        public TreeData.TreeDataTableDataTable TreeBuilder;
        public TreeForm(TreeData.TreeDataTableDataTable tree)
        {
            TreeBuilder = tree;
            InitializeComponent();
        }

        private void TreeForm_Load(object sender, EventArgs e)
        {
            myTree = new TreeBuilder(TreeBuilder);

            ShowTree();
        }
        private TreeBuilder myTree = null;
        private void ShowTree()
        {
            picTree.Image = Image.FromStream(myTree.GenerateTree(-1, -1, "1", System.Drawing.Imaging.ImageFormat.Bmp));
            //picTree.Image.Save(@"d:\temp\1.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        private TreeData.TreeDataTableDataTable GetTreeData()
        {
            TreeData.TreeDataTableDataTable dt = new TreeData.TreeDataTableDataTable();
            dt.AddTreeDataTableRow("1", "", "Localhost", "This is your Local Server");
            dt.AddTreeDataTableRow("2-2", "1", "Child 1", "This is the first child.");
            dt.AddTreeDataTableRow("3", "1", "Child 2", "This is the second child.");
            dt.AddTreeDataTableRow("4", "1", "Child 3", "This is the third child.");
            dt.AddTreeDataTableRow("5", "2-2", "GrandChild 1", "This is the only Grandchild.");
            for (int i = 6; i < 5; i++)
            {
                Random rand = new Random();
                dt.AddTreeDataTableRow(i.ToString(), rand.Next(1, i).ToString(), "GrandChild " + i.ToString(), "This is the only Grandchild.");
            }
            return dt;
        }
    }
}
