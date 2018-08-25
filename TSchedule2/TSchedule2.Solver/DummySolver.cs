using System.Collections.Generic;
using System.Linq;
using Data.SBB;
using Utils;

namespace Solver {
   public class DummySolver : BaseSolver {

      public override Solution Run() {
         do {
            Log(global::Utils.Logging.HorizontalLine);
            Log($"Iteration {Iteration}...");
            // -----------------------------------------------------------
            // Generate a new solution every time
            var currentSolution = new Solution(CurrentProblem);
            
            // -----------------------------------------------------------
            Log($"Reorder train randomly...");
            var randomTrainOrder = currentSolution.GetTrainRuns(CurrentProblem.TrainKeys.Shuffle()).ToArray();
            do {
               var resourceUsage = new ResourceUsageCollection();

               // -----------------------------------------------------------
               Log($"Assign a random path for each train...");
               foreach (var t in CurrentProblem.TrainKeys) {
                  var path = CurrentProblem.GetTrain(t).GetRandomPath();
                  currentSolution.GetTrainRun(t).AssignPath(CurrentProblem.GetTrain(t), path);
               }

               // -----------------------------------------------------------
               Log($"Schedule all trains ignoring resources ...");
               this.BasicSchedule(randomTrainOrder);

               // -----------------------------------------------------------
               Log($"Take into account resource occupation ...");
               // Delays some trains according to resource occupation
               // Prioritize trains on top of the list
               ScheduleTrains(randomTrainOrder, resourceUsage);

               // -----------------------------------------------------------
               CompareWithBest(currentSolution);

            } while (++SubIteration < MaxSubIteration && (BestSolution == null || !BestSolution.IsOptimal)); // Shuffle paths

         } while (++Iteration < MaxIteration && (BestSolution == null || !BestSolution.IsOptimal)); // Shuffle train ordering

         return BestSolution;
      }

      protected virtual void ScheduleTrains(IEnumerable<TrainRun> trainRuns, ResourceUsageCollection usedResources) {
         foreach (var trainRun in trainRuns) {
            var train = CurrentProblem.GetTrain(trainRun.ServiceIntentionId);

            // Schedule train accordingly
            for (int k = 0; k < trainRun.TrainRunSections.Count; ++k) {
               var thisSection = trainRun.TrainRunSections[k];
               var nextSection = (k < trainRun.TrainRunSections.Count - 1)
                  ? trainRun.TrainRunSections[k + 1]
                  : null;

               // ------------------------------------------------
               // Is there a min entry/exit time 
               var thisRequirement = train.GetRequirement(thisSection.SectionMarker);
               var nextRequirement = train.GetRequirement(nextSection?.SectionMarker);

               // ------------------------------------------------
               // Start time is always last exit time unless this is the first section
               if (k > 0) {
                  thisSection.EntryTime = trainRun.TrainRunSections[k - 1].ExitTime;
               }
               else {
                  thisSection.EntryTime = Math.Max(thisSection.EntryTime, thisRequirement.EntryEarliest);

                  // Check resource occupation 
                  if (nextSection != null) {
                     foreach (var resId in thisSection.ResourcesOccupied) {
                        foreach (var occupiedPeriod in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                           if (occupiedPeriod.ContainsStriclty(thisSection.EntryTime)) {
                              thisSection.EntryTime = Math.Max(thisSection.EntryTime, occupiedPeriod.End);
                           }
                        }
                     }
                  }
               }

               // ------------------------------------------------
               // Set exit time, taking into acount resource occupation of next section
               thisSection.ExitTime = Math.Max(thisRequirement.ExitEarliest,
                  thisSection.EntryTime + thisSection.MinimumRunningTime +
                  thisRequirement.MinStoppingTime);

               // Consider minEntryTime for next section
               thisSection.ExitTime = Math.Max(thisSection.ExitTime, nextRequirement.EntryEarliest);

               // Check resource occupation 
               if (nextSection != null) {
                  var timeChanged = false;
                  // Repeat this until no intersection is found
                  do {
                     foreach (var resId in nextSection.ResourcesOccupied) {
                        foreach (var occupiedPeriod in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId)) {
                           if (Math.Intersect(thisSection.OccupiedPeriod, occupiedPeriod)) {
                              timeChanged = true;
                              thisSection.ExitTime = Math.Max(thisSection.ExitTime, occupiedPeriod.End);
                           }
                        }
                     }
                  } while (timeChanged);
               }

               // ------------------------------------------------
               // Add current resource occupation
               foreach (var resId in thisSection.ResourcesOccupied) {
                  var resource = CurrentProblem.GetResource(resId);
                  usedResources.Add(resId, trainRun.ServiceIntentionId, thisSection.EntryTime, thisSection.ExitTime + resource.ReleaseTime);
               }
            }
         }
      }
   }
}