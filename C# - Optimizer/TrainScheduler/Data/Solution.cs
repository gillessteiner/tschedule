using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Msagl.Drawing;
using TrainScheduler.Utils;
using Math = System.Math;

namespace TrainScheduler.Data {
   [DataContract]
   public class Solution : Serializable {

      public Solution(ProblemInstance problem) {
         Problem = problem;
         TrainRuns = Problem.ServiceIntentions.Select(t => new TrainRun(t, Problem.RouteDic[t.Route])).ToArray();
      }

      #region Also Deserializable
      public void FromJson(string json) {
         var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
         var ser = new DataContractJsonSerializer(this.GetType());
         this.CopyFrom(ser.ReadObject(ms));
         ms.Close();
      }
      
      private void CopyFrom(object other) {
         if (other is Solution src) {
            TrainRuns = src.TrainRuns;
            ConnectToProblem();
            ComputeObjectiveFunction();
         }
         else {
            throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
         }
      }

      private void ConnectToProblem() {
         foreach (var tr in TrainRuns) {
            tr.Train = Problem.ServiceIntentionsDic[tr.ServiceIntentionId];
            tr.Route = Problem.RouteDic[tr.Train.Route];
         }
      }
      #endregion

      #region SBB Data Model

      [DataMember(Name = "problem_instance_label", Order = 1)]
      private string ProblemLabel {
         get => Problem?.Label;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "problem_instance_hash", Order = 2)]
      private int ProblemHash
      {
         get => Problem?.Hash ?? 0;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "hash", Order = 3)] public int Hash { get; private set; } = 101;

      [IgnoreDataMember]
      private TrainRun[] _trainRuns;

      [DataMember(Name = "train_runs", Order = 4)]
      internal TrainRun[] TrainRuns {
         get => _trainRuns;
         set {
            _trainRuns = value;
            TrainRunsDic = _trainRuns.ToDictionary(tr => tr.ServiceIntentionId, tr => tr);
         }
      }

      #endregion

      [IgnoreDataMember] public Dictionary<string, TrainRun> TrainRunsDic { get; private set; }

      [IgnoreDataMember] public ProblemInstance Problem { get; private set; }

      [IgnoreDataMember] public bool IsAdmissible { get; private set; } = false;

      [IgnoreDataMember] public bool IsOptimal => Math.Abs(ObjectiveValue) < 1e-10;

      [IgnoreDataMember] public double ObjectiveValue { get; private set; } = 10000;

      public string Validate() {
         #region Consistency rules

         #region Business rules 1 and 2 are implicitely satisfied

         #endregion

         #region Business rule 3

         foreach (var run in TrainRuns) {
            var seq = run.TrainRunSections.Select(trs => trs.SequenceNumber).ToArray();
            var distinct = seq.Distinct().ToArray();

            if (seq.Length != distinct.Length) {
               IsAdmissible = false;
               return $"Run {run.ServiceIntentionId} violates business rule 3";
            }
         }

         #endregion

         #region Business rule 4

         foreach (var run in TrainRuns) {
            foreach (var section in run.TrainRunSections) {
               if (Problem.TryGetRouteSection(section.Key) == null) {
                  IsAdmissible = false;
                  return $"Run {run.ServiceIntentionId} - section {section.SequenceNumber} violates business rule 4 : Invalid SequenceNumber {section.SequenceNumber}";
               }
            }
         }

         #endregion

         #region Business rule 5 is implicitely satisfied

         #endregion

         #region Business rule 6

         foreach (var run in TrainRuns) {
            // Check that all requirements are taken into account
            var requirements = new HashSet<string>(run.Train.SectionRequirementsDic.Keys);

            foreach (var section in run.TrainRunSections) {
               if (section.SectionRequirement != null && requirements.Contains(section.SectionRequirement)) {
                  requirements.Remove(section.SectionRequirement);
               }

               if (!requirements.Any()) break;
            }

            if (requirements.Any()) {
               IsAdmissible = false;
               return
                  $"Run {run.ServiceIntentionId} violates business rule 6 : Section requirements {string.Join(",", requirements)} not considered";
            }
         }

         #endregion

         #region Business rule 7

         foreach (var run in TrainRuns) {
            for (int k = 1; k < run.TrainRunSections.Count; ++k) {
               if (run.TrainRunSections[k].EntryTime != run.TrainRunSections[k - 1].ExitTime) {
                  IsAdmissible = false;
                  return
                     $"Run {run.ServiceIntentionId} violates business rule 7 : Section {run.TrainRunSections[k - 1].SequenceNumber} exit time != {run.TrainRunSections[k].SequenceNumber} entry time";
               }
            }
         }

         #endregion

         #endregion

         #region Planning rules

         #region Business rule 101 is not mandatory | it is enforced via Objective value

         #endregion

         #region Business rule 102

         foreach (var train in TrainRuns) {
            foreach (var section in train.TrainRunSections) {
               if (section.SectionRequirement != null) {
                  if (train.Train.SectionRequirementsDic.ContainsKey(section.SectionRequirement)) {
                     if (train.Train.SectionRequirementsDic[section.SectionRequirement].EntryEarliest.HasValue) {
                        if (section.EntryTime < train.Train.SectionRequirementsDic[section.SectionRequirement].EntryEarliest.Value) {
                           IsAdmissible = false;
                           return $"Train {train.ServiceIntentionId}, planning rule 102 violated : Section {section.SequenceNumber} entry time ({section.EntryTimeStr}) < earliest possible ${train.Train.SectionRequirementsDic[section.SectionRequirement].EntryEarliest.Value.ToLongTimeString()} entry time";
                        }
                     }

                     if (train.Train.SectionRequirementsDic[section.SectionRequirement].ExitEarliest.HasValue) {
                        if (section.ExitTime < train.Train.SectionRequirementsDic[section.SectionRequirement].ExitEarliest.Value) {
                           IsAdmissible = false;
                           return $"Train {train.ServiceIntentionId}, planning rule 102 violated : Section {section.SequenceNumber} exit time ({section.ExitTimeStr}) < earliest possible ${train.Train.SectionRequirementsDic[section.SectionRequirement].ExitEarliest.Value.ToLongTimeString()} exit time";
                        }
                     }
                  }
               }
            }
         }

         #endregion

         #region Business rule 103

         foreach (var train in TrainRuns) {
            foreach (var runSection in train.TrainRunSections) {
               if (runSection.SectionRequirement != null) {
                  var minStop = TimeSpan.Zero;
                  if (train.Train.SectionRequirementsDic.ContainsKey(runSection.SectionRequirement)) {
                     minStop = train.Train.SectionRequirementsDic[runSection.SectionRequirement].MinStoppingTime;
                  }

                  var section = Problem.TryGetRouteSection(runSection.Key);
                  if ((runSection.ExitTime - runSection.EntryTime) < section.MinimumRunningTime + minStop) {
                     IsAdmissible = false;
                     return $"Train {train.ServiceIntentionId}, planning rule 103 violated : Section {runSection.SequenceNumber} exit-entry time ({runSection.ExitTime - runSection.EntryTime}) < min_running_time + min_stop_time {section.MinimumRunningTime + minStop}";
                  }
               }
            }
         }

         #endregion

         #region Business rule 104

         var conflicts = GetConflicts();
         if (conflicts.Any()) {
            IsAdmissible = false;
            return
               $"Planning rule 104 violated : {conflicts.Count} conflicts detected.";
         }
        
         #endregion

         #region Business rule 105 - Connections

         #endregion

         #endregion

         IsAdmissible = true;
         return null;
      }

      public bool AssignPaths() {
         foreach (var trainRun in TrainRuns) {
            if (!trainRun.IsPathSelected) // OR some conflicts that require to change track
            {
               trainRun.SelectNextPath();
            }
         }

         return true;
      }


      public void Update() {
         foreach (var trainRun in TrainRuns) {
            trainRun.CreateGraph();
         }
      }

      public void ComputeObjectiveFunction() {
         ObjectiveValue = 0.0;
         foreach (var trainRun in TrainRuns) {
            trainRun.ObjectiveValue = 0.0;
            foreach (var section in trainRun.TrainRunSections) {
               var routeSection = Problem.TryGetRouteSection(section.Key);
               trainRun.ObjectiveValue += routeSection?.Penalty ?? 0;

               var delays = trainRun.Train.GetLatestTime(routeSection.SectionMarker);

               double entryDelay = (section.EntryTime > delays.maxEntryTime) ? (section.EntryTime - delays.maxEntryTime).TotalMinutes : 0.0;
               double exitDelay = (section.ExitTime > delays.maxExitTime) ? (section.ExitTime - delays.maxExitTime).TotalMinutes : 0.0;

               trainRun.ObjectiveValue += entryDelay * delays.entryDelayWeight + exitDelay * delays.exitDelayWeight;
            }

            ObjectiveValue += trainRun.ObjectiveValue;
         }
      }

      public Dictionary<string, Dictionary<string, List<Conflict>>> GetConflicts() {
         var res = new Dictionary<string, Dictionary<string, List<Conflict>>>();

         var usedResources = new UsedResourceCollection();
         foreach (var train in TrainRuns) {
            var train1 = train.ServiceIntentionId;
            foreach (var runSection1 in train.TrainRunSections) {
               var section1 = Problem.TryGetRouteSection(runSection1.Key);
               foreach (var resId in section1.ResourceIds) {
                  var resource = Problem.TryGetResource(resId);
                  if (resource != null) {
                     foreach (var conflict in usedResources.TryAdd(resId, train1,
                        runSection1.SequenceNumber,
                        runSection1.EntryTime,
                        runSection1.ExitTime + resource.ReleaseTime)) {

                        // Add conflict for both trains
                        if(!res.ContainsKey(train1)) res.Add(train1, new Dictionary<string, List<Conflict>>());
                        if (!res.ContainsKey(conflict.trainId)) res.Add(conflict.trainId, new Dictionary<string, List<Conflict>>());

                        if(!res[train1].ContainsKey(conflict.trainId)) res[train1].Add(conflict.trainId, new List<Conflict>());
                        if (!res[conflict.trainId].ContainsKey(train1)) res[conflict.trainId].Add(train1, new List<Conflict>());

                        res[train1][conflict.trainId].Add(new Conflict() {
                           Resource = resId,
                           Section1 = runSection1.SequenceNumber, Section2 = conflict.sectionId,
                           Entry1 = runSection1.EntryTime, Entry2 = conflict.start,
                           Exit1 = runSection1.ExitTime + resource.ReleaseTime, Exit2 = conflict.end
                        });

                        res[conflict.trainId][train1].Add(new Conflict() {
                           Resource = resId,
                           Section2 = runSection1.SequenceNumber, Section1 = conflict.sectionId,
                           Entry2 = runSection1.EntryTime, Entry1 = conflict.start,
                           Exit2 = runSection1.ExitTime + resource.ReleaseTime, Exit1 = conflict.end
                        });
                     }
                  }
               }
            }
         }
         return res;
      }


      public void AssignRandomPaths() {
         foreach (var trainRun in TrainRuns) {
            trainRun.AssignRandomPath();
         }
      }

      
   }
}
            