﻿namespace Shirehorse.Core.Configuration
{
    partial class OptionCheckboxUserControl
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
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(10, 4);
            this.checkBox.MinimumSize = new System.Drawing.Size(528, 0);
            this.checkBox.Name = "checkBox";
            this.checkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox.Size = new System.Drawing.Size(528, 17);
            this.checkBox.TabIndex = 6;
            this.checkBox.Text = "checkBox1";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // OptionControl_Checkbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox);
            this.Name = "OptionControl_Checkbox";
            this.Size = new System.Drawing.Size(539, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBox;
    }
}
