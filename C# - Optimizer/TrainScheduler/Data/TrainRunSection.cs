using System;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class TrainRunSection : Serializable
    {
        public TrainRunSection(Route route, ServiceIntention train, SectionEdge section )
        {
            RouteId = route.Id;
            RoutePathId = section.RoutePath;
            SequenceNumber = section.SequenceNumber;
        }
       
        [DataMember(Name="route", Order=3)]
        public string RouteId { get; private set; }

        [DataMember(Name = "route_section_id", Order = 4)]
        public string RouteSectionId => $"{RouteId}#{SequenceNumber}";

        [DataMember(Name = "sequence_number", Order = 5)]
        public int SequenceNumber { get; private set; }

        [DataMember(Name = "route_path", Order = 6)]
        public string RoutePathId { get; private set; }

        [DataMember(Name = "section_requirement", Order = 7)]
        public string SectionRequirement { get; private set; }

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
    }
}