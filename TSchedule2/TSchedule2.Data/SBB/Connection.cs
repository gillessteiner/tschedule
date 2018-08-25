using System;
using System.Runtime.Serialization;
using Utils;

namespace Data.SBB {
   [DataContract]
   public class Connection {
      [DataMember(Name = "id", Order = 1)]
      internal string Id { get; private set; }

      [DataMember(Name = "onto_service_intention", Order = 2)]
      internal string OntoServiceIntention { get; private set; }

      [DataMember(Name = "onto_section_marker", Order = 3)]
      internal string OntoSectionMarker { get; private set; }

      [IgnoreDataMember]
      internal TimeSpan MinConnectionTime { get; private set; }

      [DataMember(Name = "min_connection_time", Order = 4)]
      private string MinConnectionTimeStr {
         get => MinConnectionTime.DurationToString();
         set => MinConnectionTime = value.StringToDuration();
      }
   }
}