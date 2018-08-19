using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Data;
using TrainScheduler.Utils;

namespace TrainScheduler.Solver {
   public class IterativeDummySolver : BaseSolver {
      public override Solution Run() {

         do {
            // Assign path randomly
            CurrentSolution.AssignRandomPaths();

            // Simple Schedule 
            BasicSchedule(CurrentSolution.TrainRuns.OrderBy(tr => tr.Train.MinEntryEarliest));

            // Detect conflicts
            var conflicts = CurrentSolution.GetConflicts();
            foreach (var c in conflicts) {
               CurrentSolution.TrainRunsDic[c.Key].IsScheduled = false;
            }

            // Apply simple algorithm for trains with no conflicts
            var usedResources = new UsedResourceCollection();
            DummySolver.ScheduleTrains(CurrentProblem, CurrentSolution.TrainRuns.Where(tr => tr.IsScheduled).OrderBy(tr => tr.Train.MinEntryEarliest), usedResources);

            // Schedule conflicted trains
            DummySolver.ScheduleTrains(CurrentProblem, CurrentSolution.TrainRuns.Where(tr => !tr.IsScheduled).OrderBy(tr => tr.Train.MinEntryEarliest), usedResources);

            var validation = CurrentSolution.Validate();
            if (CurrentSolution.IsAdmissible) // Only consider admissible solution 
            {
               Log($"Compute objective function ... ");
               CurrentSolution.ComputeObjectiveFunction();
               CompareWithBest(CurrentSolution);
            }
            else {
               Log(validation);
            }

         } while (++Iteration < MaxIteration && !BestSolution.IsOptimal);

         return BestSolution;
      }
   }
}

