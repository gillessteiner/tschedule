using System;
using System.Runtime.Serialization;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.Input {
   [DataContract]
   public class Connection {
      [DataMember(Name = "id", Order = 1)]
      public string Id { get; private set; }

      [DataMember(Name = "onto_service_intention", Order = 2)]
      public string OntoServiceIntention { get; private set; }

      [DataMember(Name = "onto_section_marker", Order = 3)]
      public string OntoSectionMarker { get; private set; }

      [IgnoreDataMember]
      public TimeSpan MinConnectionTime { get; private set; }

      [DataMember(Name = "min_connection_time", Order = 4)]
      private string MinConnectionTimeStr {
         get => MinConnectionTime.DurationToString();
         set => MinConnectionTime = value.StringToDuration();
      }
   }
}