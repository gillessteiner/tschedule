using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class RouteSection : Serializable
    {
        public RouteSection()
        {
            ResourceOccupations = new List<ResourceOccupation>();
            RouteAlternativeMarkersAtEntry = new List<string>();
            RouteAlternativeMarkersAtExit = new List<string>();
            SectionMarkers = new List<string>();
        }

        [DataMember(Name = "sequence_number", Order = 1)]
        public int SequenceNumber { get; private set; }

        [DataMember(Name = "penalty", Order = 2)]
        public double? Penalty { get; private set; }

        [DataMember(Name = "route_alternative_marker_at_entry", Order = 3)]
        public List<string> RouteAlternativeMarkersAtEntry { get; private set; }

        [DataMember(Name = "route_alternative_marker_at_exit", Order = 4)]
        public List<string> RouteAlternativeMarkersAtExit { get; private set; }

        [DataMember(Name = "starting_point", Order = 5)]
        public string StartingPoint { get; private set; }

        [DataMember(Name = "ending_point", Order = 6)]
        public string EndingPoint { get; private set; }

        [IgnoreDataMember]
        public TimeSpan MinimumRunningTime { get; private set; }

        [DataMember(Name = "minimum_running_time", Order = 7)]
        private string MinimumRunningTimeStr
        {
            get => IsoDuration.ToString(MinimumRunningTime);
            set => MinimumRunningTime = IsoDuration.FromString(value);
        }

        [DataMember(Name = "resource_occupations", Order = 8)]
        public List<ResourceOccupation> ResourceOccupations { get; private set; }

        [DataMember(Name = "section_marker", Order = 9)]
        public List<string> SectionMarkers { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is RouteSection src)
            {
                SequenceNumber = src.SequenceNumber;
                Penalty = src.Penalty;
                RouteAlternativeMarkersAtEntry = src.RouteAlternativeMarkersAtEntry;
                RouteAlternativeMarkersAtExit = src.RouteAlternativeMarkersAtExit;
                StartingPoint = src.StartingPoint;
                EndingPoint = src.EndingPoint;
                MinimumRunningTime = src.MinimumRunningTime;
                ResourceOccupations = src.ResourceOccupations;
                SectionMarkers = src.SectionMarkers;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }

  
}