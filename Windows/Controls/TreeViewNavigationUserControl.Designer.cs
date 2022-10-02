namespace Shirehorse.Core
{
    partial class TreeViewNavigationUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewNavigationUserControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.panel_displayedPanel = new System.Windows.Forms.Panel();
            this.button_collapse = new System.Windows.Forms.Button();
            this.imageList_collapseButton = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel_displayedPanel);
            this.splitContainer.Panel2.Controls.Add(this.button_collapse);
            this.splitContainer.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.SplitterDistance = 268;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HotTracking = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.ShowLines = false;
            this.treeView.ShowPlusMinus = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(268, 600);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // panel_displayedPanel
            // 
            this.panel_displayedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_displayedPanel.Location = new System.Drawing.Point(18, 0);
            this.panel_displayedPanel.Name = "panel_displayedPanel";
            this.panel_displayedPanel.Size = new System.Drawing.Size(710, 600);
            this.panel_displayedPanel.TabIndex = 1;
            // 
            // button_collapse
            // 
            this.button_collapse.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_collapse.ImageIndex = 0;
            this.button_collapse.ImageList = this.imageList_collapseButton;
            this.button_collapse.Location = new System.Drawing.Point(0, 0);
            this.button_collapse.Name = "button_collapse";
            this.button_collapse.Size = new System.Drawing.Size(18, 600);
            this.button_collapse.TabIndex = 0;
            this.button_collapse.UseVisualStyleBackColor = true;
            this.button_collapse.Click += new System.EventHandler(this.Collapse_Click);
            // 
            // imageList_collapseButton
            // 
            this.imageList_collapseButton.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_collapseButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_collapseButton.ImageStream")));
            this.imageList_collapseButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_collapseButton.Images.SetKeyName(0, "CollapseLeft");
            this.imageList_collapseButton.Images.SetKeyName(1, "CollapseRight");
            // 
            // TreeViewNavigationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "TreeViewNavigationUserControl";
            this.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button button_collapse;
        private System.Windows.Forms.ImageList imageList_collapseButton;
        private System.Windows.Forms.Panel panel_displayedPanel;
    }
}
