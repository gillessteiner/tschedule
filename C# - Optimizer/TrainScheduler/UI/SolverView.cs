using System;
using System.Diagnostics;
using System.Windows.Forms;
using TrainScheduler.Solver;
using TrainScheduler.Utils;

namespace TrainScheduler.UI
{
    public partial class SolverView : BaseView
    {
      public SolverView()
        {
            InitializeComponent();
            cboSolverList.DataSource = Enum.GetValues(typeof(BaseSolver.SolverType));
        }

        private BaseSolver.SolverType SelectedSolver => Enum.TryParse<BaseSolver.SolverType>(cboSolverList.SelectedValue.ToString(), out var value) ? value : BaseSolver.SolverType.AssignAndCorrect;

        private int MaxIter => ((int)numMaxIter.Value);

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (Program.MainForm.CurrentSolution != null)
            {
                if (MessageBox.Show("A solution is currently loaded. If you continue, it will be lost. Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            btnSolve.Enabled = false;

            logSolver.Clear();
            logSolver.WriteTitle($"{DateTime.Now:F}, start");

            logSolver.ReportInfo($"Problem:             {Program.MainForm.CurrentProblem.Label}");
            logSolver.ReportInfo($"Solver type:         {SelectedSolver.ToString()}");
            logSolver.ReportInfo($"Solver max nb iters: {MaxIter:N0}");

            var timer = Stopwatch.StartNew();

            try
            {
                var solver = BaseSolver.Create(SelectedSolver);
                solver.MaxIteration = MaxIter;

                solver.Logging += SolverLogging;

                solver.Init(Program.MainForm.CurrentProblem);
                Program.MainForm.CurrentSolution = solver.Run();

                solver.Logging -= SolverLogging;
            }
            catch (Exception ex)
            {
                logSolver.ReportError(ex.Message);
            }

            timer.Stop();

            logSolver.ReportInfo($"Simulation time:     {timer.Elapsed.Minutes:00}:{timer.Elapsed.Seconds:00}");
            logSolver.WriteTitle($"{DateTime.Now:F}, end");
            btnSolve.Enabled = true;
        }

        private void SolverLogging(object sender, Logging.LogEventArgs logEventArgs)
        {
            logSolver.ReportMessage(logEventArgs.Message, logEventArgs.Type, logEventArgs.Newline);
        }
    }
}
