namespace Shirehorse.Core.Controls
{
    partial class ColoredProgressBarUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColoredProgressBarUserControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.nyanCatImage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.Location = new System.Drawing.Point(2, 2);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.LimeGreen;
            this.splitContainer.Panel1.Controls.Add(this.nyanCatImage);
            this.splitContainer.Panel1MinSize = 0;
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(500, 76);
            this.splitContainer.SplitterDistance = 165;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // nyanCatImage
            // 
            this.nyanCatImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.nyanCatImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nyanCatImage.Image = ((System.Drawing.Image)(resources.GetObject("nyanCatImage.Image")));
            this.nyanCatImage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nyanCatImage.Location = new System.Drawing.Point(0, 0);
            this.nyanCatImage.Margin = new System.Windows.Forms.Padding(0);
            this.nyanCatImage.Name = "nyanCatImage";
            this.nyanCatImage.Size = new System.Drawing.Size(165, 76);
            this.nyanCatImage.TabIndex = 0;
            this.nyanCatImage.Visible = false;
            // 
            // ColoredProgressBarUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ColoredProgressBarUserControl";
            this.Size = new System.Drawing.Size(504, 80);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label nyanCatImage;
    }
}
