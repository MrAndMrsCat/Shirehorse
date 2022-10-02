
namespace Shirehorse.Core.Diagnostics
{
    partial class ExceptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
            this.button_showDetail = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.button_copyToClipboard = new System.Windows.Forms.Button();
            this.button_disableExceptionMessages = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_showDetail
            // 
            this.button_showDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_showDetail.Location = new System.Drawing.Point(56, 115);
            this.button_showDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_showDetail.Name = "button_showDetail";
            this.button_showDetail.Size = new System.Drawing.Size(112, 35);
            this.button_showDetail.TabIndex = 1;
            this.button_showDetail.Text = "Show Detail";
            this.button_showDetail.UseVisualStyleBackColor = true;
            this.button_showDetail.Click += new System.EventHandler(this.ShowDetail_Click);
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(0, 23, 0, 0);
            this.label.Size = new System.Drawing.Size(369, 162);
            this.label.TabIndex = 0;
            this.label.Text = "Exception messaage";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(369, 162);
            this.treeView.TabIndex = 0;
            // 
            // button_copyToClipboard
            // 
            this.button_copyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_copyToClipboard.Location = new System.Drawing.Point(223, 14);
            this.button_copyToClipboard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_copyToClipboard.Name = "button_copyToClipboard";
            this.button_copyToClipboard.Size = new System.Drawing.Size(132, 35);
            this.button_copyToClipboard.TabIndex = 2;
            this.button_copyToClipboard.Text = "Copy to clipboard";
            this.button_copyToClipboard.UseVisualStyleBackColor = true;
            this.button_copyToClipboard.Visible = false;
            this.button_copyToClipboard.Click += new System.EventHandler(this.CopyToClipboard_Click);
            // 
            // button_disableExceptionMessages
            // 
            this.button_disableExceptionMessages.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_disableExceptionMessages.Location = new System.Drawing.Point(176, 115);
            this.button_disableExceptionMessages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_disableExceptionMessages.Name = "button_disableExceptionMessages";
            this.button_disableExceptionMessages.Size = new System.Drawing.Size(151, 35);
            this.button_disableExceptionMessages.TabIndex = 3;
            this.button_disableExceptionMessages.Text = "Disable These Messages";
            this.button_disableExceptionMessages.UseVisualStyleBackColor = true;
            this.button_disableExceptionMessages.Click += new System.EventHandler(this.DisableExceptionMessages_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 162);
            this.Controls.Add(this.button_disableExceptionMessages);
            this.Controls.Add(this.button_copyToClipboard);
            this.Controls.Add(this.button_showDetail);
            this.Controls.Add(this.label);
            this.Controls.Add(this.treeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.Text = "Unhandled Exception";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button button_showDetail;
        private System.Windows.Forms.Button button_copyToClipboard;
        private System.Windows.Forms.Button button_disableExceptionMessages;
    }
}