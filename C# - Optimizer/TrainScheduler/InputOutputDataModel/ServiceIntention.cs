using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class ServiceIntention : Serializable
    {
        public ServiceIntention()
        {
            SectionRequirements = new List<SectionRequirement>();
        }
        public ServiceIntention(string id, string route)
        {
            Id = id;
            Route = route;
        }

        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [DataMember(Name = "route")]
        public string Route { get; private set; }

        [DataMember(Name = "section_requirements")]
        public List<SectionRequirement> SectionRequirements { get; private set; }

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