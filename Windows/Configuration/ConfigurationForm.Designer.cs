namespace Shirehorse.Core.Configuration
{
    partial class ConfigurationForm
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
            this.ConfigPanel = new Shirehorse.Core.Configuration.ConfigPanel();
            this.SuspendLayout();
            // 
            // ConfigPanel
            // 
            this.ConfigPanel.AutoSize = true;
            this.ConfigPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.ConfigPanel.Name = "ConfigPanel";
            this.ConfigPanel.Size = new System.Drawing.Size(542, 43);
            this.ConfigPanel.TabIndex = 0;
            // 
            // form_options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(542, 43);
            this.Controls.Add(this.ConfigPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "form_options";
            this.ShowIcon = false;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Shirehorse.Core.Configuration.ConfigPanel ConfigPanel;
    }
}