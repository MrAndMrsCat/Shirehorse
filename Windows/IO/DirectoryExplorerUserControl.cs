namespace Shirehorse.Core
{
    public partial class DirectoryExplorerUserControl : UserControl
    {
        public string SelectedFolderPath { get; private set; }
        public bool ExpandOnSingleClick = true;

        private string default_root = "c:\\";

        public DirectoryExplorerUserControl()
        {
            InitializeComponent();
            Create_Tree(default_root);
        }

        public void Create_Tree(string root_dir)
        {
            var info = new DirectoryInfo(root_dir);
            if (!info.Exists) throw new Exception("directory not valid");

            Tree.Nodes.Clear();
            var root = new TreeNode()
            {
                Text = info.Root.FullName,
                Tag = info.Root,
                ImageKey = "drive",
                SelectedImageKey = "drive",
            };

            Populate_Nodes(root);
            Tree.Nodes.Add(root);
            root.Expand();
            Tree.SelectedNode = root;
        }

        public void Create_Tree()
        {
            Tree.Nodes.Clear();
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var node = new TreeNode()
                {
                    Text = drive,
                    Tag = new DirectoryInfo(drive),
                    ImageKey = "drive",
                    SelectedImageKey = "drive",
                };
                Populate_Nodes(node);
                Tree.Nodes.Add(node);
            }
        }

        public void GoTo(string target_dir)
        {
            Create_Tree();
            var info = new DirectoryInfo(target_dir);

            var dir_ls = new List<DirectoryInfo>();
            DirectoryInfo walk_backwards = info;
            while (walk_backwards.FullName != info.Root.FullName)
            {
                dir_ls.Add(walk_backwards);
                walk_backwards = walk_backwards.Parent;
            }
            dir_ls.Reverse();

            foreach (TreeNode drive_node in Tree.Nodes)
            {
                if (drive_node.Text == info.Root.FullName.ToUpper())
                {
                    var parent_node = drive_node;
                    foreach (var dir in dir_ls) // step forward through full path
                    {
                        var this_node = parent_node.Nodes.Find(dir.Name, false)[0];
                        Populate_Nodes(this_node);
                        this_node.EnsureVisible();
                        Tree.SelectedNode = this_node;
                        this_node.Expand();
                        parent_node = this_node;
                    }
                    SelectedFolderPath = info.FullName;
                    break;
                }
            }
        }





        private void Populate_Nodes(TreeNode parent)
        {
            var parent_info = parent.Tag as DirectoryInfo;

            parent.Nodes.Clear();
            try // can fail on directory access permission
            {
                foreach (var child_info in parent_info.GetDirectories())
                {
                    var child_node = new TreeNode()
                    {
                        Name = child_info.Name,
                        Text = child_info.Name,
                        Tag = child_info,
                        ImageKey = "folder_closed",
                        SelectedImageKey = "folder_opened"
                    };
                    
                    parent.Nodes.Add(child_node);
                }
                
            }
            catch { }

        }


        private void Tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode child in e.Node.Nodes)
                Populate_Nodes(child);
            
        }
        private void Tree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            Populate_Nodes(e.Node);
        }

        public void OnFolderClicked(EventArgs e) { FolderClicked?.Invoke(this, e); }
        public event EventHandler FolderClicked;

        private void Tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectedFolderPath = (e.Node.Tag as DirectoryInfo).FullName;
            //if (ExpandOnSingleClick && !e.Node.IsExpanded) e.Node.Expand();
            OnFolderClicked(new EventArgs());
        }



    }
}
