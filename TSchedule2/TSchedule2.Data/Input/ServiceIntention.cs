using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TSchedule2.Data.Input {
   [DataContract]
   public class ServiceIntention {
      public ServiceIntention() { }

      [DataMember(Name = "id")]
      public string Id { get; private set; }

      [DataMember(Name = "route")]
      public string Route { get; private set; }

      [DataMember(Name = "section_requirements")]
      public SectionRequirement[] SectionRequirements { get; private set; }
   }
}