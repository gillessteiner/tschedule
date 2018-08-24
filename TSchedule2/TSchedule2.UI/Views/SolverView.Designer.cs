namespace TSchedule2.Views
{
   partial class SolverView
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.numSubIter = new System.Windows.Forms.NumericUpDown();
         this.labSubIter = new System.Windows.Forms.Label();
         this.btnSolve = new System.Windows.Forms.Button();
         this.numMaxIter = new System.Windows.Forms.NumericUpDown();
         this.label4 = new System.Windows.Forms.Label();
         this.cboSolverList = new System.Windows.Forms.ComboBox();
         this.label2 = new System.Windows.Forms.Label();
         this.grpLog = new System.Windows.Forms.GroupBox();
         this.logSolver = new TSchedule2.Views.LogView();
         this.grpDefinition.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numSubIter)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numMaxIter)).BeginInit();
         this.grpLog.SuspendLayout();
         this.SuspendLayout();
         // 
         // grpDefinition
         // 
         this.grpDefinition.Controls.Add(this.numSubIter);
         this.grpDefinition.Controls.Add(this.labSubIter);
         this.grpDefinition.Controls.Add(this.btnSolve);
         this.grpDefinition.Controls.Add(this.numMaxIter);
         this.grpDefinition.Controls.Add(this.label4);
         this.grpDefinition.Controls.Add(this.cboSolverList);
         this.grpDefinition.Controls.Add(this.label2);
         this.grpDefinition.Size = new System.Drawing.Size(750, 182);
         this.grpDefinition.Controls.SetChildIndex(this.pictureBox1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.labPb, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtLabel, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label2, 0);
         this.grpDefinition.Controls.SetChildIndex(this.cboSolverList, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label4, 0);
         this.grpDefinition.Controls.SetChildIndex(this.numMaxIter, 0);
         this.grpDefinition.Controls.SetChildIndex(this.btnSolve, 0);
         this.grpDefinition.Controls.SetChildIndex(this.labSubIter, 0);
         this.grpDefinition.Controls.SetChildIndex(this.numSubIter, 0);
         // 
         // labPb
         // 
         this.labPb.Size = new System.Drawing.Size(54, 17);
         this.labPb.Text = "Solver";
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = global::TSchedule2.Properties.Resources.solver_48;
         // 
         // numSubIter
         // 
         this.numSubIter.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
         this.numSubIter.Location = new System.Drawing.Point(418, 99);
         this.numSubIter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this.numSubIter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numSubIter.Name = "numSubIter";
         this.numSubIter.Size = new System.Drawing.Size(168, 22);
         this.numSubIter.TabIndex = 32;
         this.numSubIter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.numSubIter.ThousandsSeparator = true;
         this.numSubIter.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
         this.numSubIter.Visible = false;
         // 
         // labSubIter
         // 
         this.labSubIter.AutoSize = true;
         this.labSubIter.Location = new System.Drawing.Point(354, 101);
         this.labSubIter.Name = "labSubIter";
         this.labSubIter.Size = new System.Drawing.Size(55, 17);
         this.labSubIter.TabIndex = 31;
         this.labSubIter.Text = "subIter:";
         this.labSubIter.Visible = false;
         // 
         // btnSolve
         // 
         this.btnSolve.Location = new System.Drawing.Point(156, 131);
         this.btnSolve.Margin = new System.Windows.Forms.Padding(0);
         this.btnSolve.Name = "btnSolve";
         this.btnSolve.Size = new System.Drawing.Size(168, 31);
         this.btnSolve.TabIndex = 30;
         this.btnSolve.Text = "Run solver";
         this.btnSolve.UseVisualStyleBackColor = true;
         this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
         // 
         // numMaxIter
         // 
         this.numMaxIter.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
         this.numMaxIter.Location = new System.Drawing.Point(156, 99);
         this.numMaxIter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this.numMaxIter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numMaxIter.Name = "numMaxIter";
         this.numMaxIter.Size = new System.Drawing.Size(168, 22);
         this.numMaxIter.TabIndex = 29;
         this.numMaxIter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.numMaxIter.ThousandsSeparator = true;
         this.numMaxIter.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(92, 101);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(57, 17);
         this.label4.TabIndex = 28;
         this.label4.Text = "maxIter:";
         // 
         // cboSolverList
         // 
         this.cboSolverList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboSolverList.FormattingEnabled = true;
         this.cboSolverList.Location = new System.Drawing.Point(155, 67);
         this.cboSolverList.Name = "cboSolverList";
         this.cboSolverList.Size = new System.Drawing.Size(169, 24);
         this.cboSolverList.TabIndex = 27;
         this.cboSolverList.SelectionChangeCommitted += new System.EventHandler(this.cboSolverList_SelectionChangeCommitted);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(99, 70);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(50, 17);
         this.label2.TabIndex = 26;
         this.label2.Text = "solver:";
         // 
         // grpLog
         // 
         this.grpLog.Controls.Add(this.logSolver);
         this.grpLog.Dock = System.Windows.Forms.DockStyle.Fill;
         this.grpLog.Location = new System.Drawing.Point(0, 182);
         this.grpLog.Name = "grpLog";
         this.grpLog.Size = new System.Drawing.Size(750, 436);
         this.grpLog.TabIndex = 3;
         this.grpLog.TabStop = false;
         // 
         // logSolver
         // 
         this.logSolver.Dock = System.Windows.Forms.DockStyle.Fill;
         this.logSolver.Location = new System.Drawing.Point(3, 18);
         this.logSolver.Name = "logSolver";
         this.logSolver.Size = new System.Drawing.Size(744, 415);
         this.logSolver.TabIndex = 0;
         // 
         // SolverView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.grpLog);
         this.Name = "SolverView";
         this.Controls.SetChildIndex(this.grpDefinition, 0);
         this.Controls.SetChildIndex(this.grpLog, 0);
         this.grpDefinition.ResumeLayout(false);
         this.grpDefinition.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numSubIter)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numMaxIter)).EndInit();
         this.grpLog.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.NumericUpDown numSubIter;
      private System.Windows.Forms.Label labSubIter;
      private System.Windows.Forms.Button btnSolve;
      private System.Windows.Forms.NumericUpDown numMaxIter;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox cboSolverList;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox grpLog;
      private LogView logSolver;
   }
}
