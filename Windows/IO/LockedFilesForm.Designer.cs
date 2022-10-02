namespace Shirehorse.Core
{
    partial class LockedFilesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockedFilesForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ts_killAllProcesses = new System.Windows.Forms.ToolStripButton();
            this.ts_refresh = new System.Windows.Forms.ToolStripButton();
            this.Tree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_killProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_killAllProcesses,
            this.ts_refresh});
            this.toolStrip.Location = new System.Drawing.Point(0, 290);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip.Size = new System.Drawing.Size(552, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // ts_killAllProcesses
            // 
            this.ts_killAllProcesses.Image = ((System.Drawing.Image)(resources.GetObject("ts_killAllProcesses.Image")));
            this.ts_killAllProcesses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_killAllProcesses.Name = "ts_killAllProcesses";
            this.ts_killAllProcesses.Size = new System.Drawing.Size(143, 22);
            this.ts_killAllProcesses.Text = "Kill all listed processes";
            this.ts_killAllProcesses.Visible = false;
            this.ts_killAllProcesses.Click += new System.EventHandler(this.KillAllProcesses_Click);
            // 
            // ts_refresh
            // 
            this.ts_refresh.Image = ((System.Drawing.Image)(resources.GetObject("ts_refresh.Image")));
            this.ts_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_refresh.Name = "ts_refresh";
            this.ts_refresh.Size = new System.Drawing.Size(66, 22);
            this.ts_refresh.Text = "Refresh";
            this.ts_refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Tree
            // 
            this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree.Location = new System.Drawing.Point(0, 0);
            this.Tree.Name = "Tree";
            this.Tree.Size = new System.Drawing.Size(552, 290);
            this.Tree.TabIndex = 1;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_killProcess});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(134, 26);
            // 
            // mi_killProcess
            // 
            this.mi_killProcess.Name = "mi_killProcess";
            this.mi_killProcess.Size = new System.Drawing.Size(133, 22);
            this.mi_killProcess.Text = "Kill Process";
            this.mi_killProcess.Click += new System.EventHandler(this.KillProcess_Click);
            // 
            // LockedFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 315);
            this.Controls.Add(this.Tree);
            this.Controls.Add(this.toolStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockedFilesForm";
            this.ShowIcon = false;
            this.Text = "Locked Files";
            this.TopMost = true;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton ts_killAllProcesses;
        private System.Windows.Forms.TreeView Tree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mi_killProcess;
        private System.Windows.Forms.ToolStripButton ts_refresh;
    }
}