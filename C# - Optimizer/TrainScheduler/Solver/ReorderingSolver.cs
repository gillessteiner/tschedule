using System.Collections.Generic;
using System.Linq;
using TrainScheduler.Data;

namespace TrainScheduler.Solver {
   /// <summary>
   /// An iterative solver trying to use alternative routes
   /// </summary>
   public class ReorderingSolver : BaseSolver {
      private IEnumerable<TrainRun> OrderedTrainRuns;

      public override void Init(ProblemInstance problem) {
         base.Init(problem);
         CurrentSolution.AssignPaths();

         OrderedTrainRuns = CurrentSolution.TrainRunsDic.Values.OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty).ToArray();
      }

      public override Solution Run() {
         do {
            BasicSchedule(OrderedTrainRuns);
            int subIter = 0;
            int nbConflicts;
            do {
               nbConflicts = IterativeConflictSolver.ScheduleTrains(CurrentProblem, OrderedTrainRuns);
               Log($"Iteration {Iteration}: {nbConflicts} conflicts.");
            } while (++subIter < SubIteration && nbConflicts > 0);

            CurrentSolution.ComputeObjectiveFunction();
            Log($"Iteration {Iteration}, Objective value {CurrentSolution.ObjectiveValue}");
            CurrentSolution.Validate();
            if (CurrentSolution.IsAdmissible) // Only consider admissible solution 
            {
               CompareWithBest(CurrentSolution);
            }

            // Reorder trains and start again (unless optimal has been reached)
            OrderedTrainRuns = CurrentSolution.TrainRunsDic.Values.OrderByDescending(tr => tr.ObjectiveValue).ToArray();

            // We need a way to identify that some ordering has been used already. Hash of the array ? 


         } while (++Iteration < MaxIteration && (BestSolution == null || !BestSolution.IsOptimal));

         return (BestSolution ?? CurrentSolution);
      }
   }
}
