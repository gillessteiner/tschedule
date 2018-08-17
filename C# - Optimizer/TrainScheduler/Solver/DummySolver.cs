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
         var usedResources = new UsedResourceCollection();
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
            foreach (var trainRun in CurrentSolution.TrainRunsDic.Values
               .OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty)) {
               // Schedule train accordingly
               for (int k = 0; k < trainRun.TrainRunSections.Count; ++k) {
                  var thisSection = trainRun.TrainRunSections[k];
                  var nextSection = (k < trainRun.TrainRunSections.Count - 1)
                     ? trainRun.TrainRunSections[k + 1]
                     : null;

                  // ------------------------------------------------
                  // Is there a min entry/exit time 
                  var thisRequirement = trainRun.Train.GetRequirement(thisSection.SectionMarker);
                  var nextRequirement = trainRun.Train.GetRequirement(nextSection?.SectionMarker);

                  // ------------------------------------------------
                  // Start time is always last exit time unless this is the first section
                  if (k > 0) {
                     thisSection.EntryTime = trainRun.TrainRunSections[k - 1].ExitTime;
                  }
                  else {
                     thisSection.EntryTime = Math.Max(thisSection.EntryTime, thisRequirement.minEntryTime);

                     // Check resource occupation 
                     if (nextSection != null) {
                        foreach (var resId in thisSection.UnderlyingEdge.ResourceOccupations) {
                           foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                              thisSection.EntryTime = Math.Max(thisSection.EntryTime, usage.Item4);
                           }
                        }
                     }
                  }

                  // ------------------------------------------------
                  // Set exit time, taking into acount resource occupation of next section
                  thisSection.ExitTime = Math.Max(thisRequirement.minExitTime,
                     thisSection.EntryTime + thisSection.UnderlyingEdge.MinimumRunningTime +
                     thisRequirement.minStoppingTime);

                  // Consider minEntryTime for next section
                  thisSection.ExitTime = Math.Max(thisSection.ExitTime, nextRequirement.minEntryTime);

                  // Check resource occupation 
                  if (nextSection != null) {
                     foreach (var resId in nextSection.UnderlyingEdge.ResourceOccupations) {
                        foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                           thisSection.ExitTime = Math.Max(thisSection.ExitTime, usage.Item4);
                        }
                     }
                  }

                  // ------------------------------------------------
                  // Add current resource occupation
                  foreach (var resId in thisSection.UnderlyingEdge.ResourceOccupations) {
                     var resource = CurrentProblem.TryGetResource(resId);
                     if (resource != null) {
                        usedResources.Add(resId, trainRun.ServiceIntentionId, thisSection.SequenceNumber,
                           thisSection.EntryTime, thisSection.ExitTime + resource.ReleaseTime);
                     }
                  }
               }
            }

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
   }
}
