using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB {
   [DataContract]
   internal class ServiceIntention {
      [DataMember(Name = "id")]
      internal string Id { get; private set; }

      [DataMember(Name = "route")]
      internal string Route { get; private set; }

      [DataMember(Name = "section_requirements")]
      internal SectionRequirement[] SectionRequirements { get; private set; }
   }
}