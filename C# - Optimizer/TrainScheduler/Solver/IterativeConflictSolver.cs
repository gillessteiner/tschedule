using System;
using System.Collections.Generic;
using System.Linq;
using TrainScheduler.Data;
using TrainScheduler.Utils;
using Math = TrainScheduler.Utils.Math;

namespace TrainScheduler.Solver {

   /// <summary>
   /// Solve conflict iteratively by delaying some trains
   /// </summary>
   public class IterativeConflictSolver : BaseSolver {

      public override void Init(ProblemInstance problem) {
         base.Init(problem);

         Log($"Assign a path for each train ... ");
         CurrentSolution.AssignPaths();

         // Schedule every train without considering conflicts in resource occupation
         Log($"Schedule each train without considering resource occupation ... ", Utils.Logging.LogEventArgs.MessageType.Info, false);
         BasicSchedule(CurrentSolution.TrainRunsDic.Values.OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty));
         Log("done.", Utils.Logging.LogEventArgs.MessageType.Success);
      }

      public override Solution Run() {
         int nbConflicts = 0;
         // Only one iteration for this solver
         do {
            nbConflicts = ScheduleTrains(CurrentProblem, CurrentSolution.TrainRunsDic.Values
               .OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty));

            Log($"Iteration {Iteration}: {nbConflicts} conflicts.");

            CurrentSolution.Validate();
            if (CurrentSolution.IsAdmissible) // Only consider admissible solution 
            {
               Log($"Compute objective function ... ");
               CurrentSolution.ComputeObjectiveFunction();
               CompareWithBest(CurrentSolution);
               break;
            }
         } while (++Iteration < MaxIteration && nbConflicts > 0);

         return BestSolution;
      }

      public static int ScheduleTrains(ProblemInstance problem, IEnumerable<TrainRun> trainRuns) {
         var nbConflicts = 0;
         var usedResources = new UsedResourceCollection();
         foreach (var trainRun in trainRuns) {

            // ----------------------------------------------------
            //Log($"Delay train {trainRun.ServiceIntentionId} start ? ", Utils.Logging.LogEventArgs.MessageType.Info, false);
            var delay = TimeSpan.Zero;
            var initialRunSection = trainRun.TrainRunSections[0];
            var initialSection = problem.TryGetRouteSection(initialRunSection.Key);
            foreach (var resId in initialSection.ResourceIds) {
               var resource = problem.TryGetResource(resId);
               if (resource != null) {
                  foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                     if (Math.Intersect(initialRunSection.EntryTime, initialRunSection.ExitTime + resource.ReleaseTime, usage.Item3, usage.Item4))
                        delay = Math.Max(delay, usage.Item4 - initialRunSection.EntryTime);
                  }
               }
            }

            // Postpone train departure
            if (delay > TimeSpan.Zero) {
               ++nbConflicts;
               //Log($"Yes. Delay is {delay}");
               initialRunSection.EntryTime += delay;
               trainRun.ApplyDelay(delay);
            }

            //Log($"Check other sections for delays ... ");
            for (int k = 1; k < trainRun.TrainRunSections.Count; ++k) {
               var runSection = trainRun.TrainRunSections[k];
               var section = problem.TryGetRouteSection(runSection.Key);

               // Check resource occupation for next section
               delay = TimeSpan.Zero;

               foreach (var resId in section.ResourceIds) {
                  var resource = problem.TryGetResource(resId);
                  if (resource != null) {
                     foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                        if (Math.Intersect(runSection.EntryTime, runSection.ExitTime + resource.ReleaseTime, usage.Item3, usage.Item4)) {
                           delay = Math.Max(delay, usage.Item4 - runSection.EntryTime);
                        }
                     }
                  }
               }

               if (delay > TimeSpan.Zero) {
                  ++nbConflicts;
                  //Log($"Delay exit of train {trainRun.ServiceIntentionId}, from section {k-1} by {delay}");
                  trainRun.ApplyDelay(delay, k - 1);
               }
            }

            // ----------------------------------------------------
            //Log($"Add current train resource occupation ... ");
            foreach (var runSection in trainRun.TrainRunSections) {
               var section = problem.TryGetRouteSection(runSection.Key);

               foreach (var resId in section.ResourceIds) {
                  var resource = problem.TryGetResource(resId);
                  if (resource != null) {
                     usedResources.Add(resId, trainRun.ServiceIntentionId, runSection.SequenceNumber,
                        runSection.EntryTime, runSection.ExitTime + resource.ReleaseTime);
                  }
               }
            }
         }

         return nbConflicts;
      }
   }
}
