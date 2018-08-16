using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheduler.Data
{
    public class PossiblePath
    {
        public PossiblePath(EventNode node)
        {
            lastNode = node;
        }

        public PossiblePath(PossiblePath basePath, SectionEdge edge)
        {
            Sections.AddRange(basePath.Sections);
            Add(edge);
        }

        public void Add(SectionEdge edge)
        {
            Sections.Add(edge);
            lastNode = edge.Exit;
            if (edge.SectionMarker != null)
                SectionMarkers.Add(edge.SectionMarker);

            if (edge.Penalty.HasValue)
                Penalty += edge.Penalty.Value;

            foreach(var res in edge.ResourceOccupations)
                 _resourceOccupied.Add(res);
        }

        public HashSet<string> SectionMarkers { get; private set; } = new HashSet<string>();

        // List of sections (index of edges in collection)
        public List<SectionEdge> Sections { get; private set; } = new List<SectionEdge>();

        public EventNode lastNode { get; private set; }

        public double Penalty { get; private set; } = 0.0;

        private readonly HashSet<string> _resourceOccupied = new HashSet<string>();
        public int NbResourcesOccupied => _resourceOccupied.Count;
        public bool Visited { get; set; } = false;

        public int HowMany(IEnumerable<string> conflictingResources)
        {
            return conflictingResources.Count(res => _resourceOccupied.Contains(res));
        }
    }
}
