using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using TrainScheduler.InputOutputDataModel;

namespace TrainScheduler.UI
{
    public partial class ProblemSolutionView : UserControl
    {
        public ProblemSolutionView()
        {
            InitializeComponent();
            Setup();
        }

        private ProblemInstance currentProblem;
        public ProblemInstance CurrentProblem
        {
            get => currentProblem;
            set
            {
                currentProblem = value;
                this.Setup();
            }
        }

        private Solution currentSolution;
        public Solution CurrentSolution
        {
            get => currentSolution;
            set
            {
                currentSolution = value;
                this.Setup();
            }
        }


        public void Setup()
        {
            grpProblem.Enabled = currentProblem != null;
            grpSolution.Enabled = currentSolution != null;

            txtLabel.Text = currentProblem?.Label;
            txtHash.Text = currentProblem?.Hash.ToString();
            txtNbTrains.Text = currentProblem?.ServiceIntentions?.Count.ToString();
            txtNbRoutes.Text = currentProblem?.Routes?.Count.ToString();
            txtNbResources.Text = currentProblem?.Resources?.Count.ToString();
        }
    }
}
