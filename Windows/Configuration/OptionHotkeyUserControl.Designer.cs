namespace Shirehorse.Core
{
    partial class Hotkey_Control
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
            this.comboBox_keys = new System.Windows.Forms.ComboBox();
            this.comboBox_action = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.label_key = new System.Windows.Forms.Label();
            this.label_action = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_keys
            // 
            this.comboBox_keys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_keys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_keys.FormattingEnabled = true;
            this.comboBox_keys.Location = new System.Drawing.Point(180, 9);
            this.comboBox_keys.Name = "comboBox_keys";
            this.comboBox_keys.Size = new System.Drawing.Size(106, 21);
            this.comboBox_keys.TabIndex = 5;
            this.comboBox_keys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
            // 
            // comboBox_action
            // 
            this.comboBox_action.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_action.FormattingEnabled = true;
            this.comboBox_action.Location = new System.Drawing.Point(335, 9);
            this.comboBox_action.Name = "comboBox_action";
            this.comboBox_action.Size = new System.Drawing.Size(166, 21);
            this.comboBox_action.TabIndex = 6;
            this.comboBox_action.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
            // 
            // checkedListBox
            // 
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.ColumnWidth = 60;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox.MultiColumn = true;
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(140, 34);
            this.checkedListBox.TabIndex = 7;
            // 
            // label_key
            // 
            this.label_key.AutoSize = true;
            this.label_key.Location = new System.Drawing.Point(149, 12);
            this.label_key.Name = "label_key";
            this.label_key.Size = new System.Drawing.Size(25, 13);
            this.label_key.TabIndex = 8;
            this.label_key.Text = "Key";
            // 
            // label_action
            // 
            this.label_action.AutoSize = true;
            this.label_action.Location = new System.Drawing.Point(292, 12);
            this.label_action.Name = "label_action";
            this.label_action.Size = new System.Drawing.Size(37, 13);
            this.label_action.TabIndex = 9;
            this.label_action.Text = "Action";
            // 
            // Hotkey_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_action);
            this.Controls.Add(this.label_key);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.comboBox_action);
            this.Controls.Add(this.comboBox_keys);
            this.Name = "Hotkey_Control";
            this.Size = new System.Drawing.Size(539, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_keys;
        private System.Windows.Forms.ComboBox comboBox_action;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Label label_key;
        private System.Windows.Forms.Label label_action;
    }
}
