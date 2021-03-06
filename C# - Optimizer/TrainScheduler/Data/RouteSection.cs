﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class RouteSection : DeSerializable
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

        [IgnoreDataMember]
        public string AlternativeMarkerAtEntry =>
            (RouteAlternativeMarkersAtEntry != null && RouteAlternativeMarkersAtEntry.Any())
                ? RouteAlternativeMarkersAtEntry[0]
                : null;

        [DataMember(Name = "route_alternative_marker_at_entry", Order = 3)]
        public List<string> RouteAlternativeMarkersAtEntry { get; private set; }

        [IgnoreDataMember]
        public string AlternativeMarkerAtExit =>
            (RouteAlternativeMarkersAtExit != null && RouteAlternativeMarkersAtExit.Any())
                ? RouteAlternativeMarkersAtExit[0]
                : null;

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

        [IgnoreDataMember] private List<ResourceOccupation> _resourceOccupations;

        [DataMember(Name = "resource_occupations", Order = 8)]
        private List<ResourceOccupation> ResourceOccupations
        {
            get => _resourceOccupations;
            set
            {
                _resourceOccupations = value;
                OccupiedRessources = _resourceOccupations.Select(r => r.Resource).ToArray();
            }
        }

        // Save as resourceoccupation but simply the id
        [IgnoreDataMember]
        public string[] OccupiedRessources { get; private set; }

        [DataMember(Name = "section_marker", Order = 9)]
        public List<string> SectionMarkers { get; private set; }

       [IgnoreDataMember]
       public string SectionMarker => SectionMarkers?.FirstOrDefault();

       [IgnoreDataMember]
       public IEnumerable<string> ResourceIds => ResourceOccupations?.Select(ro => ro.Resource);

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