using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using TSchedule2.Data.Model;

namespace TSchedule2.Views {
   public partial class ProblemView : BaseView {
      private readonly GViewer _graphViewer = new GViewer();

      public ProblemView() {
         InitializeComponent();
         Setup();

         _graphViewer.EdgeInsertButtonVisible = _graphViewer.UndoRedoButtonsVisible = _graphViewer.LayoutAlgorithmSettingsButtonVisible = _graphViewer.NavigationVisible = false;
         _graphViewer.PanButtonPressed = true;

         panGraphViewContainer.SuspendLayout();
         _graphViewer.Dock = DockStyle.Fill;
         panGraphViewContainer.Controls.Add(_graphViewer);
         panGraphViewContainer.ResumeLayout();
      }

      private Track SelectedTrack => Program.MainForm?.CurrentProblem?.GetTrack((string) cboRouteSelection.SelectedItem);
      private int SelectedPath => (int) cboPathSelection.SelectedItem;

      public sealed override void Setup() {
         base.Setup();

         grpDefinition.Enabled = grpGraph.Enabled = Program.MainForm?.CurrentProblem != null;

         txtHash.Text = Program.MainForm?.CurrentProblem?.Hash.ToString();

         txtNbTrains.Text = Program.MainForm?.CurrentProblem?.NbTrains.ToString();
         txtNbRoutes.Text = Program.MainForm?.CurrentProblem?.NbRoutes.ToString();
         txtNbResources.Text = Program.MainForm?.CurrentProblem?.NbResources.ToString();

         cboRouteSelection.Items.Clear();
         if (Program.MainForm?.CurrentProblem != null)
            cboRouteSelection.Items.AddRange(Program.MainForm.CurrentProblem.RouteKeys.ToArray());
         
      }

      private void cboRouteSelection_SelectionChangeCommitted(object sender, EventArgs e) {
         if (SelectedTrack != null) {
            txtNbPossiblePaths.Text = SelectedTrack.NbPaths.ToString("##,###");
            _graphViewer.SuspendLayout();
            SelectedTrack.SelectPath(-1); // Reinit edge colors
            _graphViewer.Graph = SelectedTrack;
            _graphViewer.ResumeLayout();

            cboPathSelection.Items.Clear();
            for (int i = 0; i < SelectedTrack.NbPaths; ++i)
               cboPathSelection.Items.Add(i);
         }
      }

      private void cboPathSelection_SelectionChangeCommitted(object sender, EventArgs e) {
         SelectedTrack?.SelectPath(SelectedPath);
         _graphViewer.Refresh();
      }
   }
}
