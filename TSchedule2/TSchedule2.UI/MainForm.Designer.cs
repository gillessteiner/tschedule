namespace TSchedule2
{
   partial class MainForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openJsonFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.saveJsonFileDialog = new System.Windows.Forms.SaveFileDialog();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabProblem = new System.Windows.Forms.TabPage();
         this.problemView = new TSchedule2.Views.ProblemView();
         this.tabSolver = new System.Windows.Forms.TabPage();
         this.solverView = new TSchedule2.Views.SolverView();
         this.tabSolution = new System.Windows.Forms.TabPage();
         this.solutionView = new TSchedule2.Views.SolutionView();
         this.menuStrip1.SuspendLayout();
         this.tabControl1.SuspendLayout();
         this.tabProblem.SuspendLayout();
         this.tabSolver.SuspendLayout();
         this.tabSolution.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(800, 28);
         this.menuStrip1.TabIndex = 1;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProblemToolStripMenuItem,
            this.saveSolutionToolStripMenuItem,
            this.openSolutionToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // openProblemToolStripMenuItem
         // 
         this.openProblemToolStripMenuItem.Name = "openProblemToolStripMenuItem";
         this.openProblemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openProblemToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
         this.openProblemToolStripMenuItem.Text = "Open problem ...";
         this.openProblemToolStripMenuItem.Click += new System.EventHandler(this.openProblemToolStripMenuItem_Click);
         // 
         // saveSolutionToolStripMenuItem
         // 
         this.saveSolutionToolStripMenuItem.Enabled = false;
         this.saveSolutionToolStripMenuItem.Name = "saveSolutionToolStripMenuItem";
         this.saveSolutionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveSolutionToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
         this.saveSolutionToolStripMenuItem.Text = "Save solution ...";
         this.saveSolutionToolStripMenuItem.Click += new System.EventHandler(this.saveSolutionToolStripMenuItem_Click);
         // 
         // openSolutionToolStripMenuItem
         // 
         this.openSolutionToolStripMenuItem.Enabled = false;
         this.openSolutionToolStripMenuItem.Name = "openSolutionToolStripMenuItem";
         this.openSolutionToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
         this.openSolutionToolStripMenuItem.Text = "Open solution ...";
         this.openSolutionToolStripMenuItem.Click += new System.EventHandler(this.openSolutionToolStripMenuItem_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(244, 6);
         // 
         // closeToolStripMenuItem
         // 
         this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.closeToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
         this.closeToolStripMenuItem.Text = "Quit";
         this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
         // 
         // tabControl1
         // 
         this.tabControl1.Controls.Add(this.tabProblem);
         this.tabControl1.Controls.Add(this.tabSolver);
         this.tabControl1.Controls.Add(this.tabSolution);
         this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl1.Location = new System.Drawing.Point(0, 28);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(800, 422);
         this.tabControl1.TabIndex = 2;
         // 
         // tabProblem
         // 
         this.tabProblem.Controls.Add(this.problemView);
         this.tabProblem.Location = new System.Drawing.Point(4, 25);
         this.tabProblem.Name = "tabProblem";
         this.tabProblem.Padding = new System.Windows.Forms.Padding(3);
         this.tabProblem.Size = new System.Drawing.Size(792, 393);
         this.tabProblem.TabIndex = 0;
         this.tabProblem.Text = "Problem";
         this.tabProblem.UseVisualStyleBackColor = true;
         // 
         // problemView
         // 
         this.problemView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.problemView.Location = new System.Drawing.Point(3, 3);
         this.problemView.Name = "problemView";
         this.problemView.Size = new System.Drawing.Size(786, 387);
         this.problemView.TabIndex = 0;
         // 
         // tabSolver
         // 
         this.tabSolver.Controls.Add(this.solverView);
         this.tabSolver.Location = new System.Drawing.Point(4, 25);
         this.tabSolver.Name = "tabSolver";
         this.tabSolver.Padding = new System.Windows.Forms.Padding(3);
         this.tabSolver.Size = new System.Drawing.Size(792, 393);
         this.tabSolver.TabIndex = 1;
         this.tabSolver.Text = "Solver";
         this.tabSolver.UseVisualStyleBackColor = true;
         // 
         // solverView
         // 
         this.solverView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.solverView.Location = new System.Drawing.Point(3, 3);
         this.solverView.Name = "solverView";
         this.solverView.Size = new System.Drawing.Size(786, 387);
         this.solverView.TabIndex = 0;
         // 
         // tabSolution
         // 
         this.tabSolution.Controls.Add(this.solutionView);
         this.tabSolution.Location = new System.Drawing.Point(4, 25);
         this.tabSolution.Name = "tabSolution";
         this.tabSolution.Size = new System.Drawing.Size(792, 393);
         this.tabSolution.TabIndex = 2;
         this.tabSolution.Text = "Solution";
         this.tabSolution.UseVisualStyleBackColor = true;
         // 
         // solutionView
         // 
         this.solutionView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.solutionView.Location = new System.Drawing.Point(0, 0);
         this.solutionView.Name = "solutionView";
         this.solutionView.Size = new System.Drawing.Size(792, 393);
         this.solutionView.TabIndex = 0;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.tabControl1);
         this.Controls.Add(this.menuStrip1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "MainForm";
         this.Text = "Train Scheduler";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.tabControl1.ResumeLayout(false);
         this.tabProblem.ResumeLayout(false);
         this.tabSolver.ResumeLayout(false);
         this.tabSolution.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openProblemToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveSolutionToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openSolutionToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
      private System.Windows.Forms.OpenFileDialog openJsonFileDialog;
      private System.Windows.Forms.SaveFileDialog saveJsonFileDialog;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabProblem;
      private System.Windows.Forms.TabPage tabSolver;
      private System.Windows.Forms.TabPage tabSolution;
      private Views.ProblemView problemView;
      private Views.SolverView solverView;
      private Views.SolutionView solutionView;
   }
}

