
namespace Shirehorse.Core.FiniteStateMachines
{
    partial class FiniteStateMachineCollectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiniteStateMachineCollectionForm));
            this.treeViewNavigationUserControl = new Shirehorse.Core.TreeViewNavigationUserControl();
            this.SuspendLayout();
            // 
            // treeViewNavigationUserControl
            // 
            this.treeViewNavigationUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNavigationUserControl.Location = new System.Drawing.Point(0, 0);
            this.treeViewNavigationUserControl.Name = "treeViewNavigationUserControl";
            this.treeViewNavigationUserControl.Size = new System.Drawing.Size(1498, 777);
            this.treeViewNavigationUserControl.SplitterDistance = 500;
            this.treeViewNavigationUserControl.TabIndex = 0;
            // 
            // FiniteStateMachineCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1498, 777);
            this.Controls.Add(this.treeViewNavigationUserControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FiniteStateMachineCollectionForm";
            this.Text = "Finite State Machines";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FiniteStateMachineForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeViewNavigationUserControl treeViewNavigationUserControl;
    }
}