
namespace Shirehorse.Core.FiniteStateMachines
{
    partial class FiniteStateMachineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiniteStateMachineForm));
            this.FSMUserControl = new FiniteStateMachineUserControl();
            this.SuspendLayout();
            // 
            // FSMUserControl
            // 
            this.FSMUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSMUserControl.Location = new System.Drawing.Point(0, 0);
            this.FSMUserControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.FSMUserControl.Name = "FSMUserControl";
            this.FSMUserControl.Size = new System.Drawing.Size(1067, 554);
            this.FSMUserControl.TabIndex = 0;
            // 
            // FiniteStateMachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.FSMUserControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FiniteStateMachineForm";
            this.Text = "Finite State Machine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FiniteStateMachineForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public FiniteStateMachineUserControl FSMUserControl;
    }
}