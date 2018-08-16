using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrainScheduler.Properties;

namespace TrainScheduler.Data
{
    public class SectionEdge
    {
        public SectionEdge(string routePath, RouteSection section, EventNode entry, EventNode exit)
        {
            RoutePath = routePath;
            Entry = entry;
            Exit = exit;
            RouteSection = section;

            Entry.AttachEdge(this, EventNode.EdgeType.Output);
            Exit.AttachEdge(this, EventNode.EdgeType.Input);
        }

        public string RoutePath { get; private set; }

        public EventNode Entry { get; private set; }
        public EventNode Exit { get; private set; }

        private RouteSection RouteSection { get; set; }

        // Fwd some properties of the section (read-only)
        public int SequenceNumber => RouteSection?.SequenceNumber ?? 0;
        public double? Penalty => RouteSection?.Penalty;
        public TimeSpan MinimumRunningTime => RouteSection?.MinimumRunningTime ?? TimeSpan.Zero;
        public int NbResourcesOccupied => RouteSection?.OccupiedRessources?.Length ?? 0;
        public string[] ResourceOccupations => RouteSection?.OccupiedRessources;
        
        public string SectionMarker => (RouteSection?.SectionMarkers != null && RouteSection.SectionMarkers.Any())
            ? RouteSection.SectionMarkers[0]
            : null;

    }
}
