﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class SectionRequirement : DeSerializable
    {
        public SectionRequirement()
        {
            Connections = new List<Connection>();
        }

        [DataMember(Name = "sequence_number", Order = 1)]
        public int SequenceNumber { get; private set; }

        [DataMember(Name = "section_marker", Order = 2)]
        public string SectionMarker { get; private set; }

        [IgnoreDataMember]
        public TimeSpan MinStoppingTime { get; private set; }

        [DataMember(Name = "min_stopping_time", Order = 3)]
        private string MinStoppingTimeStr
        {
            get => IsoDuration.ToString(MinStoppingTime);
            set => MinStoppingTime = IsoDuration.FromString(value);
        }
       
        [IgnoreDataMember]
        public DateTime? EntryEarliest { get; private set; }

        [DataMember(Name = "entry_earliest", Order = 4)]
        private string EntryEarliestStr
        {
            get => EntryEarliest?.ToLongTimeString();
            set
            {
                if (value != null)
                {
                    EntryEarliest = DateTime.Parse(value);
                }
                else 
                    EntryEarliest = null;
            }
        }

        [IgnoreDataMember]
        public DateTime? EntryLatest { get; private set; }

        [DataMember(Name = "entry_latest", Order = 5)]
        private string EntryLatestStr
        {
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
        private string ExitEarliestSrc
        {
            get => ExitEarliest?.ToLongTimeString();
            set
            {
                if (value != null)
                {
                    ExitEarliest = DateTime.Parse(value);
                }
                else
                    ExitEarliest = null;
            }
        }

        [IgnoreDataMember]
        public DateTime? ExitLatest { get; private set; }

        [DataMember(Name = "exit_latest", Order = 7)]
        private string ExitLatestSrc
        {
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
        public List<Connection> Connections { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is SectionRequirement src)
            {
                SequenceNumber = src.SequenceNumber;
                SectionMarker = src.SectionMarker;
                MinStoppingTime = src.MinStoppingTime;
                EntryEarliest = src.EntryEarliest;
                ExitEarliest = src.ExitEarliest;
                EntryDelayWeight = src.EntryDelayWeight;
                ExitDelayWeight = src.ExitDelayWeight;
                Connections = src.Connections;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}