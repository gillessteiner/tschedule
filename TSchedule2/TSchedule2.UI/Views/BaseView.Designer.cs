namespace TSchedule2.Views
{
    partial class BaseView
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
            this.grpDefinition = new System.Windows.Forms.GroupBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labPb = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpDefinition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDefinition
            // 
            this.grpDefinition.Controls.Add(this.txtLabel);
            this.grpDefinition.Controls.Add(this.label1);
            this.grpDefinition.Controls.Add(this.labPb);
            this.grpDefinition.Controls.Add(this.pictureBox1);
            this.grpDefinition.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDefinition.Enabled = false;
            this.grpDefinition.Location = new System.Drawing.Point(0, 0);
            this.grpDefinition.Name = "grpDefinition";
            this.grpDefinition.Size = new System.Drawing.Size(686, 133);
            this.grpDefinition.TabIndex = 1;
            this.grpDefinition.TabStop = false;
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Location = new System.Drawing.Point(155, 32);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.ReadOnly = true;
            this.txtLabel.Size = new System.Drawing.Size(496, 22);
            this.txtLabel.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Title :";
            // 
            // labPb
            // 
            this.labPb.AutoSize = true;
            this.labPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPb.Location = new System.Drawing.Point(15, 81);
            this.labPb.Name = "labPb";
            this.labPb.Size = new System.Drawing.Size(67, 17);
            this.labPb.TabIndex = 13;
            this.labPb.Text = "Problem";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TSchedule2.Properties.Resources.problem_48;
            this.pictureBox1.Location = new System.Drawing.Point(18, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // BaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDefinition);
            this.Name = "BaseView";
            this.Size = new System.Drawing.Size(686, 545);
            this.grpDefinition.ResumeLayout(false);
            this.grpDefinition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox grpDefinition;
        protected System.Windows.Forms.TextBox txtLabel;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label labPb;
        protected System.Windows.Forms.PictureBox pictureBox1;
    }
}
