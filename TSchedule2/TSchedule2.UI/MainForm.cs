using System;
using System.IO;
using System.Windows.Forms;
using TSchedule2.Data.Model;
using TSchedule2.Data.SBB;

namespace TSchedule2 {
   public partial class MainForm : Form {
      public MainForm() {
         InitializeComponent();
      }

      private Problem  _currentProblem;
      private Solution _currentsolution;

      internal Problem CurrentProblem {
         get => _currentProblem;
         set {
            _currentProblem = value;
            problemView.Setup();
            solverView.Setup();
            openSolutionToolStripMenuItem.Enabled = true;
         }
      }

      internal Solution CurrentSolution {
         get => _currentsolution;
         set {
            _currentsolution = value;
            solutionView.Setup();
            saveSolutionToolStripMenuItem.Enabled = _currentsolution != null;
         }
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

      private void openProblemToolStripMenuItem_Click(object sender, System.EventArgs e) {
         // Displays an OpenFileDialog so the user can select a Cursor.  
         openJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
         openJsonFileDialog.Title = "Select a Problem Instance File";

         if (openJsonFileDialog.ShowDialog() == DialogResult.OK) {
            try {
               CurrentProblem = new Problem(ProblemInstance.FromJson(File.ReadAllText(openJsonFileDialog.FileName)));
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

      internal void openSolutionToolStripMenuItem_Click(object sender, EventArgs e) {
         // Displays an OpenFileDialog so the user can select a Cursor.  
         openJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
         openJsonFileDialog.Title = "Select a Problem Instance File";

         if (openJsonFileDialog.ShowDialog() == DialogResult.OK) {
            try {
               var solution = Solution.FromJson(File.ReadAllText(openJsonFileDialog.FileName));
               solution.Problem = CurrentProblem;
               solution.EvalObjectiveFunction();
               CurrentSolution = solution; // When we assign the CurrentSolution, it must be complete
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
