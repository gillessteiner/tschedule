using System;
using System.Collections.Generic;
using Data;
using Data.Model;
using Data.SBB;
using Utils;
using Math = Utils.Math;

namespace Solver
{
   public abstract class BaseSolver : IDisposable
   {
      public enum SolverType
      {
         DummySolver,
      }

      protected int Iteration { get; set; } = 1;
      public int MaxIteration { get; set; }
      protected int SubIteration { get; set; } = 1;
      public int MaxSubIteration { get; set; }

      protected Problem  CurrentProblem { get; set; }
      protected Solution BestSolution { get; set; }
      
      /// <summary>
      /// Factory method for solver creation
      /// </summary>
      public static BaseSolver Create(SolverType type) {
         switch (type) {
            case SolverType.DummySolver:
               return new DummySolver();
         default:
               throw new ArgumentOutOfRangeException(nameof(type), type, null);
         }
      }

      public virtual void Init(Problem problem) {
         Log(global::Utils.Logging.HorizontalLine);
         Log($"Initializing {GetType()} ...", global::Utils.Logging.LogEventArgs.MessageType.Info , false);
         CurrentProblem = problem;
         Log(" done.", global::Utils.Logging.LogEventArgs.MessageType.Success);
      }

      public abstract Solution Run();

      protected void CompareWithBest(Solution currentSolution) {
#if DEBUG
         currentSolution.Validate(); // In debug mode we validate the solution each time
#endif
         currentSolution.EvalObjectiveFunction();

         if (BestSolution == null || currentSolution.ObjectiveValue < BestSolution.ObjectiveValue) {
            BestSolution = currentSolution.Clone();
            Log($"New solution with objective value {BestSolution.ObjectiveValue:N2}", Utils.Logging.LogEventArgs.MessageType.Success);
         }
         else {
            Log($"Worse objective value {currentSolution.ObjectiveValue:N2}", Utils.Logging.LogEventArgs.MessageType.Warning);
         }
      }

      /// <summary>
      /// Schedule all train in an enumeration diregarding resource occupation and connections
      /// </summary>
      protected void BasicSchedule(IEnumerable<TrainRun> trains) {
         foreach (var trainRun in trains) {
            var train = CurrentProblem.GetTrain(trainRun.ServiceIntentionId);
            // ----------------------------------------------------
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
               thisSection.EntryTime = k > 0 ? trainRun.TrainRunSections[k - 1].ExitTime : thisRequirement.EntryEarliest ;

               // ------------------------------------------------
               // Set exit time
               thisSection.ExitTime = Math.Max(thisRequirement.ExitEarliest,
                  thisSection.EntryTime + thisSection.MinimumRunningTime +
                  thisRequirement.MinStoppingTime);

               // Consider minEntryTime for next section
               thisSection.ExitTime = Math.Max(thisSection.ExitTime, nextRequirement.EntryEarliest);
            }
         }
      }

      #region Event for logging 

      public event Logging.LogEventHandler Logging;

      protected void Log(string msg, Logging.LogEventArgs.MessageType type = global::Utils.Logging.LogEventArgs.MessageType.Info, bool newline = true) {
         Logging?.Invoke(this, new Logging.LogEventArgs() { Message = msg, Type = type, Newline = newline });
      }

      #endregion

      #region IDisposable
      protected virtual void Dispose(bool disposing) {
         if (disposing) { }
      }

      public void Dispose() {
         Dispose(true);
         GC.SuppressFinalize(this);
      }
      #endregion

   }
}
