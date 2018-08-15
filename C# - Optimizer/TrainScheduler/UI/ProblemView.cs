using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace TrainScheduler.UI
{
    public partial class ProblemView : BaseView
    {
        private readonly GViewer _graphViewer = new GViewer();

        public ProblemView()
        {
            InitializeComponent();
            Setup();

            _graphViewer.EdgeInsertButtonVisible = _graphViewer.UndoRedoButtonsVisible = _graphViewer.LayoutAlgorithmSettingsButtonVisible = _graphViewer.NavigationVisible = false;
            _graphViewer.PanButtonPressed = true;

            panGraphViewContainer.SuspendLayout();
            _graphViewer.Dock = DockStyle.Fill;
            panGraphViewContainer.Controls.Add(_graphViewer);
            panGraphViewContainer.ResumeLayout();
        }

        public sealed override void Setup()
        {
            base.Setup();

            grpDefinition.Enabled = grpGraph.Enabled = Program.MainForm?.CurrentProblem != null;

            txtHash.Text = Program.MainForm?.CurrentProblem?.Hash.ToString();
            txtNbTrains.Text = Program.MainForm?.CurrentProblem?.ServiceIntentions?.Count.ToString();
            txtNbRoutes.Text = Program.MainForm?.CurrentProblem?.Routes?.Count.ToString();
            txtNbResources.Text = Program.MainForm?.CurrentProblem?.Resources?.Count.ToString();

            cboRouteSelection.Items.Clear();
            if (Program.MainForm?.CurrentProblem != null)
                cboRouteSelection.Items.AddRange(Program.MainForm.CurrentProblem.RouteDic.Keys.ToArray());
        }

        private void cboRouteSelection_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var key = (string)cboRouteSelection.SelectedItem;
            if (Program.MainForm.CurrentProblem.RouteDic.ContainsKey(key))
            {
                txtNbPossiblePaths.Text = Program.MainForm.CurrentProblem.RouteDic[key].Graph.PossiblePathsOrderedByPenalty.Length.ToString("##,###");
                _graphViewer.SuspendLayout();
                _graphViewer.Graph = Program.MainForm.CurrentProblem.RouteDic[key].Graph.MSGraph;
                _graphViewer.ResumeLayout();

                cboPathSelection.Items.Clear();
                for(int i=0; i < Program.MainForm.CurrentProblem.RouteDic[key].Graph.PossiblePathsOrderedByPenalty.Length; ++i)
                    cboPathSelection.Items.Add(i);
            }
        }

        private void cboPathSelection_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var routeKey = (string)cboRouteSelection.SelectedItem;
            var pathKey = (int) cboPathSelection.SelectedItem;

            if (Program.MainForm.CurrentProblem.RouteDic.ContainsKey(routeKey))
            {
                _graphViewer.SuspendLayout();
                foreach (var edge in _graphViewer.Graph.Edges)
                {
                    edge.Attr.Color = Color.Black;
                }

                foreach (var edge in Program.MainForm.CurrentProblem.RouteDic[routeKey].Graph.PossiblePathsOrderedByPenalty[pathKey].Sections)
                {
                    _graphViewer.Graph.EdgeById(edge.SequenceNumber.ToString()).Attr.Color = Color.Red;
                }
                _graphViewer.Refresh();
                _graphViewer.ResumeLayout();

            }
        }
    }
}
