using System;
using System.IO;
using System.Windows.Forms;
using TrainScheduler.Data;

namespace TrainScheduler.UI {
   public partial class MainForm : Form {
      public MainForm() {
         InitializeComponent();
      }

      private bool CanClose {
         get {
            if (CurrentProblem == null)
               return true;

            return (MessageBox.Show("A problem is currently loaded, are you sure you want to quit?",
                       "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) ==
                    DialogResult.Yes);
         }
      }

      internal ProblemInstance CurrentProblem;
      private Solution _currentsolution;

      internal Solution CurrentSolution {
         get => _currentsolution;
         set {
            _currentsolution = value;
            solutionView.Setup();
            saveSolutionToolStripMenuItem.Enabled = _currentsolution != null;
         }

      }

      private void openProblemToolStripMenuItem_Click(object sender, System.EventArgs e) {
         // Displays an OpenFileDialog so the user can select a Cursor.  
         openJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
         openJsonFileDialog.Title = "Select a Problem Instance File";

         if (openJsonFileDialog.ShowDialog() == DialogResult.OK) {
            try {
               CurrentProblem = new ProblemInstance();
               CurrentProblem.FromJson(File.ReadAllText(openJsonFileDialog.FileName));
               openSolutionToolStripMenuItem.Enabled = true;
               problemView.Setup();
               solverView.Setup();

            }
            catch (Exception ex) {
               MessageBox.Show(ex.Message, "File opening failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         }
      }

      internal void saveSolutionToolStripMenuItem_Click(object sender, System.EventArgs e) {
         // Displays an OpenFileDialog so the user can select a Cursor.  
         saveJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
         saveJsonFileDialog.Title = "Select a name for the Solution File";

         if (saveJsonFileDialog.ShowDialog() == DialogResult.OK) {
            try {
               File.WriteAllText(saveJsonFileDialog.FileName, CurrentSolution.ToJson());
            }
            catch (Exception ex) {
               MessageBox.Show(ex.Message, "File saving failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         }
      }

      internal void openSolution_Click(object sender, System.EventArgs e) {
         // Displays an OpenFileDialog so the user can select a Cursor.  
         openJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
         openJsonFileDialog.Title = "Select a Problem Instance File";

         if (openJsonFileDialog.ShowDialog() == DialogResult.OK) {
            try {
               var solution = new Solution(CurrentProblem);
               solution.FromJson(File.ReadAllText(openJsonFileDialog.FileName));
               CurrentSolution = solution;
            }
            catch (Exception ex) {
               MessageBox.Show(ex.Message, "File opening failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         }
      }

      private void closeToolStripMenuItem_Click(object sender, System.EventArgs e) {
         if (CanClose) {
            this.Close();
         }
      }
   }
}
