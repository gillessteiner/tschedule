using System;
using System.Linq;
using System.Runtime.Serialization;
using Data.SBB;
using Math = Utils.Math;

namespace Data.Model
{
   [DataContract]
   public class Section
   {
      public Section(string routeId, string routePathId, int index, RouteSection section) {
         RouteId = routeId;
         RoutePathId = routePathId;
         Index = index;
         SequenceNumber = section.SequenceNumber;
         MinimumRunningTime = section.MinimumRunningTime;
         ResourcesOccupied = section.ResourceOccupations.Any() ? section.ResourceOccupations.Select(r => r.ResourceId).ToArray() : new string[0];
         Penalty = section.Penalty ?? 0;
         AlternativeMarkerAtEntry = section.AlternativeMarkerAtEntry;
         AlternativeMarkerAtExit = section.AlternativeMarkerAtExit;
         SectionMarker = section.SectionMarkers?.FirstOrDefault();

         Key = GetKey(RouteId, SequenceNumber);
      }

      // Deep copy
      public Section(Section other, bool reinit = true) {
         RouteId = other.RouteId;
         RoutePathId = other.RoutePathId;
         SequenceNumber = other.SequenceNumber;
         MinimumRunningTime = other.MinimumRunningTime;
         ResourcesOccupied = other.ResourcesOccupied;
         Penalty = other.Penalty;
         AlternativeMarkerAtEntry = other.AlternativeMarkerAtEntry;
         AlternativeMarkerAtExit = other.AlternativeMarkerAtExit;
         SectionMarker = other.SectionMarker;
         SectionRequirement = other.SectionRequirement;
         Index = other.Index;
         Key = other.Key;

         // Reinit these
         if(reinit)
            EntryTime = ExitTime = DateTime.Today;
         else {
            EntryTime = other.EntryTime;
            ExitTime = other.ExitTime;
         }
      }  
      
      #region From Problem
      [DataMember(Name = "route", Order = 3)]
      public string RouteId { get; private set; }

      [DataMember(Name = "route_path", Order = 6)]
      public string RoutePathId { get; private set; }

      [DataMember(Name = "sequence_number", Order = 5)]
      public int SequenceNumber { get; private set; }

      [IgnoreDataMember]
      public TimeSpan MinimumRunningTime { get; private set; }

      [IgnoreDataMember]
      public string[] ResourcesOccupied { get; private set; }

      [IgnoreDataMember]
      internal double Penalty { get; private set; }

      [IgnoreDataMember]
      internal string AlternativeMarkerAtEntry { get; private set; }

      [IgnoreDataMember]
      internal string AlternativeMarkerAtExit { get; private set; }

      [IgnoreDataMember]
      public string SectionMarker { get; private set; }
      #endregion

      private int Index { get; set; }

      [DataMember(Name = "route_section_id", Order = 4)]
      public string Key { get; private set; }

      public static string GetKey(string routeId, int seqNum) => $"{routeId}#{seqNum}";

      [IgnoreDataMember]
      public string EntryMarker => AlternativeMarkerAtEntry ?? $"{RoutePathId}{Index - 1}";

      [IgnoreDataMember]
      public string ExitMarker => AlternativeMarkerAtExit ?? $"{RoutePathId}{Index}";

      #region For Solution
      [IgnoreDataMember]
      public DateTime EntryTime { get; set; }

      [DataMember(Name = "entry_time", Order = 1)]
      public string EntryTimeStr
      {
         get => EntryTime.ToLongTimeString();
         private set => EntryTime = value != null ? DateTime.Parse(value) : DateTime.Today;
      }

      [IgnoreDataMember]
      public DateTime ExitTime { get; set; }

      [DataMember(Name = "exit_time", Order = 2)]
      public string ExitTimeStr
      {
         get => ExitTime.ToLongTimeString();
         private set => ExitTime = value != null ? DateTime.Parse(value) : DateTime.Today;
      }

      [DataMember(Name = "section_requirement", Order = 7)]
      public string SectionRequirement { get; internal set; }

      [IgnoreDataMember]
      public Math.Period OccupiedPeriod => new Math.Period(EntryTime, ExitTime);
      #endregion

   }
}
