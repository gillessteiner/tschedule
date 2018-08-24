using TSchedule2.Data.SBB;
using TSchedule2.Data.Utils;

namespace TSchedule2.Solver
{
   public class DummySolver : BaseSolver
   {
      public override Solution Run() {
         do {
            Log($"Iteration {Iteration}...");
            // -----------------------------------------------------------
            // Generate a new solution every time
            CurrentSolution = new Solution(CurrentProblem);

            // -----------------------------------------------------------
            Log($"Reorder train randomly...");
            var randomTrainOrder = CurrentProblem.TrainKeys.Shuffle();

            // -----------------------------------------------------------
            Log($"Assign a random path for each train...");
            foreach (var t in CurrentProblem.TrainKeys) {
               var path = CurrentProblem.GetTrain(t).GetRandomPath();
               CurrentSolution.GetTrainRun(t).AssignPath(path);
            }

            // -----------------------------------------------------------
            Log($"Schedule all trains ignoring resources ...");
            this.BasicSchedule(CurrentSolution.GetTrainRuns(randomTrainOrder));

            // -----------------------------------------------------------
            Log($"Take into account resource occupation ...");
            // Delays some trains according to resource occupation
            // Prioritize trains on top of the list


            // -----------------------------------------------------------
            CurrentSolution.EvalObjectiveFunction();

            if (BestSolution == null || CurrentSolution.ObjectiveValue < BestSolution.ObjectiveValue) {
               BestSolution = CurrentSolution;
               Log($"New solution with objective value {BestSolution.ObjectiveValue:N2}");
            }

         } while (++Iteration < MaxIteration && (BestSolution == null || !BestSolution.IsOptimal));

         return BestSolution;
      }
   }
}
