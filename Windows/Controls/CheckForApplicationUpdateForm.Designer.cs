
namespace Shirehorse.Core
{
    partial class CheckForApplicationUpdateForm
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
            this.label_mainText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_currentVersion = new System.Windows.Forms.TextBox();
            this.txtBox_newVersion = new System.Windows.Forms.TextBox();
            this.button_accept = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_mainText
            // 
            this.label_mainText.AutoSize = true;
            this.label_mainText.Location = new System.Drawing.Point(74, 71);
            this.label_mainText.Name = "label_mainText";
            this.label_mainText.Size = new System.Drawing.Size(87, 13);
            this.label_mainText.TabIndex = 10;
            this.label_mainText.Text = "Launch installer?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Current version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "New version:";
            // 
            // txtBox_currentVersion
            // 
            this.txtBox_currentVersion.Location = new System.Drawing.Point(105, 12);
            this.txtBox_currentVersion.Name = "txtBox_currentVersion";
            this.txtBox_currentVersion.ReadOnly = true;
            this.txtBox_currentVersion.Size = new System.Drawing.Size(113, 20);
            this.txtBox_currentVersion.TabIndex = 30;
            // 
            // txtBox_newVersion
            // 
            this.txtBox_newVersion.Location = new System.Drawing.Point(105, 38);
            this.txtBox_newVersion.Name = "txtBox_newVersion";
            this.txtBox_newVersion.ReadOnly = true;
            this.txtBox_newVersion.Size = new System.Drawing.Size(113, 20);
            this.txtBox_newVersion.TabIndex = 40;
            // 
            // button_accept
            // 
            this.button_accept.Location = new System.Drawing.Point(46, 95);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(68, 28);
            this.button_accept.TabIndex = 0;
            this.button_accept.Text = "Yes";
            this.button_accept.UseVisualStyleBackColor = true;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(120, 95);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(68, 28);
            this.button_cancel.TabIndex = 1;
            this.button_cancel.Text = "No";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // CheckForUpdateForm
            // 
            this.AcceptButton = this.button_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(245, 135);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.txtBox_newVersion);
            this.Controls.Add(this.txtBox_currentVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_mainText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckForUpdateForm";
            this.ShowIcon = false;
            this.Text = "Update Available";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_mainText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_currentVersion;
        private System.Windows.Forms.TextBox txtBox_newVersion;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Button button_cancel;
    }
}