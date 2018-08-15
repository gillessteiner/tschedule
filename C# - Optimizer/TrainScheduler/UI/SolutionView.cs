using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;

namespace TrainScheduler.UI
{
    public partial class SolutionView : BaseView
    {
        private readonly GViewer _graphViewer = new GViewer();

        public SolutionView()
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

        public override void Setup()
        {
            base.Setup();
            grpDefinition.Enabled = grpGraph.Enabled = Program.MainForm?.CurrentSolution != null;
            txtObjValue.Text = Program.MainForm?.CurrentSolution?.ObjectiveValue.ToString("N2");
            cboTrainSelection.Items.Clear();

            if (Program.MainForm?.CurrentSolution != null)
            {
                cboTrainSelection.Items.AddRange(Program.MainForm.CurrentSolution.TrainRunsDic.Keys.ToArray());
            }

            txtNbTrains.Text = cboTrainSelection.Items.Count.ToString();
        }

        private void cboTrainSelection_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var key = (string)cboTrainSelection.SelectedItem;
            if (Program.MainForm.CurrentSolution.TrainRunsDic.ContainsKey(key))
            {
                _graphViewer.SuspendLayout();
                _graphViewer.Graph = Program.MainForm.CurrentSolution.TrainRunsDic[key].MSGraph;
                _graphViewer.ResumeLayout();
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Program.MainForm?.saveSolutionToolStripMenuItem_Click(sender, e);
        }
    }
}
