using System;

namespace TrainScheduler.UI
{
    public partial class SolverView : BaseView
    {
        enum SolverType
        {
            AssignAndCorrect,
            Swarm
        }

        public SolverView()
        {
            InitializeComponent();
            cboSolverList.DataSource = Enum.GetValues(typeof(SolverType));
        }

        private SolverType SelectedSolver => Enum.TryParse<SolverType>(cboSolverList.SelectedValue.ToString(), out var value) ? value : SolverType.AssignAndCorrect;

        public override void Setup()
        {
            base.Setup();
        }
    }
}
