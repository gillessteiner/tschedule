using System;
using System.Collections.Generic;
using TSchedule2.Data;
using TSchedule2.Data.Model;
using TSchedule2.Data.SBB;
using Math = TSchedule2.Data.Utils.Math;

namespace TSchedule2.Solver
{
   public abstract class BaseSolver : IDisposable
   {
      public enum SolverType
      {
         DummySolver,
      }

      public int Iteration { get; protected set; } = 1;
      public int MaxIteration { get; set; }
      public int SubIteration { get; set; }

      protected Problem  CurrentProblem { get; set; }
      protected Solution CurrentSolution { get; set; }
      public    Solution BestSolution { get; protected set; }
      
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
         Log(Data.Logging.HorizontalLine);
         Log($"Initializing {GetType()} ...", Data.Logging.LogEventArgs.MessageType.Info , false);
         CurrentProblem = problem;
         Log(" done.", Data.Logging.LogEventArgs.MessageType.Success);
      }

      public abstract Solution Run();

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

      protected void Log(string msg, Logging.LogEventArgs.MessageType type = Data.Logging.LogEventArgs.MessageType.Info, bool newline = true) {
         Logging?.Invoke(this, new Logging.LogEventArgs() { Message = msg, Type = type, Newline = newline });
      }

      #endregion

      protected virtual void Dispose(bool disposing) {
         if (disposing) { }
      }

      public void Dispose() {
         Dispose(true);
         GC.SuppressFinalize(this);
      }
   }
}
