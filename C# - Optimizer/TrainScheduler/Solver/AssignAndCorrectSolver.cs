using TrainScheduler.Data;

namespace TrainScheduler.Solver
{
    public class AssignAndCorrectSolver : BaseSolver
    {
        private Solution CurrentSolution { get;  set; }

        public override void Init(ProblemInstance problem)
        {
            Log(HorizontalLine);
            Log("Initializing AssignAndCorrect Solver ...", Utils.Logging.LogEventArgs.MessageType.Info, false);
            CurrentProblem = problem;
            CurrentSolution = new Solution(problem);
            Log(" done.", Utils.Logging.LogEventArgs.MessageType.Success);
        }

        public override Solution Run()
        {
            do
            {
                Log(HorizontalLine);
                Log($"Iteration {++Iteration}");

                Log($"Assign a path for each train ... ", Utils.Logging.LogEventArgs.MessageType.Info, false);
                if (CurrentSolution.AssignPaths())
                {
                    Log($"done.", Utils.Logging.LogEventArgs.MessageType.Success);
                }
                else
                {
                    Log($"failed.", Utils.Logging.LogEventArgs.MessageType.Error);
                }
                
                CompareWithBest(CurrentSolution);
            } while (Iteration < MaxIteration && !BestSolution.IsOptimal);

            return BestSolution;
        }
    }
}
