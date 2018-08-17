using System.Collections.Generic;
using System.Linq;
using TrainScheduler.Data;

namespace TrainScheduler.Solver {
   /// <summary>
   /// An iterative solver trying to use alternative routes
   /// </summary>
   public class SmarterConflictSolver : BaseSolver {
      private IEnumerable<TrainRun> OrderedTrainRuns;

      public override void Init(ProblemInstance problem) {
         base.Init(problem);
         CurrentSolution.AssignPaths();

         OrderedTrainRuns = CurrentSolution.TrainRunsDic.Values.OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty).ToArray();
      }

      public override Solution Run() {
         do {
            BasicSchedule(OrderedTrainRuns);

            var conflicts = CurrentSolution.GetConflicts();
            foreach (var kvp in conflicts) {
               CurrentSolution.TrainRunsDic[kvp.Key].Order = kvp.Value.Count;
               if (CurrentSolution.TrainRunsDic[kvp.Key].Route.Graph.PossiblePathsOrderedByPenalty.Length > 1) {
                  Log($"Train {kvp.Key} has {CurrentSolution.TrainRunsDic[kvp.Key].Route.Graph.PossiblePathsOrderedByPenalty.Length-1} alternative routes");
               }
            }

            // Schedule all trains with simple algorithm
            var subIter = 0;
            var nbConflicts = 0;
            do {
               nbConflicts = IterativeConflictSolver.ScheduleTrains(CurrentProblem, CurrentSolution.TrainRunsDic.Values.OrderByDescending(tr => tr.Order));
               // Log($"Iteration {subIter}: {nbConflicts} conflicts.");
            } while (++subIter < SubIteration && nbConflicts > 0);

            if (nbConflicts == 0) // Only consider admissible solution 
            {
               CurrentSolution.ComputeObjectiveFunction();
               Log($"Iteration {Iteration}, Objective value {CurrentSolution.ObjectiveValue}");   
               CompareWithBest(CurrentSolution);
            }
            
            if(BestSolution == null || !BestSolution.IsOptimal)
            {
               // Is there some route alternative
               foreach (var kvp in conflicts) {
                  if (CurrentSolution.TrainRunsDic[kvp.Key].SelectNextPath()) {
                     Log($"Train {kvp.Key}, changing route");
                     break;
                  }
               }
            }
        } while (++Iteration < MaxIteration && (BestSolution == null || !BestSolution.IsOptimal));

         return (BestSolution ?? CurrentSolution);
      }
   }
}
