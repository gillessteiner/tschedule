using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class Solution : Serializable
    {
        public Solution()
        {
            TrainRuns = new List<TrainRun>();
        }

        public Solution(ProblemInstance problem) : this()
        {
            ProblemLabel = problem.Label;
            ProblemHash = problem.Hash;
        }

        [DataMember(Name = "problem_instance_label", Order = 1)]
        public string ProblemLabel { get; private set; }

        [DataMember(Name = "problem_instance_hash", Order = 2)]
        public int ProblemHash { get; private set; }

        [DataMember(Name = "hash", Order = 3)]
        public int Hash => 101;

        [DataMember(Name = "train_runs", Order = 4)]
        public List<TrainRun> TrainRuns { get; private set; }

        [IgnoreDataMember]
        public bool IsOptimal { get; private set; } = false;

        protected override void CopyFrom(object other)
        {
            if (other is Solution src)
            {
                ProblemLabel = src.ProblemLabel;
                ProblemHash = src.ProblemHash;
                TrainRuns = src.TrainRuns;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}
