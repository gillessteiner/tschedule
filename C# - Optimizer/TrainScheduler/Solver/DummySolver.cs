using System;
using System.Collections.Generic;
using System.Linq;
using TrainScheduler.Data;
using TrainScheduler.Utils;
using Math = TrainScheduler.Utils.Math;

namespace TrainScheduler.Solver {

   /// <summary>
   /// In case of conflicts, just postpone the last train until the conflict is resolved
   /// </summary>
   public class DummySolver : BaseSolver {
      public override Solution Run() {
         Log($"Assign a path for each train ... ", Utils.Logging.LogEventArgs.MessageType.Info, false);
         if (CurrentSolution.AssignPaths()) {
            Log($"done.", Utils.Logging.LogEventArgs.MessageType.Success);
         }
         else {
            Log($"failed.", Utils.Logging.LogEventArgs.MessageType.Error);
         }

         // Only one iteration for this solver
         {
            Log(HorizontalLine);
            Log($"Loop on trains and assign entry-exit times ... ");
            var usedResources = new UsedResourceCollection();
            ScheduleTrains(CurrentProblem, CurrentSolution.TrainRunsDic.Values.OrderBy(tr => tr.Train.MinEntryEarliest), usedResources);

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
         }

         return BestSolution;
      }

      public static void ScheduleTrains(ProblemInstance problem, IEnumerable<TrainRun> trainRuns, UsedResourceCollection usedResources) {
         foreach (var trainRun in trainRuns) {
            // Schedule train accordingly
            for (int k = 0; k < trainRun.TrainRunSections.Count; ++k) {
               var thisRunSection = trainRun.TrainRunSections[k];
               var nextRunSection = (k < trainRun.TrainRunSections.Count - 1)
                  ? trainRun.TrainRunSections[k + 1]
                  : null;

               var thisSection = problem.TryGetRouteSection(thisRunSection.Key);
               var nextSection = problem.TryGetRouteSection(nextRunSection?.Key);
               
               // ------------------------------------------------
               // Is there a min entry/exit time 
               var thisRequirement = trainRun.Train.GetRequirement(thisSection.SectionMarker);
               var nextRequirement = trainRun.Train.GetRequirement(nextSection?.SectionMarker);

               // ------------------------------------------------
               // Start time is always last exit time unless this is the first section
               if (k > 0) {
                  thisRunSection.EntryTime = trainRun.TrainRunSections[k - 1].ExitTime;
               }
               else {
                  thisRunSection.EntryTime = Math.Max(thisRunSection.EntryTime, thisRequirement.minEntryTime);

                  // Check resource occupation 
                  if (nextRunSection != null) {
                     foreach (var resId in thisSection.ResourceIds) {
                        foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                           thisRunSection.EntryTime = Math.Max(thisRunSection.EntryTime, usage.Item4);
                        }
                     }
                  }
               }

               // ------------------------------------------------
               // Set exit time, taking into acount resource occupation of next section
               thisRunSection.ExitTime = Math.Max(thisRequirement.minExitTime,
                  thisRunSection.EntryTime + thisSection.MinimumRunningTime +
                  thisRequirement.minStoppingTime);

               // Consider minEntryTime for next section
               thisRunSection.ExitTime = Math.Max(thisRunSection.ExitTime, nextRequirement.minEntryTime);

               // Check resource occupation 
               if (nextRunSection != null) {
                  foreach (var resId in nextSection.ResourceIds) {
                     foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                        thisRunSection.ExitTime = Math.Max(thisRunSection.ExitTime, usage.Item4);
                     }
                  }
               }

               // ------------------------------------------------
               // Add current resource occupation
               foreach (var resId in thisSection.ResourceIds) {
                  var resource = problem.TryGetResource(resId);
                  if (resource != null) {
                     usedResources.Add(resId, trainRun.ServiceIntentionId, thisRunSection.SequenceNumber,
                        thisRunSection.EntryTime, thisRunSection.ExitTime + resource.ReleaseTime);
                  }
               }
            }
         }
      }
   }
}