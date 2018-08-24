using System;
using System.Globalization;
using System.Runtime.Serialization;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.SBB {
   [DataContract]
   public class SectionRequirement {
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
      public DateTime EntryEarliest { get; private set; } = DateTime.MinValue;

      [DataMember(Name = "entry_earliest", Order = 4)]
      private string EntryEarliestStr {
         get => (EntryEarliest != DateTime.MinValue) ? EntryEarliest.ToLongTimeString() : null;
         set => EntryEarliest = value != null ? DateTime.Parse(value) : DateTime.MinValue;
      }

      [IgnoreDataMember]
      public DateTime EntryLatest { get; internal set; } = DateTime.MaxValue;

      [DataMember(Name = "entry_latest", Order = 5)]
      private string EntryLatestStr {
         get => (EntryLatest != DateTime.MaxValue) ? EntryLatest.ToLongTimeString() : null;
         set => EntryLatest = (value != null) ? DateTime.Parse(value) : DateTime.MaxValue;
      }

      [IgnoreDataMember]
      public DateTime ExitEarliest { get; private set; } = DateTime.MinValue;

      [DataMember(Name = "exit_earliest", Order = 6)]
      private string ExitEarliestSrc {
         get => (ExitEarliest != DateTime.MinValue) ? ExitEarliest.ToLongTimeString() : null;
         set => ExitEarliest = value != null ? DateTime.Parse(value) : DateTime.MinValue;
      }

      [IgnoreDataMember]
      public DateTime ExitLatest { get; internal set; } = DateTime.MaxValue;

      [DataMember(Name = "exit_latest", Order = 7)]
      private string ExitLatestSrc {
         get => (ExitLatest != DateTime.MaxValue) ? ExitLatest.ToLongTimeString() : null;
         set => ExitLatest = value != null ? DateTime.Parse(value) : DateTime.MaxValue;
      }

      [IgnoreDataMember]
      public double EntryDelayWeight { get; private set; } = 0.0;

      [DataMember(Name = "entry_delay_weight", Order = 8)]
      private string EntryDelayWeightStr {
         get => EntryDelayWeight.ToString(CultureInfo.InvariantCulture);
         set {
            if (value != null) {
               EntryDelayWeight = double.Parse(value);
            }
         }
      }

      [IgnoreDataMember]
      public double ExitDelayWeight { get; private set; } = 0.0;

      [DataMember(Name = "exit_delay_weight", Order = 9)]
      private string ExitDelayWeightStr {
         get => ExitDelayWeight.ToString(CultureInfo.InvariantCulture);
         set {
            if (value != null) {
               ExitDelayWeight = double.Parse(value);
            }
         }
      }

      [DataMember(Name = "connections", Order = 10)]
      public Connection[] Connections { get; private set; }
   }
}