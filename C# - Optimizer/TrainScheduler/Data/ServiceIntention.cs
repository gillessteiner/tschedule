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
                SectionRequirementsDic = _sectionRequirements.ToDictionary(sr => sr.SectionMarker);
                MaxDelayPenalty =
                    _sectionRequirements.Max(sr => Math.Max(sr.EntryDelayWeight ?? 0, sr.ExitDelayWeight ?? 0));
                MinEntryEarliest = _sectionRequirements.Where(sr => sr.EntryEarliest.HasValue)
                    .Min(sr => sr.EntryEarliest.Value);
            }
        }

        [IgnoreDataMember] public DateTime MinEntryEarliest { get; private set; } = DateTime.Today;

        [IgnoreDataMember]
        public double MaxDelayPenalty { get; private set; }

        [IgnoreDataMember] public Dictionary<string, SectionRequirement> SectionRequirementsDic { get; private set; }

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