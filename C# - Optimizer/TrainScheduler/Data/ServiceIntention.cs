using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class ServiceIntention : DeSerializable
    {
        public ServiceIntention()
        {
            SectionRequirements = new List<SectionRequirement>();
        }

        [DataMember(Name = "id")] public string Id { get; private set; }

        [DataMember(Name = "route")] public string Route { get; private set; }

        [IgnoreDataMember] private List<SectionRequirement> _sectionRequirements;

        [DataMember(Name = "section_requirements")]
        public List<SectionRequirement> SectionRequirements
        {
            get => _sectionRequirements;
            private set
            {
                _sectionRequirements = value;
                SectionRequirementsMarkers = new HashSet<string>(_sectionRequirements.Select(sr => sr.SectionMarker));
            }

        }

        [IgnoreDataMember] public HashSet<string> SectionRequirementsMarkers { get; private set; }

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
                throw new InvalidCastException(
                    $"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}