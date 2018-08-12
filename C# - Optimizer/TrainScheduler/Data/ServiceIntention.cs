using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TrainScheduler.Data
{
    [DataContract]
    public class ServiceIntention : Serializable
    {
        public ServiceIntention()
        {
            SectionRequirements = new List<SectionRequirements>();
        }
        public ServiceIntention(int id, int route)
        {
            Id = id;
            Route = route;
        }

        [DataMember(Name = "id")]
        public int Id { get; private set; }

        [DataMember(Name = "route")]
        public int Route { get; private set; }

        [DataMember(Name = "section_requirements")]
        public List<SectionRequirements> SectionRequirements { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is ServiceIntention src)
            {
                Id = src.Id;
                Route = src.Route;
                SectionRequirements = src.SectionRequirements;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}