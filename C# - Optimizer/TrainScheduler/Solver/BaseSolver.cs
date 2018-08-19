using System;
using System.Collections.Generic;
using TrainScheduler.Data;
using TrainScheduler.Utils;
using Math = TrainScheduler.Utils.Math;

namespace TrainScheduler.Solver {
   public abstract class BaseSolver {
      public enum SolverType {
         DummySolver,
         IterativeDummySolver,
         IterativeConflictSolver,
         SmarterConflictSolver
      }

      public int Iteration { get; protected set; } = 1;
      public int MaxIteration { get; set; }
      public int SubIteration { get; set; }


      protected ProblemInstance CurrentProblem { get; set; }
      protected Solution CurrentSolution { get; set; }
      
      public Solution BestSolution { get; protected set; }

      protected static readonly string HorizontalLine = new string('-', 100);

      /// <summary>
      /// Factory method for solver creation
      /// </summary>
      public static BaseSolver Create(SolverType type) {
         switch (type) {
            case SolverType.DummySolver:
               return new DummySolver();
            case SolverType.IterativeDummySolver:
               return new IterativeDummySolver();
            case SolverType.IterativeConflictSolver:
               return new IterativeConflictSolver();
            case SolverType.SmarterConflictSolver:
               return new SmarterConflictSolver();
            default:
               throw new ArgumentOutOfRangeException(nameof(type), type, null);
         }
      }

      public virtual void Init(ProblemInstance problem) {
         Log(HorizontalLine);
         Log($"Initializing {GetType()} ...", Utils.Logging.LogEventArgs.MessageType.Info, false);
         CurrentProblem = problem;
         CurrentSolution = new Solution(problem);
         Log(" done.", Utils.Logging.LogEventArgs.MessageType.Success);
      }

      protected void CompareWithBest(Solution someSolution) {
         if (BestSolution == null || (someSolution.ObjectiveValue < BestSolution.ObjectiveValue)) {
            BestSolution = someSolution;
            Log($"Found a better solution {someSolution.ObjectiveValue:F2}");
         }
      }

      /// <summary>
      /// Schedule all train in an enumeration diregarding resource occupation and connections
      /// </summary>
      public void BasicSchedule(IEnumerable<TrainRun> trains) {
         foreach (var trainRun in trains) {
            // ----------------------------------------------------
            for (int k = 0; k < trainRun.TrainRunSections.Count; ++k) {
               var thisRunSection = trainRun.TrainRunSections[k];
               var nextRunSection = (k < trainRun.TrainRunSections.Count - 1)
                  ? trainRun.TrainRunSections[k + 1]
                  : null;

               var thisSection = CurrentProblem.TryGetRouteSection(thisRunSection.Key);
               var nextSection = CurrentProblem.TryGetRouteSection(nextRunSection?.Key);

               // ------------------------------------------------
               // Is there a min entry/exit time 
               var thisRequirement = trainRun.Train.GetRequirement(thisSection?.SectionMarker);
               var nextRequirement = trainRun.Train.GetRequirement(nextSection?.SectionMarker);

               // ------------------------------------------------
               // Start time is always last exit time unless this is the first section
               thisRunSection.EntryTime = k > 0 ? trainRun.TrainRunSections[k - 1].ExitTime : thisRequirement.minEntryTime;

               // ------------------------------------------------
               // Set exit time
               thisRunSection.ExitTime = Math.Max(thisRequirement.minExitTime,
                  thisRunSection.EntryTime + thisSection.MinimumRunningTime +
                  thisRequirement.minStoppingTime);

               // Consider minEntryTime for next section
               thisRunSection.ExitTime = Math.Max(thisRunSection.ExitTime, nextRequirement.minEntryTime);
            }

            trainRun.IsScheduled = true;
         }
      } 

      public abstract Solution Run();

      #region Event for logging 

      public event Logging.LogEventHandler Logging;

      protected void Log(string msg, Logging.LogEventArgs.MessageType type = Utils.Logging.LogEventArgs.MessageType.Info, bool newline = true) {
         Logging?.Invoke(this, new Logging.LogEventArgs() {Message = msg, Type = type, Newline = newline});
      }

      #endregion

   }
}
