﻿namespace TrainScheduler.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolverView));
            this.label2 = new System.Windows.Forms.Label();
            this.cboSolverList = new System.Windows.Forms.ComboBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.logSolver = new TrainScheduler.UI.LogView();
            this.label4 = new System.Windows.Forms.Label();
            this.numMaxIter = new System.Windows.Forms.NumericUpDown();
            this.grpDefinition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIter)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDefinition
            // 
            this.grpDefinition.Controls.Add(this.numMaxIter);
            this.grpDefinition.Controls.Add(this.label4);
            this.grpDefinition.Controls.Add(this.cboSolverList);
            this.grpDefinition.Controls.Add(this.label2);
            this.grpDefinition.Size = new System.Drawing.Size(670, 227);
            this.grpDefinition.Controls.SetChildIndex(this.pictureBox1, 0);
            this.grpDefinition.Controls.SetChildIndex(this.labPb, 0);
            this.grpDefinition.Controls.SetChildIndex(this.label1, 0);
            this.grpDefinition.Controls.SetChildIndex(this.txtLabel, 0);
            this.grpDefinition.Controls.SetChildIndex(this.label2, 0);
            this.grpDefinition.Controls.SetChildIndex(this.cboSolverList, 0);
            this.grpDefinition.Controls.SetChildIndex(this.label4, 0);
            this.grpDefinition.Controls.SetChildIndex(this.numMaxIter, 0);
            // 
            // labPb
            // 
            this.labPb.Size = new System.Drawing.Size(54, 17);
            this.labPb.Text = "Solver";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "solver:";
            // 
            // cboSolverList
            // 
            this.cboSolverList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSolverList.FormattingEnabled = true;
            this.cboSolverList.Location = new System.Drawing.Point(155, 64);
            this.cboSolverList.Name = "cboSolverList";
            this.cboSolverList.Size = new System.Drawing.Size(169, 24);
            this.cboSolverList.TabIndex = 17;
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.logSolver);
            this.grpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLog.Location = new System.Drawing.Point(0, 227);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(670, 281);
            this.grpLog.TabIndex = 2;
            this.grpLog.TabStop = false;
            // 
            // logSolver
            // 
            this.logSolver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logSolver.Location = new System.Drawing.Point(3, 18);
            this.logSolver.Name = "logSolver";
            this.logSolver.Size = new System.Drawing.Size(664, 260);
            this.logSolver.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "maxIter:";
            // 
            // numMaxIter
            // 
            this.numMaxIter.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxIter.Location = new System.Drawing.Point(156, 96);
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
            this.numMaxIter.TabIndex = 21;
            this.numMaxIter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMaxIter.ThousandsSeparator = true;
            this.numMaxIter.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // SolverView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpLog);
            this.Name = "SolverView";
            this.Size = new System.Drawing.Size(670, 508);
            this.Controls.SetChildIndex(this.grpDefinition, 0);
            this.Controls.SetChildIndex(this.grpLog, 0);
            this.grpDefinition.ResumeLayout(false);
            this.grpDefinition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSolverList;
        private System.Windows.Forms.GroupBox grpLog;
        private LogView logSolver;
        private System.Windows.Forms.NumericUpDown numMaxIter;
        private System.Windows.Forms.Label label4;
    }
}