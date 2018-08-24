using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.SBB {
   [DataContract]
   public class RouteSection {
      [DataMember(Name = "sequence_number", Order = 1)]
      internal int SequenceNumber { get; private set; }

      [DataMember(Name = "penalty", Order = 2)]
      internal double? Penalty { get; private set; }

      [IgnoreDataMember]
      internal string AlternativeMarkerAtEntry =>
         (RouteAlternativeMarkersAtEntry != null && RouteAlternativeMarkersAtEntry.Any())
            ? RouteAlternativeMarkersAtEntry[0]
            : null;

      [DataMember(Name = "route_alternative_marker_at_entry", Order = 3)]
      internal string[] RouteAlternativeMarkersAtEntry { get; private set; }

      [IgnoreDataMember]
      internal string AlternativeMarkerAtExit =>
         (RouteAlternativeMarkersAtExit != null && RouteAlternativeMarkersAtExit.Any())
            ? RouteAlternativeMarkersAtExit[0]
            : null;

      [DataMember(Name = "route_alternative_marker_at_exit", Order = 4)]
      internal string[] RouteAlternativeMarkersAtExit { get; private set; }

      [DataMember(Name = "starting_point", Order = 5)]
      internal string StartingPoint { get; private set; }

      [DataMember(Name = "ending_point", Order = 6)]
      internal string EndingPoint { get; private set; }

      [IgnoreDataMember]
      internal TimeSpan MinimumRunningTime { get; private set; }

      [DataMember(Name = "minimum_running_time", Order = 7)]
      private string MinimumRunningTimeStr {
         get => MinimumRunningTime.DurationToString();
         set => MinimumRunningTime = value.StringToDuration();
      }

      [DataMember(Name = "resource_occupations", Order = 8)]
      internal ResourceOccupation[] ResourceOccupations { get; private set; }

      [DataMember(Name = "section_marker", Order = 9)]
      internal List<string> SectionMarkers { get; private set; }
   }
}