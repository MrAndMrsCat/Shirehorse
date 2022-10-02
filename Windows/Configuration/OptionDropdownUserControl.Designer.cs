namespace Shirehorse.Core.Configuration
{
    partial class OptionControl_Dropdown
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.lbl_description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.CausesValidation = false;
            this.comboBox.Location = new System.Drawing.Point(8, 3);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(82, 21);
            this.comboBox.TabIndex = 5;
            this.comboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
            // 
            // lbl_description
            // 
            this.lbl_description.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_description.AutoSize = true;
            this.lbl_description.Location = new System.Drawing.Point(93, 7);
            this.lbl_description.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lbl_description.MinimumSize = new System.Drawing.Size(420, 0);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(420, 13);
            this.lbl_description.TabIndex = 4;
            this.lbl_description.Text = "label1";
            this.lbl_description.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionControl_Dropdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.lbl_description);
            this.Name = "OptionControl_Dropdown";
            this.Size = new System.Drawing.Size(539, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label lbl_description;
    }
}
