using System;
using System.IO;
using System.Windows.Forms;
using TrainScheduler.InputOutputDataModel;

namespace TrainScheduler.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool CanClose
        {
            get
            {
                if (CurrentProblem == null)
                    return true;

                
                return (MessageBox.Show("A problem is currently loaded, are you sure you want to quit?",
                            "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) ==
                        DialogResult.Yes);
            }
        }

        internal static ProblemInstance CurrentProblem;
        internal static Solution CurrentSolution;

        private void openProblemToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            openJsonFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
            openJsonFileDialog.Title = "Select a Problem Instance File";

            if (openJsonFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CurrentProblem = new ProblemInstance();
                    CurrentProblem.FromJson(File.ReadAllText(openJsonFileDialog.FileName));
                    problemView.Setup();
                    solverView.Setup();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File opening failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void saveSolutionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (CanClose)
            {
                this.Close();
            }
        }
    }
}
