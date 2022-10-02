namespace Shirehorse.Core
{
    public partial class TreeViewNavigationUserControl : UserControl
    {
        public TreeViewNavigationUserControl()
        {
            InitializeComponent();
        }

        public Control? DisplayedControl { get; private set; }

        public int SplitterDistance
        {
            get => splitContainer.SplitterDistance;
            set => splitContainer.SplitterDistance = value;
        }

        //public IEnumerable<Control> Items => treeView
        //    .Nodes
        //    .Cast<TreeNode>()
        //    .Select(node => node.Tag as Control)
        //    .Where(control => control is not null);

        //public TreeNodeCollection Nodes => treeView.Nodes;

        public void Add(string label, UserControl userControl)
        {
            treeView.Nodes.Add(new TreeNode(label) { Tag = userControl });

            if (DisplayedControl is null) ShowControlAtNode(treeView.Nodes[0]);
        }

        private void ShowControlAtNode(TreeNode node)
        {
            if (node.Tag is Control control)
            {
                if (DisplayedControl is not null)
                {
                    panel_displayedPanel.Controls.Remove(DisplayedControl);
                }

                panel_displayedPanel.Controls.Add(control);

                control.Dock = DockStyle.Fill;

                DisplayedControl = control;
            }
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) => ShowControlAtNode(e.Node);

        private void Collapse_Click(object sender, EventArgs e)
        {
            splitContainer.Panel1Collapsed = !splitContainer.Panel1Collapsed;

            button_collapse.ImageIndex = splitContainer.Panel1Collapsed ? 1 : 0;
        }
    }
}
