﻿namespace TSchedule2.Views
{
   sealed partial class SolutionView
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
         this.picValidation = new System.Windows.Forms.PictureBox();
         this.txtValidationError = new System.Windows.Forms.TextBox();
         this.btnValidate = new System.Windows.Forms.Button();
         this.btnSave = new System.Windows.Forms.Button();
         this.txtObjValue = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.grpGraph = new System.Windows.Forms.GroupBox();
         this.panGraphViewContainer = new System.Windows.Forms.Panel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.cboTrainSelection = new System.Windows.Forms.ComboBox();
         this.label6 = new System.Windows.Forms.Label();
         this.txtNbTrains = new System.Windows.Forms.TextBox();
         this.grpDefinition.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.picValidation)).BeginInit();
         this.grpGraph.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // grpDefinition
         // 
         this.grpDefinition.Controls.Add(this.picValidation);
         this.grpDefinition.Controls.Add(this.txtValidationError);
         this.grpDefinition.Controls.Add(this.btnValidate);
         this.grpDefinition.Controls.Add(this.btnSave);
         this.grpDefinition.Controls.Add(this.txtObjValue);
         this.grpDefinition.Controls.Add(this.label2);
         this.grpDefinition.Size = new System.Drawing.Size(711, 182);
         this.grpDefinition.Controls.SetChildIndex(this.pictureBox1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.labPb, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtLabel, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label2, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtObjValue, 0);
         this.grpDefinition.Controls.SetChildIndex(this.btnSave, 0);
         this.grpDefinition.Controls.SetChildIndex(this.btnValidate, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtValidationError, 0);
         this.grpDefinition.Controls.SetChildIndex(this.picValidation, 0);
         // 
         // txtLabel
         // 
         this.txtLabel.Size = new System.Drawing.Size(521, 22);
         // 
         // labPb
         // 
         this.labPb.Text = "Solution";
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = global::TSchedule2.Properties.Resources.solution_48;
         // 
         // picValidation
         // 
         this.picValidation.Location = new System.Drawing.Point(127, 135);
         this.picValidation.Name = "picValidation";
         this.picValidation.Size = new System.Drawing.Size(22, 22);
         this.picValidation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.picValidation.TabIndex = 34;
         this.picValidation.TabStop = false;
         this.picValidation.Visible = false;
         // 
         // txtValidationError
         // 
         this.txtValidationError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtValidationError.Location = new System.Drawing.Point(155, 135);
         this.txtValidationError.Name = "txtValidationError";
         this.txtValidationError.ReadOnly = true;
         this.txtValidationError.Size = new System.Drawing.Size(521, 22);
         this.txtValidationError.TabIndex = 33;
         this.txtValidationError.Visible = false;
         // 
         // btnValidate
         // 
         this.btnValidate.Location = new System.Drawing.Point(334, 96);
         this.btnValidate.Margin = new System.Windows.Forms.Padding(0);
         this.btnValidate.Name = "btnValidate";
         this.btnValidate.Size = new System.Drawing.Size(168, 31);
         this.btnValidate.TabIndex = 32;
         this.btnValidate.Text = "Validate solution";
         this.btnValidate.UseVisualStyleBackColor = true;
         this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
         // 
         // btnSave
         // 
         this.btnSave.Location = new System.Drawing.Point(155, 96);
         this.btnSave.Margin = new System.Windows.Forms.Padding(0);
         this.btnSave.Name = "btnSave";
         this.btnSave.Size = new System.Drawing.Size(168, 31);
         this.btnSave.TabIndex = 31;
         this.btnSave.Text = "Save solution ...";
         this.btnSave.UseVisualStyleBackColor = true;
         this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
         // 
         // txtObjValue
         // 
         this.txtObjValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtObjValue.Location = new System.Drawing.Point(155, 61);
         this.txtObjValue.Name = "txtObjValue";
         this.txtObjValue.ReadOnly = true;
         this.txtObjValue.Size = new System.Drawing.Size(521, 22);
         this.txtObjValue.TabIndex = 30;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(79, 64);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(70, 17);
         this.label2.TabIndex = 29;
         this.label2.Text = "ObjValue:";
         // 
         // grpGraph
         // 
         this.grpGraph.Controls.Add(this.panGraphViewContainer);
         this.grpGraph.Controls.Add(this.panel1);
         this.grpGraph.Dock = System.Windows.Forms.DockStyle.Fill;
         this.grpGraph.Enabled = false;
         this.grpGraph.Location = new System.Drawing.Point(0, 182);
         this.grpGraph.Name = "grpGraph";
         this.grpGraph.Size = new System.Drawing.Size(711, 436);
         this.grpGraph.TabIndex = 3;
         this.grpGraph.TabStop = false;
         this.grpGraph.Text = "Graph";
         // 
         // panGraphViewContainer
         // 
         this.panGraphViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panGraphViewContainer.Location = new System.Drawing.Point(3, 52);
         this.panGraphViewContainer.Name = "panGraphViewContainer";
         this.panGraphViewContainer.Size = new System.Drawing.Size(705, 381);
         this.panGraphViewContainer.TabIndex = 1;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.cboTrainSelection);
         this.panel1.Controls.Add(this.label6);
         this.panel1.Controls.Add(this.txtNbTrains);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(3, 18);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(705, 34);
         this.panel1.TabIndex = 0;
         // 
         // cboTrainSelection
         // 
         this.cboTrainSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboTrainSelection.FormattingEnabled = true;
         this.cboTrainSelection.Location = new System.Drawing.Point(152, 5);
         this.cboTrainSelection.Name = "cboTrainSelection";
         this.cboTrainSelection.Size = new System.Drawing.Size(152, 24);
         this.cboTrainSelection.TabIndex = 22;
         this.cboTrainSelection.SelectionChangeCommitted += new System.EventHandler(this.cboTrainSelection_SelectionChangeCommitted);
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(8, 8);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(48, 17);
         this.label6.TabIndex = 21;
         this.label6.Text = "Trains";
         // 
         // txtNbTrains
         // 
         this.txtNbTrains.Location = new System.Drawing.Point(67, 5);
         this.txtNbTrains.Name = "txtNbTrains";
         this.txtNbTrains.ReadOnly = true;
         this.txtNbTrains.Size = new System.Drawing.Size(79, 22);
         this.txtNbTrains.TabIndex = 21;
         this.txtNbTrains.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // SolutionView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.grpGraph);
         this.Name = "SolutionView";
         this.Size = new System.Drawing.Size(711, 618);
         this.Controls.SetChildIndex(this.grpDefinition, 0);
         this.Controls.SetChildIndex(this.grpGraph, 0);
         this.grpDefinition.ResumeLayout(false);
         this.grpDefinition.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.picValidation)).EndInit();
         this.grpGraph.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion
      private System.Windows.Forms.PictureBox picValidation;
      private System.Windows.Forms.TextBox txtValidationError;
      private System.Windows.Forms.Button btnValidate;
      private System.Windows.Forms.Button btnSave;
      private System.Windows.Forms.TextBox txtObjValue;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox grpGraph;
      private System.Windows.Forms.Panel panGraphViewContainer;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.ComboBox cboTrainSelection;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TextBox txtNbTrains;
   }
}