
namespace Shirehorse.Core.IO
{
    partial class FileOperationCoordinatorProgressForm
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
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_timeRemaining = new System.Windows.Forms.Label();
            this.lbl_itemsRemaining = new System.Windows.Forms.Label();
            this.coloredProgressBar = new Shirehorse.Core.Controls.ColoredProgressBarUserControl();
            this.SuspendLayout();
            // 
            // lbl_status
            // 
            this.lbl_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(14, 79);
            this.lbl_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(42, 15);
            this.lbl_status.TabIndex = 1;
            this.lbl_status.Text = "Status:";
            // 
            // lbl_timeRemaining
            // 
            this.lbl_timeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_timeRemaining.AutoSize = true;
            this.lbl_timeRemaining.Location = new System.Drawing.Point(14, 104);
            this.lbl_timeRemaining.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_timeRemaining.Name = "lbl_timeRemaining";
            this.lbl_timeRemaining.Size = new System.Drawing.Size(165, 15);
            this.lbl_timeRemaining.TabIndex = 2;
            this.lbl_timeRemaining.Text = "Time remaining: Calculating...";
            // 
            // lbl_itemsRemaining
            // 
            this.lbl_itemsRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_itemsRemaining.AutoSize = true;
            this.lbl_itemsRemaining.Location = new System.Drawing.Point(14, 129);
            this.lbl_itemsRemaining.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_itemsRemaining.Name = "lbl_itemsRemaining";
            this.lbl_itemsRemaining.Size = new System.Drawing.Size(96, 15);
            this.lbl_itemsRemaining.TabIndex = 3;
            this.lbl_itemsRemaining.Text = "Items remaining:";
            // 
            // coloredProgressBar
            // 
            this.coloredProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coloredProgressBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.coloredProgressBar.BarColor = System.Drawing.SystemColors.ControlDark;
            this.coloredProgressBar.Location = new System.Drawing.Point(9, 9);
            this.coloredProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.coloredProgressBar.Maximum = 1D;
            this.coloredProgressBar.Name = "coloredProgressBar";
            this.coloredProgressBar.NyanCat = true;
            this.coloredProgressBar.Size = new System.Drawing.Size(630, 59);
            this.coloredProgressBar.TabIndex = 5;
            this.coloredProgressBar.Value = 0D;
            // 
            // FileOperationCoordinatorProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 160);
            this.Controls.Add(this.coloredProgressBar);
            this.Controls.Add(this.lbl_itemsRemaining);
            this.Controls.Add(this.lbl_timeRemaining);
            this.Controls.Add(this.lbl_status);
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileOperationCoordinatorProgressForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FileOperationForm_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileDialogForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileOperationForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_timeRemaining;
        private System.Windows.Forms.Label lbl_itemsRemaining;
        private Controls.ColoredProgressBarUserControl coloredProgressBar;
        private System.Windows.Forms.Label lbl_status;
    }
}