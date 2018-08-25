using System.Linq;
using System.Windows.Forms;
using Data.Model;
using Microsoft.Msagl.GraphViewerGdi;
using TSchedule2.Properties;

namespace TSchedule2.Views
{
   public sealed partial class SolutionView : BaseView
   {
      private readonly GViewer _graphViewer = new GViewer();

      public SolutionView() {
         InitializeComponent();
         Setup();

         _graphViewer.EdgeInsertButtonVisible = _graphViewer.UndoRedoButtonsVisible = _graphViewer.LayoutAlgorithmSettingsButtonVisible = _graphViewer.NavigationVisible = false;
         _graphViewer.PanButtonPressed = true;

         panGraphViewContainer.SuspendLayout();
         _graphViewer.Dock = DockStyle.Fill;
         panGraphViewContainer.Controls.Add(_graphViewer);
         panGraphViewContainer.ResumeLayout();
      }

      public override void Setup() {
         base.Setup();
         grpDefinition.Enabled = grpGraph.Enabled = Program.MainForm?.CurrentSolution != null;
         txtObjValue.Text = Program.MainForm?.CurrentSolution?.ObjectiveValue.ToString("N2");
         cboTrainSelection.Items.Clear();

         if (Program.MainForm?.CurrentSolution != null) {
            cboTrainSelection.Items.AddRange(Program.MainForm.CurrentSolution.TrainKeys.ToArray());
         }
         
         txtNbTrains.Text = cboTrainSelection.Items.Count.ToString();
      }

      private Track SelectedTrack => Program.MainForm?.CurrentProblem?.GetTrain((string)cboTrainSelection.SelectedItem)?.Track;

      private void cboTrainSelection_SelectionChangeCommitted(object sender, System.EventArgs e) {
         if (SelectedTrack != null && Program.MainForm?.CurrentSolution != null) {

            var selectedRoute = Program.MainForm.CurrentSolution.GetTrainRun((string) cboTrainSelection.SelectedItem).TrainRunSections;
            SelectedTrack.Highlight(selectedRoute);
            SelectedTrack.AdjustLabels(selectedRoute);
            _graphViewer.SuspendLayout();
            _graphViewer.Graph = SelectedTrack;
            _graphViewer.ResumeLayout();
         }
      }

      private void btnSave_Click(object sender, System.EventArgs e) {
         Program.MainForm?.saveSolutionToolStripMenuItem_Click(sender, e);
      }

      private void btnValidate_Click(object sender, System.EventArgs e) {
         btnValidate.Enabled = false;
         
          var txt = Program.MainForm?.CurrentSolution?.Validate();
          
         if (txt != null) {
            txtValidationError.Text = txt;
            picValidation.Image = Resources.StatusInvalid_32x;
         }
         else {
            txtValidationError.Text = "Solution is valid";
            picValidation.Image = Resources.StatusOK_32x;
         }

         txtValidationError.Visible = picValidation.Visible = btnValidate.Enabled = true;
      }
   }
}
