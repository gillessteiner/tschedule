namespace TSchedule2.Views
{
   partial class ProblemView
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
         this.txtNbResources = new System.Windows.Forms.TextBox();
         this.label5 = new System.Windows.Forms.Label();
         this.txtNbTrains = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.txtHash = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.grpGraph = new System.Windows.Forms.GroupBox();
         this.panGraphViewContainer = new System.Windows.Forms.Panel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.cboPathSelection = new System.Windows.Forms.ComboBox();
         this.txtNbPossiblePaths = new System.Windows.Forms.TextBox();
         this.label7 = new System.Windows.Forms.Label();
         this.cboRouteSelection = new System.Windows.Forms.ComboBox();
         this.label6 = new System.Windows.Forms.Label();
         this.txtNbRoutes = new System.Windows.Forms.TextBox();
         this.grpDefinition.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.grpGraph.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // grpDefinition
         // 
         this.grpDefinition.Controls.Add(this.txtNbResources);
         this.grpDefinition.Controls.Add(this.label5);
         this.grpDefinition.Controls.Add(this.txtNbTrains);
         this.grpDefinition.Controls.Add(this.label3);
         this.grpDefinition.Controls.Add(this.txtHash);
         this.grpDefinition.Controls.Add(this.label2);
         this.grpDefinition.Size = new System.Drawing.Size(750, 172);
         this.grpDefinition.Controls.SetChildIndex(this.pictureBox1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.labPb, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label1, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtLabel, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label2, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtHash, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label3, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtNbTrains, 0);
         this.grpDefinition.Controls.SetChildIndex(this.label5, 0);
         this.grpDefinition.Controls.SetChildIndex(this.txtNbResources, 0);
         // 
         // txtLabel
         // 
         this.txtLabel.Size = new System.Drawing.Size(574, 22);
         // 
         // txtNbResources
         // 
         this.txtNbResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtNbResources.Location = new System.Drawing.Point(155, 128);
         this.txtNbResources.Name = "txtNbResources";
         this.txtNbResources.ReadOnly = true;
         this.txtNbResources.Size = new System.Drawing.Size(574, 22);
         this.txtNbResources.TabIndex = 30;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(65, 131);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(84, 17);
         this.label5.TabIndex = 29;
         this.label5.Text = "Resources :";
         // 
         // txtNbTrains
         // 
         this.txtNbTrains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtNbTrains.Location = new System.Drawing.Point(155, 96);
         this.txtNbTrains.Name = "txtNbTrains";
         this.txtNbTrains.ReadOnly = true;
         this.txtNbTrains.Size = new System.Drawing.Size(574, 22);
         this.txtNbTrains.TabIndex = 27;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(93, 99);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(56, 17);
         this.label3.TabIndex = 26;
         this.label3.Text = "Trains :";
         // 
         // txtHash
         // 
         this.txtHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtHash.Location = new System.Drawing.Point(155, 64);
         this.txtHash.Name = "txtHash";
         this.txtHash.ReadOnly = true;
         this.txtHash.Size = new System.Drawing.Size(574, 22);
         this.txtHash.TabIndex = 25;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(100, 67);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(49, 17);
         this.label2.TabIndex = 24;
         this.label2.Text = "Hash :";
         // 
         // grpGraph
         // 
         this.grpGraph.Controls.Add(this.panGraphViewContainer);
         this.grpGraph.Controls.Add(this.panel1);
         this.grpGraph.Dock = System.Windows.Forms.DockStyle.Fill;
         this.grpGraph.Enabled = false;
         this.grpGraph.Location = new System.Drawing.Point(0, 172);
         this.grpGraph.Name = "grpGraph";
         this.grpGraph.Size = new System.Drawing.Size(750, 446);
         this.grpGraph.TabIndex = 3;
         this.grpGraph.TabStop = false;
         this.grpGraph.Text = "Graph";
         // 
         // panGraphViewContainer
         // 
         this.panGraphViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panGraphViewContainer.Location = new System.Drawing.Point(3, 52);
         this.panGraphViewContainer.Name = "panGraphViewContainer";
         this.panGraphViewContainer.Size = new System.Drawing.Size(744, 391);
         this.panGraphViewContainer.TabIndex = 1;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.cboPathSelection);
         this.panel1.Controls.Add(this.txtNbPossiblePaths);
         this.panel1.Controls.Add(this.label7);
         this.panel1.Controls.Add(this.cboRouteSelection);
         this.panel1.Controls.Add(this.label6);
         this.panel1.Controls.Add(this.txtNbRoutes);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(3, 18);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(744, 34);
         this.panel1.TabIndex = 0;
         // 
         // cboPathSelection
         // 
         this.cboPathSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboPathSelection.FormattingEnabled = true;
         this.cboPathSelection.Location = new System.Drawing.Point(505, 5);
         this.cboPathSelection.Name = "cboPathSelection";
         this.cboPathSelection.Size = new System.Drawing.Size(221, 24);
         this.cboPathSelection.TabIndex = 25;
         this.cboPathSelection.SelectionChangeCommitted += new System.EventHandler(this.cboPathSelection_SelectionChangeCommitted);
         // 
         // txtNbPossiblePaths
         // 
         this.txtNbPossiblePaths.Location = new System.Drawing.Point(420, 6);
         this.txtNbPossiblePaths.Name = "txtNbPossiblePaths";
         this.txtNbPossiblePaths.ReadOnly = true;
         this.txtNbPossiblePaths.Size = new System.Drawing.Size(79, 22);
         this.txtNbPossiblePaths.TabIndex = 24;
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(310, 8);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(104, 17);
         this.label7.TabIndex = 23;
         this.label7.Text = "Possible paths:";
         // 
         // cboRouteSelection
         // 
         this.cboRouteSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboRouteSelection.FormattingEnabled = true;
         this.cboRouteSelection.Location = new System.Drawing.Point(152, 5);
         this.cboRouteSelection.Name = "cboRouteSelection";
         this.cboRouteSelection.Size = new System.Drawing.Size(152, 24);
         this.cboRouteSelection.TabIndex = 22;
         this.cboRouteSelection.SelectionChangeCommitted += new System.EventHandler(this.cboRouteSelection_SelectionChangeCommitted);
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(8, 8);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(53, 17);
         this.label6.TabIndex = 21;
         this.label6.Text = "Routes";
         // 
         // txtNbRoutes
         // 
         this.txtNbRoutes.Location = new System.Drawing.Point(67, 5);
         this.txtNbRoutes.Name = "txtNbRoutes";
         this.txtNbRoutes.ReadOnly = true;
         this.txtNbRoutes.Size = new System.Drawing.Size(79, 22);
         this.txtNbRoutes.TabIndex = 21;
         this.txtNbRoutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // ProblemView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.grpGraph);
         this.Name = "ProblemView";
         this.Controls.SetChildIndex(this.grpDefinition, 0);
         this.Controls.SetChildIndex(this.grpGraph, 0);
         this.grpDefinition.ResumeLayout(false);
         this.grpDefinition.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.grpGraph.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TextBox txtNbResources;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox txtNbTrains;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox txtHash;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox grpGraph;
      private System.Windows.Forms.Panel panGraphViewContainer;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.ComboBox cboPathSelection;
      private System.Windows.Forms.TextBox txtNbPossiblePaths;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.ComboBox cboRouteSelection;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TextBox txtNbRoutes;
   }
}
