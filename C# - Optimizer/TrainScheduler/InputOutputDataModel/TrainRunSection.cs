using System;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class TrainRunSection : Serializable
    {
        [IgnoreDataMember]
        public DateTime? EntryTime { get; private set; }

        [DataMember(Name = "entry_time", Order = 1)]
        private string EntryTimeStr
        {
            get => EntryTime?.ToLongTimeString();
            set
            {
                if (value != null)
                {
                    EntryTime = DateTime.Parse(value);
                }
                else
                    EntryTime = null;
            }
        }

        [IgnoreDataMember]
        public DateTime? ExitTime { get; private set; }

        [DataMember(Name = "exit_time", Order = 2)]
        private string ExitTimeStr
        {
            get => ExitTime?.ToLongTimeString();
            set
            {
                if (value != null)
                {
                    ExitTime = DateTime.Parse(value);
                }
                else
                    ExitTime = null;
            }
        }

        [DataMember(Name="route", Order=3)]
        public int Route { get; private set; }

        [DataMember(Name = "route_section_id", Order = 4)]
        public string RouteSectionId { get; private set; }

        [DataMember(Name = "sequence_number", Order = 5)]
        public int SequenceNumber { get; private set; }

        [DataMember(Name = "route_path", Order = 6)]
        public int RoutePath { get; private set; }

        [DataMember(Name = "section_requirement", Order = 7)]
        public string SectionRequirement { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is TrainRunSection src)
            {
                EntryTime = src.EntryTime;
                ExitTime = src.ExitTime;
                Route = src.Route;
                RouteSectionId = src.RouteSectionId;
                SequenceNumber = src.SequenceNumber;
                RoutePath = src.RoutePath;
                SectionRequirement = src.SectionRequirement;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}