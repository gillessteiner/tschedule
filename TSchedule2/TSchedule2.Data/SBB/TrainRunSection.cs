using System;
using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB
{
   [DataContract]
   public class TrainRunSection
   {
      [DataMember(Name = "route", Order = 3)]
      public string RouteId { get; private set; }

      [DataMember(Name = "route_section_id", Order = 4)]
      public string RouteSectionId
      {
         get => $"{RouteId}#{SequenceNumber}";
         private set { /* ignore setter */ }
      }

      [DataMember(Name = "sequence_number", Order = 5)]
      public int SequenceNumber { get; private set; }

      [DataMember(Name = "route_path", Order = 6)]
      public string RoutePathId { get; private set; }

      [DataMember(Name = "section_requirement", Order = 7)]
      public string SectionRequirement { get; private set; }

      [IgnoreDataMember]
      public DateTime EntryTime { get; internal set; }

      [DataMember(Name = "entry_time", Order = 1)]
      public string EntryTimeStr
      {
         get => EntryTime.ToLongTimeString();
         private set => EntryTime = value != null ? DateTime.Parse(value) : DateTime.Today;
      }

      [IgnoreDataMember]
      public DateTime ExitTime { get; internal set; }

      [DataMember(Name = "exit_time", Order = 2)]
      public string ExitTimeStr
      {
         get => ExitTime.ToLongTimeString();
         private set => ExitTime = value != null ? DateTime.Parse(value) : DateTime.Today;
      }

      [IgnoreDataMember]
      public string Key => $"{RouteId}#{SequenceNumber}";
   }
}
