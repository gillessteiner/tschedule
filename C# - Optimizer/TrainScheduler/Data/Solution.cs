using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Msagl.Drawing;

namespace TrainScheduler.Data
{
    [DataContract]
    public class Solution : Serializable
    {
        public Solution(ProblemInstance problem)
        {
            Problem = problem;
            TrainRuns = Problem.ServiceIntentions.Select(t => new TrainRun(t, Problem.RouteDic[t.Route])).ToArray();
            TrainRunsDic = TrainRuns.ToDictionary(tr => tr.ServiceIntentionId, tr => tr);
        }

        #region SBB Data Model
        [DataMember(Name = "problem_instance_label", Order = 1)]
        public string ProblemLabel => Problem?.Label;

        [DataMember(Name = "problem_instance_hash", Order = 2)]
        public int ProblemHash => Problem?.Hash ?? 0;

        [DataMember(Name = "hash", Order = 3)]
        public int Hash => 101;

        [DataMember(Name = "train_runs", Order = 4)]
        private TrainRun[] TrainRuns { get; set; }
        #endregion

        public Dictionary<string, TrainRun> TrainRunsDic { get; private set; }

        [IgnoreDataMember]
        public ProblemInstance Problem { get; private set; }

        [IgnoreDataMember]
        public bool IsAdmissible { get; private set; } = false;

        [IgnoreDataMember]
        public bool IsOptimal => Math.Abs(ObjectiveValue) < 1e-10;

        public double ObjectiveValue => 1.0;

        public string Validate()
        {
            #region Business rules 1 and 2 are implicitely satisfied
            #endregion

            #region Business rule 3
            foreach (var run in TrainRuns)
            {
                var seq = run.TrainRunSections.Select(trs => trs.SequenceNumber).ToArray();
                var distinct = seq.Distinct().ToArray();

                if (seq.Length != distinct.Length)
                {
                    IsAdmissible = false;
                    return $"Run {run.ServiceIntentionId} violates business rule 3";
                }
            }
            #endregion

            #region Business rule 4

            foreach (var run in TrainRuns)
            {
                foreach (var section in run.TrainRunSections)
                {
                    if (Problem.RouteDic.ContainsKey(section.RouteId))
                    {
                        if (Problem.RouteDic[section.RouteId].RoutePathDic.ContainsKey(section.RoutePathId))
                        {
                            if (!Problem.RouteDic[section.RouteId].RoutePathDic[section.RoutePathId].RouteSectionDic
                                .ContainsKey(section.SequenceNumber))
                            {
                                IsAdmissible = false;
                                return
                                    $"Run {run.ServiceIntentionId} - section {section.SequenceNumber} violates business rule 4 : Invalid SequenceNumber {section.SequenceNumber}";
                            }
                            else
                            {
                                IsAdmissible = false;
                                return
                                    $"Run {run.ServiceIntentionId} - section {section.SequenceNumber} violates business rule 4 : Invalid RoutePathId {section.RoutePathId}";
                            }
                        }
                        else
                        {
                            IsAdmissible = false;
                            return
                                $"Run {run.ServiceIntentionId} - section {section.SequenceNumber} violates business rule 4 : Invalid RouteId {section.RouteId}";
                        }
                    }
                }
            }

            #endregion

            #region Business rule 5 is implicitely satisfied
            #endregion

            #region Business rule 6

            foreach (var run in TrainRuns)
            {
                // HashSet<string> RunMarkers = new HashSet<string>(run.TrainRunSections.Select());

            }

            #endregion

            IsAdmissible = true;
            return null;
        }

        public bool AssignPaths()
        {
            foreach (var trainRun in TrainRuns)
            {
                if (!trainRun.IsPathSelected) // OR some conflicts that require to change track
                {
                    trainRun.SelectNextPath();
                }
            }

            return true;
        }

      


    }
}
