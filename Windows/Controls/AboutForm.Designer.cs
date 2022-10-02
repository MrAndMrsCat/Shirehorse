namespace Shirehorse.Core
{
    partial class AboutForm
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
            this.label_header = new System.Windows.Forms.Label();
            this.label_version_value = new System.Windows.Forms.Label();
            this.linkLabel_url = new System.Windows.Forms.LinkLabel();
            this.label_version = new System.Windows.Forms.Label();
            this.linkLabel_support_contact = new System.Windows.Forms.LinkLabel();
            this.btn_showStateMachines = new System.Windows.Forms.Button();
            this.btn_showConsole = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_header
            // 
            this.label_header.AutoSize = true;
            this.label_header.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_header.Location = new System.Drawing.Point(14, 14);
            this.label_header.Margin = new System.Windows.Forms.Padding(4, 3, 14, 3);
            this.label_header.Name = "label_header";
            this.label_header.Size = new System.Drawing.Size(98, 19);
            this.label_header.TabIndex = 0;
            this.label_header.Text = "label_header";
            // 
            // label_version_value
            // 
            this.label_version_value.AutoSize = true;
            this.label_version_value.Location = new System.Drawing.Point(71, 43);
            this.label_version_value.Margin = new System.Windows.Forms.Padding(4, 3, 14, 3);
            this.label_version_value.Name = "label_version_value";
            this.label_version_value.Size = new System.Drawing.Size(75, 15);
            this.label_version_value.TabIndex = 1;
            this.label_version_value.Text = "label_version";
            // 
            // linkLabel_url
            // 
            this.linkLabel_url.AutoSize = true;
            this.linkLabel_url.Location = new System.Drawing.Point(15, 80);
            this.linkLabel_url.Margin = new System.Windows.Forms.Padding(4, 3, 14, 3);
            this.linkLabel_url.Name = "linkLabel_url";
            this.linkLabel_url.Size = new System.Drawing.Size(73, 15);
            this.linkLabel_url.TabIndex = 3;
            this.linkLabel_url.TabStop = true;
            this.linkLabel_url.Text = "linkLabel_url";
            this.linkLabel_url.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.URL_LinkClicked);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Location = new System.Drawing.Point(15, 43);
            this.label_version.Margin = new System.Windows.Forms.Padding(4, 3, 0, 3);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(48, 30);
            this.label_version.TabIndex = 4;
            this.label_version.Text = "Version:\r\nBuild:";
            // 
            // linkLabel_support_contact
            // 
            this.linkLabel_support_contact.AutoSize = true;
            this.linkLabel_support_contact.Location = new System.Drawing.Point(15, 102);
            this.linkLabel_support_contact.Margin = new System.Windows.Forms.Padding(4, 3, 14, 3);
            this.linkLabel_support_contact.Name = "linkLabel_support_contact";
            this.linkLabel_support_contact.Size = new System.Drawing.Size(145, 15);
            this.linkLabel_support_contact.TabIndex = 5;
            this.linkLabel_support_contact.TabStop = true;
            this.linkLabel_support_contact.Text = "linkLabel_support_contact";
            this.linkLabel_support_contact.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Contact_LinkClicked);
            // 
            // btn_showStateMachines
            // 
            this.btn_showStateMachines.Location = new System.Drawing.Point(122, 127);
            this.btn_showStateMachines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_showStateMachines.Name = "btn_showStateMachines";
            this.btn_showStateMachines.Size = new System.Drawing.Size(99, 31);
            this.btn_showStateMachines.TabIndex = 6;
            this.btn_showStateMachines.Text = "Show FSMs";
            this.btn_showStateMachines.UseVisualStyleBackColor = true;
            this.btn_showStateMachines.Click += new System.EventHandler(this.ShowStateMachines_Click);
            // 
            // btn_showConsole
            // 
            this.btn_showConsole.Location = new System.Drawing.Point(15, 127);
            this.btn_showConsole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_showConsole.Name = "btn_showConsole";
            this.btn_showConsole.Size = new System.Drawing.Size(99, 31);
            this.btn_showConsole.TabIndex = 7;
            this.btn_showConsole.Text = "Show Console";
            this.btn_showConsole.UseVisualStyleBackColor = true;
            this.btn_showConsole.Click += new System.EventHandler(this.ShowConsole_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(273, 168);
            this.Controls.Add(this.btn_showConsole);
            this.Controls.Add(this.btn_showStateMachines);
            this.Controls.Add(this.linkLabel_support_contact);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.linkLabel_url);
            this.Controls.Add(this.label_version_value);
            this.Controls.Add(this.label_header);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 7, 7);
            this.ShowIcon = false;
            this.Text = "About";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_header;
        private System.Windows.Forms.Label label_version_value;
        private System.Windows.Forms.LinkLabel linkLabel_url;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.LinkLabel linkLabel_support_contact;
        private System.Windows.Forms.Button btn_showStateMachines;
        private System.Windows.Forms.Button btn_showConsole;
    }
}