using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using TrainScheduler.InputOutputDataModel;
using Microsoft.Msagl.GraphViewerGdi;
using Graph = TrainScheduler.Data.Graph;

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

        private readonly Dictionary<string, Graph> _routes = new Dictionary<string, Graph>();
        public override void Setup()
        {
            base.Setup();

            grpDefinition.Enabled = grpGraph.Enabled = MainForm.CurrentProblem != null;

            txtHash.Text = MainForm.CurrentProblem?.Hash.ToString();
            txtNbTrains.Text = MainForm.CurrentProblem?.ServiceIntentions?.Count.ToString();
            txtNbRoutes.Text = MainForm.CurrentProblem?.Routes?.Count.ToString();
            txtNbResources.Text = MainForm.CurrentProblem?.Resources?.Count.ToString();

            CreateRouteGraphs();
        }

        public void CreateRouteGraphs()
        {
            _routes.Clear();
            if (MainForm.CurrentProblem != null)
            {
                foreach (var route in MainForm.CurrentProblem.Routes)
                {
                    _routes.Add(route.Id, new Graph(route));
                }

                cboRouteSelection.Items.Clear();
                cboRouteSelection.Items.AddRange(_routes.Keys.ToArray());
            }
        }

        private void cboRouteSelection_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var key = (string)cboRouteSelection.SelectedItem;
            if (_routes.ContainsKey(key))
            {
                txtNbPossiblePaths.Text = _routes[key].PossiblePathsOrderedByPenalty.Length.ToString("##,###");
                _graphViewer.SuspendLayout();
                _graphViewer.Graph = _routes[key].MSGraph;
                _graphViewer.ResumeLayout();

                cboPathSelection.Items.Clear();
                for(int i=0; i < _routes[key].PossiblePathsOrderedByPenalty.Length; ++i)
                    cboPathSelection.Items.Add(i);
            }
        }

        private void cboPathSelection_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var routeKey = (string)cboRouteSelection.SelectedItem;
            var pathKey = (int) cboPathSelection.SelectedItem;

            if (_routes.ContainsKey(routeKey))
            {
                _graphViewer.SuspendLayout();
                foreach (var edge in _graphViewer.Graph.Edges)
                {
                    edge.Attr.Color = Color.Black;
                }

                foreach (var edge in _routes[routeKey].PossiblePathsOrderedByPenalty[pathKey].Sections)
                {
                    _graphViewer.Graph.EdgeById(edge.SequenceNumber.ToString()).Attr.Color = Color.Red;
                }
                _graphViewer.Refresh();
                _graphViewer.ResumeLayout();

            }
        }
    }
}
