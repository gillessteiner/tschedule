using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using TSchedule2.Data.Model;
using TSchedule2.Data.Utils;
using Math = System.Math;

namespace TSchedule2.Data.SBB
{
   [DataContract]
   public class Solution : Serializable
   {
      public Solution() { }

      public Solution(Problem problem) {
         Problem = problem;
         TrainRuns = Problem.TrainKeys.Select(t => new TrainRun(Problem.GetTrain(t))).ToArray();
      }

      [IgnoreDataMember]
      public Problem Problem { get; set; }

      [IgnoreDataMember] public double ObjectiveValue { get; private set; } = 10000;

      [IgnoreDataMember] public bool IsAdmissible { get; private set; } = false;

      [IgnoreDataMember] public bool IsOptimal => Math.Abs(ObjectiveValue) < 1e-10;

      #region SBB Data Model

      [DataMember(Name = "problem_instance_label", Order = 1)]
      private string ProblemLabel
      {
         get => Problem?.Label;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "problem_instance_hash", Order = 2)]
      private int ProblemHash
      {
         get => Problem?.Hash ?? 0;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "hash", Order = 3)]
      public int Hash { get; private set; } = 101;

      [IgnoreDataMember]
      private TrainRun[] _trainRuns;

      [DataMember(Name = "train_runs", Order = 4)]
      internal TrainRun[] TrainRuns {
         get => _trainRuns;
         set {
            _trainRuns = value;
            TrainRunsDic = _trainRuns.ToDictionary(tr => tr.ServiceIntentionId);
         }
      }

      [IgnoreDataMember]
      private Dictionary<string, TrainRun> TrainRunsDic { get; set; }

      [IgnoreDataMember]
      public IEnumerable<string> TrainKeys => TrainRunsDic?.Keys;

      public TrainRun GetTrainRun(string id) {
         return TrainRunsDic.ContainsKey(id) ? TrainRunsDic[id] : null;
      }

      #endregion

      public static Solution FromJson(string json) {
         var res = new Solution();
         var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
         var ser = new DataContractJsonSerializer(res.GetType());
         res = (Solution)ser.ReadObject(ms);
         ms.Close();
         return res;
      }

      public void EvalObjectiveFunction() {
         ObjectiveValue = 0.0;
         foreach (var trainRun in TrainRuns) {
            trainRun.ObjectiveValue = 0.0;
            var train = Problem.GetTrain(trainRun.ServiceIntentionId);
            foreach (var section in trainRun.TrainRunSections) {
               trainRun.ObjectiveValue += section.Penalty;
               var req = train.GetRequirement(section.SectionRequirement);
               if (req != null) {
                  var entryDelay = (section.EntryTime > req.EntryLatest) ? (section.EntryTime - req.EntryLatest).TotalMinutes : 0.0;
                  var exitDelay = (section.ExitTime > req.ExitLatest) ? (section.ExitTime - req.ExitLatest).TotalMinutes : 0.0;
                  trainRun.ObjectiveValue += entryDelay * req.EntryDelayWeight + exitDelay * req.ExitDelayWeight;
               }
            }

            ObjectiveValue += trainRun.ObjectiveValue;
         }
      }

      public string Validate() {
         IsAdmissible = false;

         #region Consistency rules

         #region Business rules 1 is implicitely satisfied
         #endregion
         
         #region Business rule 2

         foreach (var trainId in Problem.TrainKeys) {
            if (!TrainRunsDic.ContainsKey(trainId) || !TrainRunsDic[trainId].TrainRunSections.Any()) {
               return $"Solution violates business rule 2 : Train {trainId} is not scheduled.";
            }
         }
         #endregion
         
         #region Business rule 3

         foreach (var run in TrainRuns) {
            var seq = run.TrainRunSections.Select(trs => trs.SequenceNumber).ToArray();
            var distinct = seq.Distinct().ToArray();

            if (seq.Length != distinct.Length) {
               return $"Solution violates business rule 3 : Train run {run.ServiceIntentionId} sequence contains duplicates.";
            }
         }

         #endregion

         #region Business rule 4

         foreach (var run in TrainRuns) {
            foreach (var section in run.TrainRunSections) {
               if (Problem.GetTrack(section.RouteId) == null || Problem.GetTrack(section.RouteId).GetSection(section.Key) == null) {
                  return $"Solution violates business rule 4 : Train run {run.ServiceIntentionId} references some invalid route/section.";
               }
            }
         }

         #endregion

         #region Business rule 5 is implicitely satisfied

         #endregion

         #region Business rule 6

         foreach (var run in TrainRuns) {
            var train = Problem.GetTrain(run.ServiceIntentionId);
            // Check that all requirements are taken into account
            var requirements = new HashSet<string>(train.RequirementKeys);

            foreach (var section in run.TrainRunSections) {
               if (section.SectionRequirement != null) {
                  if (requirements.Contains(section.SectionRequirement)) {
                     requirements.Remove(section.SectionRequirement);
                  }
                  else {
                     if(train.GetRequirement(section.SectionRequirement) == null)
                        return $"Solution violates business rule 6 : Section {section.Key} for run {run.ServiceIntentionId}, has a requirement {section.SectionRequirement} not requested for this service intention.";
                  }
               }
            }

            if (requirements.Any()) {
               return
                  $"Solution violates business rule 6 : In run {run.ServiceIntentionId}, section requirements {string.Join(",", requirements)} are not considered.";
            }
         }

         #endregion

         #region Business rule 7

         foreach (var run in TrainRuns) {
            for (int k = 1; k < run.TrainRunSections.Count; ++k) {
               if (run.TrainRunSections[k].EntryTime != run.TrainRunSections[k - 1].ExitTime) {
                  return
                     $"Solution violates business rule 7 : In run {run.ServiceIntentionId}, section {run.TrainRunSections[k - 1].SequenceNumber} exit time != {run.TrainRunSections[k].SequenceNumber} entry time.";
               }
            }
         }

         #endregion

         #endregion

         #region Planning rules

         #region Business rule 101 is not mandatory | it is enforced via Objective value

         #endregion

         #region Business rule 102

         foreach (var trainRun in TrainRuns) {
            var train = Problem.GetTrain(trainRun.ServiceIntentionId);
            foreach (var section in trainRun.TrainRunSections) {
               var requirement = train.GetRequirement(section.SectionRequirement);
               if (requirement != null) {
                  if (section.EntryTime < requirement.EntryEarliest) {
                     return
                        $"Solution violates business rule 102 : In run {trainRun.ServiceIntentionId}, train enter section {section.SectionRequirement} earlier than earliest possible time.";
                  }

                  if (section.ExitTime < requirement.ExitEarliest) {
                     return
                        $"Solution violates business rule 102 : In run {trainRun.ServiceIntentionId}, train exit section {section.SectionRequirement} earlier than earliest possible time.";
                  }
               }
            }
         }

         #endregion

         #region Business rule 103

         foreach (var trainRun in TrainRuns) {
            var train = Problem.GetTrain(trainRun.ServiceIntentionId);
            foreach (var section in trainRun.TrainRunSections) {
               TimeSpan minPresenceTime = section.MinimumRunningTime;
               var requirement = train.GetRequirement(section.SectionRequirement);
               if (requirement != null) {
                  minPresenceTime += requirement.MinStoppingTime;
               }

               if ((section.ExitTime - section.EntryTime) < minPresenceTime) {
                  return
                     $"Solution violates business rule 103 : In run {trainRun.ServiceIntentionId}, train does not spend enough time in section {section.SectionRequirement}.";

               }
            }
         }

         #endregion

         #region Business rule 104
         // TBD
         #endregion

         #region Business rule 105 - Connections
         // TBD
         #endregion

         #endregion

         IsAdmissible = true;
         return null;
      }

      public IEnumerable<TrainRun> GetTrainRuns(IEnumerable<string> trainIds) {
         return trainIds.Select(GetTrainRun);
      }
   }
}
