using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Msagl.Drawing;

namespace TrainScheduler.Data {
   [DataContract]
   public class TrainRun : Serializable {
      public TrainRun(ServiceIntention train, Route route) {
         Train = train;
         Route = route;
      }

      [IgnoreDataMember] public ServiceIntention Train { get; private set; }

      [IgnoreDataMember]
      internal Route Route { get; set; }

      [DataMember(Name = "service_intention_id", Order = 1)]
      public string ServiceIntentionId {
         get => Train.Id;
         private set { /* ignore setter */
         }
      }

      [IgnoreDataMember] private List<TrainRunSection> _trainRunSections;

      [DataMember(Name = "train_run_sections", Order = 2)]
      public List<TrainRunSection> TrainRunSections {
         get => _trainRunSections;
         private set {
            _trainRunSections = value;
            CreateGraph();
         }
      }

      public int Order { get; set; }

      public double ObjectiveValue { get; set; } = 10000;

      public bool IsPathSelected => _selectedPathIndex >= 0;

      [IgnoreDataMember] private int _selectedPathIndex = -1;

      private bool SelectPath(int index) {
         if (index >= 0 && index < Route.Graph.PossiblePathsOrderedByPenalty.Length) {
            _selectedPathIndex = index;
            Route.Graph.PossiblePathsOrderedByPenalty[index].Visited = true;
            TrainRunSections = Route.Graph.PossiblePathsOrderedByPenalty[index].Sections
               .Select(sec => new TrainRunSection(Route, Train, sec)).ToList();
            return true;
         }

         return false;
      }

      public bool SelectNextPath() {
         return SelectPath(++_selectedPathIndex);
      }

      public void ApplyDelay(TimeSpan delay, int startIndex = 0) {
         if (delay > TimeSpan.Zero) {
            for (int k = startIndex; k < TrainRunSections.Count; ++k) {
               TrainRunSections[k].ExitTime += delay;
               if (k < TrainRunSections.Count - 1)
                  TrainRunSections[k + 1].EntryTime = TrainRunSections[k].ExitTime;
            }
         }
      }

      /*
       public bool TryChangePath()
      {
          if (Route.Graph.PossiblePathsOrderedByPenalty.Length < 2)
          {
              return false; // No other path
          }

          var nbConflictByPath = new List<(int pathIndex, int nbConflictByPath)>();
          for (int k=0; k < Route.Graph.PossiblePathsOrderedByPenalty.Length; ++k)
          {
              var path = Route.Graph.PossiblePathsOrderedByPenalty[k];
              if (!path.Visited)
                  nbConflictByPath.Add((k, path.HowMany(ConflictingResources)));
          }

          foreach (var path in nbConflictByPath.OrderBy(v => v.nbConflictByPath))
          {
              if (path.pathIndex != _selectedPathIndex)
              {
                  SelectPath(path.pathIndex);
                  return true;
              }
          }

          return false;
      }
      */

      #region Visualization

      [IgnoreDataMember] private Microsoft.Msagl.Drawing.Graph _msGraph;

      [IgnoreDataMember] public Microsoft.Msagl.Drawing.Graph MSGraph => _msGraph;

      public bool IsScheduled { get; set; } = false;

      internal void CreateGraph() {
         _msGraph = new Microsoft.Msagl.Drawing.Graph($"solution_graph") {
            Attr = {LayerDirection = LayerDirection.LR}
         };

         foreach (var edge in TrainRunSections) {
            _msGraph.AddEdge(edge.UnderlyingEdge.Entry.Marker, $"{edge.EntryTimeStr} - {edge.ExitTimeStr}",
               edge.UnderlyingEdge.Exit.Marker);
         }
      }

      #endregion


   }
}