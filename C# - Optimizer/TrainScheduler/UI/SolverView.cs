using System;
using System.Diagnostics;

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
            Iteration = 0;
        }

        private int Iteration
        {
            get => int.Parse(txtIteration.Text);
            set => txtIteration.Text = value.ToString("N0");
        }

        private int MaxIter => ((int)numMaxIter.Value);

        private void btnSolve_Click(object sender, EventArgs e)
        {
            var timer = Stopwatch.StartNew();

            Iteration = 1; grpSolverInfo.Visible = true;
            logSolver.Clear();
            logSolver.WriteTitle($"{DateTime.Now:F}, start");

            logSolver.ReportInfo($"Problem:             {MainForm.CurrentProblem.Label}");
            logSolver.ReportInfo($"Solver type:         {SelectedSolver.ToString()}");
            logSolver.ReportInfo($"Solver max nb iters: {MaxIter:N0}");

            timer.Stop();
            logSolver.ReportInfo($"Simulation time:     {timer.Elapsed.Minutes:00}:{timer.Elapsed.Seconds:00}");
            logSolver.WriteTitle($"{DateTime.Now:F}, end");

        }
    }
}
