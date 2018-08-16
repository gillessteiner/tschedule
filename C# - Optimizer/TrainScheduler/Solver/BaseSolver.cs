using System;
using TrainScheduler.Data;
using TrainScheduler.Utils;

namespace TrainScheduler.Solver
{
    public abstract class BaseSolver
    {
        public enum SolverType
        {
            AssignAndCorrect
        }

        public int Iteration { get; protected set; }
        public int MaxIteration { get; set; }

        protected ProblemInstance CurrentProblem { get; set; }
        public Solution BestSolution { get; protected set; }

        protected static readonly string HorizontalLine = new string('-', 100);

        /// <summary>
        /// Factory method for solver creation
        /// </summary>
        public static BaseSolver Create(SolverType type)
        {
            switch (type)
            {
                case SolverType.AssignAndCorrect:
                    return new AssignAndCorrectSolver();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public abstract void Init(ProblemInstance problem);

        protected void CompareWithBest(Solution someSolution)
        {
            if (BestSolution == null || (someSolution.ObjectiveValue < BestSolution.ObjectiveValue))
            {
                BestSolution = someSolution;
                BestSolution.Update();
                Log($"Found a better solution {someSolution.ObjectiveValue:F2}");
            }
        }

        public abstract Solution Run();

        #region Event for logging 
        public event Logging.LogEventHandler Logging;

        protected void Log(string msg, Logging.LogEventArgs.MessageType type = Utils.Logging.LogEventArgs.MessageType.Info, bool newline = true)
        {
            Logging?.Invoke(this, new Logging.LogEventArgs() {Message = msg, Type = type, Newline = newline});
        }

        #endregion

    }
}
