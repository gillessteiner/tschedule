using System;
using System.Runtime.Serialization;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.SBB {
   [DataContract]
   public class SectionRequirement {
      public SectionRequirement() { }

      [DataMember(Name = "sequence_number", Order = 1)]
      public int SequenceNumber { get; private set; }

      [DataMember(Name = "section_marker", Order = 2)]
      public string SectionMarker { get; private set; }

      [IgnoreDataMember]
      public TimeSpan MinStoppingTime { get; private set; }

      [DataMember(Name = "min_stopping_time", Order = 3)]
      private string MinStoppingTimeStr {
         get => MinStoppingTime.DurationToString();
         set => MinStoppingTime = value.StringToDuration();
      }

      [IgnoreDataMember]
      public DateTime? EntryEarliest { get; private set; }

      [DataMember(Name = "entry_earliest", Order = 4)]
      private string EntryEarliestStr {
         get => EntryEarliest?.ToLongTimeString();
         set {
            if (value != null) {
               EntryEarliest = DateTime.Parse(value);
            }
            else
               EntryEarliest = null;
         }
      }

      [IgnoreDataMember]
      public DateTime? EntryLatest { get; private set; }

      [DataMember(Name = "entry_latest", Order = 5)]
      private string EntryLatestStr {
         get => EntryLatest?.ToLongTimeString();
         set {
            if (value != null) {
               EntryLatest = DateTime.Parse(value);
            }
            else
               EntryLatest = null;
         }
      }

      [IgnoreDataMember]
      public DateTime? ExitEarliest { get; private set; }

      [DataMember(Name = "exit_earliest", Order = 6)]
      private string ExitEarliestSrc {
         get => ExitEarliest?.ToLongTimeString();
         set {
            if (value != null) {
               ExitEarliest = DateTime.Parse(value);
            }
            else
               ExitEarliest = null;
         }
      }

      [IgnoreDataMember]
      public DateTime? ExitLatest { get; private set; }

      [DataMember(Name = "exit_latest", Order = 7)]
      private string ExitLatestSrc {
         get => ExitLatest?.ToLongTimeString();
         set {
            if (value != null) {
               ExitLatest = DateTime.Parse(value);
            }
            else
               ExitLatest = null;
         }
      }

      [DataMember(Name = "entry_delay_weight", Order = 8)]
      public double? EntryDelayWeight { get; private set; }

      [DataMember(Name = "exit_delay_weight", Order = 9)]
      public double? ExitDelayWeight { get; private set; }

      [DataMember(Name = "connections", Order = 10)]
      public Connection[] Connections { get; private set; }

   }
}