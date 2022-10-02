
namespace Shirehorse.Core
{
    partial class FilePathUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilePathUserControl));
            this.btn_file = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_file
            // 
            this.btn_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_file.ImageKey = "file";
            this.btn_file.ImageList = this.imageList;
            this.btn_file.Location = new System.Drawing.Point(436, 0);
            this.btn_file.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_file.Name = "btn_file";
            this.btn_file.Size = new System.Drawing.Size(30, 25);
            this.btn_file.TabIndex = 43;
            this.btn_file.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "File");
            this.imageList.Images.SetKeyName(1, "Folder");
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(4, 5);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(32, 15);
            this.label.TabIndex = 40;
            this.label.Text = "label";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(44, 1);
            this.comboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(384, 23);
            this.comboBox.TabIndex = 44;
            this.comboBox.SelectedValueChanged += new System.EventHandler(this.InputBox_SelectedValueChanged);
            this.comboBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            this.comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            // 
            // FilePathUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.btn_file);
            this.Controls.Add(this.label);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(116665, 28);
            this.MinimumSize = new System.Drawing.Size(175, 26);
            this.Name = "FilePathUserControl";
            this.Size = new System.Drawing.Size(467, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_file;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ComboBox comboBox;
    }
}
