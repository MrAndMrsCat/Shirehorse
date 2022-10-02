
namespace Shirehorse.Core.FiniteStateMachines
{
    partial class FiniteStateMachineUserControl
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
            this.grid_exitStates = new System.Windows.Forms.DataGridView();
            this.grid_entryStates = new System.Windows.Forms.DataGridView();
            this.StatesGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel_master = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_grids = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_transitions = new System.Windows.Forms.TableLayoutPanel();
            this.panel_controls = new System.Windows.Forms.Panel();
            this.button_reset = new System.Windows.Forms.Button();
            this.checkBox_selectFirstTransitionOnStateChange = new System.Windows.Forms.CheckBox();
            this.checkBox_selectStateOnChange = new System.Windows.Forms.CheckBox();
            this.button_startTransition = new System.Windows.Forms.Button();
            this.checkBox_pollingPaused = new System.Windows.Forms.CheckBox();
            this.Button_ForceStateChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid_exitStates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_entryStates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).BeginInit();
            this.tableLayoutPanel_master.SuspendLayout();
            this.tableLayoutPanel_grids.SuspendLayout();
            this.tableLayoutPanel_transitions.SuspendLayout();
            this.panel_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid_exitStates
            // 
            this.grid_exitStates.AllowUserToAddRows = false;
            this.grid_exitStates.AllowUserToDeleteRows = false;
            this.grid_exitStates.AllowUserToResizeRows = false;
            this.grid_exitStates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_exitStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_exitStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_exitStates.Location = new System.Drawing.Point(4, 245);
            this.grid_exitStates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grid_exitStates.MultiSelect = false;
            this.grid_exitStates.Name = "grid_exitStates";
            this.grid_exitStates.ReadOnly = true;
            this.grid_exitStates.RowHeadersVisible = false;
            this.grid_exitStates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_exitStates.Size = new System.Drawing.Size(434, 237);
            this.grid_exitStates.TabIndex = 2;
            this.grid_exitStates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExitStatesGrid_CellClick);
            // 
            // grid_entryStates
            // 
            this.grid_entryStates.AllowUserToAddRows = false;
            this.grid_entryStates.AllowUserToDeleteRows = false;
            this.grid_entryStates.AllowUserToResizeRows = false;
            this.grid_entryStates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_entryStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_entryStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_entryStates.Location = new System.Drawing.Point(4, 3);
            this.grid_entryStates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grid_entryStates.MultiSelect = false;
            this.grid_entryStates.Name = "grid_entryStates";
            this.grid_entryStates.ReadOnly = true;
            this.grid_entryStates.RowHeadersVisible = false;
            this.grid_entryStates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_entryStates.Size = new System.Drawing.Size(434, 236);
            this.grid_entryStates.TabIndex = 1;
            // 
            // StatesGrid
            // 
            this.StatesGrid.AllowUserToAddRows = false;
            this.StatesGrid.AllowUserToDeleteRows = false;
            this.StatesGrid.AllowUserToResizeRows = false;
            this.StatesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatesGrid.Location = new System.Drawing.Point(4, 3);
            this.StatesGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.StatesGrid.MultiSelect = false;
            this.StatesGrid.Name = "StatesGrid";
            this.StatesGrid.ReadOnly = true;
            this.StatesGrid.RowHeadersVisible = false;
            this.StatesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatesGrid.Size = new System.Drawing.Size(442, 485);
            this.StatesGrid.TabIndex = 0;
            this.StatesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StatesGrid_CellClick);
            // 
            // tableLayoutPanel_master
            // 
            this.tableLayoutPanel_master.ColumnCount = 1;
            this.tableLayoutPanel_master.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_master.Controls.Add(this.tableLayoutPanel_grids, 0, 0);
            this.tableLayoutPanel_master.Controls.Add(this.panel_controls, 0, 1);
            this.tableLayoutPanel_master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_master.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_master.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel_master.Name = "tableLayoutPanel_master";
            this.tableLayoutPanel_master.RowCount = 2;
            this.tableLayoutPanel_master.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_master.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel_master.Size = new System.Drawing.Size(908, 555);
            this.tableLayoutPanel_master.TabIndex = 2;
            // 
            // tableLayoutPanel_grids
            // 
            this.tableLayoutPanel_grids.ColumnCount = 2;
            this.tableLayoutPanel_grids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_grids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_grids.Controls.Add(this.tableLayoutPanel_transitions, 1, 0);
            this.tableLayoutPanel_grids.Controls.Add(this.StatesGrid, 0, 0);
            this.tableLayoutPanel_grids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_grids.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel_grids.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel_grids.Name = "tableLayoutPanel_grids";
            this.tableLayoutPanel_grids.RowCount = 1;
            this.tableLayoutPanel_grids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_grids.Size = new System.Drawing.Size(900, 491);
            this.tableLayoutPanel_grids.TabIndex = 0;
            // 
            // tableLayoutPanel_transitions
            // 
            this.tableLayoutPanel_transitions.ColumnCount = 1;
            this.tableLayoutPanel_transitions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_transitions.Controls.Add(this.grid_exitStates, 0, 1);
            this.tableLayoutPanel_transitions.Controls.Add(this.grid_entryStates, 0, 0);
            this.tableLayoutPanel_transitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_transitions.Location = new System.Drawing.Point(454, 3);
            this.tableLayoutPanel_transitions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel_transitions.Name = "tableLayoutPanel_transitions";
            this.tableLayoutPanel_transitions.RowCount = 2;
            this.tableLayoutPanel_transitions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_transitions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_transitions.Size = new System.Drawing.Size(442, 485);
            this.tableLayoutPanel_transitions.TabIndex = 0;
            // 
            // panel_controls
            // 
            this.panel_controls.Controls.Add(this.button_reset);
            this.panel_controls.Controls.Add(this.checkBox_selectFirstTransitionOnStateChange);
            this.panel_controls.Controls.Add(this.checkBox_selectStateOnChange);
            this.panel_controls.Controls.Add(this.button_startTransition);
            this.panel_controls.Controls.Add(this.checkBox_pollingPaused);
            this.panel_controls.Controls.Add(this.Button_ForceStateChange);
            this.panel_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_controls.Location = new System.Drawing.Point(4, 500);
            this.panel_controls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(900, 52);
            this.panel_controls.TabIndex = 1;
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(4, 3);
            this.button_reset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(92, 44);
            this.button_reset.TabIndex = 5;
            this.button_reset.Text = "Reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // checkBox_selectFirstTransitionOnStateChange
            // 
            this.checkBox_selectFirstTransitionOnStateChange.AutoSize = true;
            this.checkBox_selectFirstTransitionOnStateChange.Checked = true;
            this.checkBox_selectFirstTransitionOnStateChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_selectFirstTransitionOnStateChange.Location = new System.Drawing.Point(354, 28);
            this.checkBox_selectFirstTransitionOnStateChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_selectFirstTransitionOnStateChange.Name = "checkBox_selectFirstTransitionOnStateChange";
            this.checkBox_selectFirstTransitionOnStateChange.Size = new System.Drawing.Size(191, 19);
            this.checkBox_selectFirstTransitionOnStateChange.TabIndex = 4;
            this.checkBox_selectFirstTransitionOnStateChange.Text = "Select transition when changed";
            this.checkBox_selectFirstTransitionOnStateChange.UseVisualStyleBackColor = true;
            // 
            // checkBox_selectStateOnChange
            // 
            this.checkBox_selectStateOnChange.AutoSize = true;
            this.checkBox_selectStateOnChange.Checked = true;
            this.checkBox_selectStateOnChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_selectStateOnChange.Location = new System.Drawing.Point(354, 3);
            this.checkBox_selectStateOnChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_selectStateOnChange.Name = "checkBox_selectStateOnChange";
            this.checkBox_selectStateOnChange.Size = new System.Drawing.Size(191, 19);
            this.checkBox_selectStateOnChange.TabIndex = 3;
            this.checkBox_selectStateOnChange.Text = "Select new state when changed";
            this.checkBox_selectStateOnChange.UseVisualStyleBackColor = true;
            // 
            // button_startTransition
            // 
            this.button_startTransition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_startTransition.Location = new System.Drawing.Point(777, 3);
            this.button_startTransition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_startTransition.Name = "button_startTransition";
            this.button_startTransition.Size = new System.Drawing.Size(124, 44);
            this.button_startTransition.TabIndex = 2;
            this.button_startTransition.Text = "Start Transition";
            this.button_startTransition.UseVisualStyleBackColor = true;
            this.button_startTransition.Click += new System.EventHandler(this.StartTransition_Click);
            // 
            // checkBox_pollingPaused
            // 
            this.checkBox_pollingPaused.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_pollingPaused.Location = new System.Drawing.Point(241, 3);
            this.checkBox_pollingPaused.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_pollingPaused.Name = "checkBox_pollingPaused";
            this.checkBox_pollingPaused.Size = new System.Drawing.Size(105, 44);
            this.checkBox_pollingPaused.TabIndex = 1;
            this.checkBox_pollingPaused.Text = "Pause Polling";
            this.checkBox_pollingPaused.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_pollingPaused.UseVisualStyleBackColor = true;
            // 
            // Button_ForceStateChange
            // 
            this.Button_ForceStateChange.Enabled = false;
            this.Button_ForceStateChange.Location = new System.Drawing.Point(103, 3);
            this.Button_ForceStateChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button_ForceStateChange.Name = "Button_ForceStateChange";
            this.Button_ForceStateChange.Size = new System.Drawing.Size(132, 44);
            this.Button_ForceStateChange.TabIndex = 0;
            this.Button_ForceStateChange.Text = "Force State Change";
            this.Button_ForceStateChange.UseVisualStyleBackColor = true;
            this.Button_ForceStateChange.Click += new System.EventHandler(this.ForceStateChange_Click);
            // 
            // FiniteStateMachineUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_master);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FiniteStateMachineUserControl";
            this.Size = new System.Drawing.Size(908, 555);
            ((System.ComponentModel.ISupportInitialize)(this.grid_exitStates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_entryStates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).EndInit();
            this.tableLayoutPanel_master.ResumeLayout(false);
            this.tableLayoutPanel_grids.ResumeLayout(false);
            this.tableLayoutPanel_transitions.ResumeLayout(false);
            this.panel_controls.ResumeLayout(false);
            this.panel_controls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView grid_exitStates;
        private System.Windows.Forms.DataGridView grid_entryStates;
        private System.Windows.Forms.DataGridView StatesGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_master;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_grids;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_transitions;
        private System.Windows.Forms.Panel panel_controls;
        private System.Windows.Forms.Button Button_ForceStateChange;
        private System.Windows.Forms.CheckBox checkBox_pollingPaused;
        private System.Windows.Forms.Button button_startTransition;
        private System.Windows.Forms.CheckBox checkBox_selectStateOnChange;
        private System.Windows.Forms.CheckBox checkBox_selectFirstTransitionOnStateChange;
        private System.Windows.Forms.Button button_reset;
    }
}
