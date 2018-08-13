using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class TrainRun : Serializable
    {
        [DataMember(Name="service_intention_id", Order = 1)]
        public int ServiceIntentionId { get; private set; }

        [DataMember(Name = "train_run_sections", Order = 2)]
        public List<TrainRunSection> TrainRunSections { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is TrainRun src)
            {
                ServiceIntentionId = src.ServiceIntentionId;
                TrainRunSections = src.TrainRunSections;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}