using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Msagl.Drawing;

namespace TrainScheduler.Data
{
    [DataContract]
    public class TrainRun : Serializable
    {
        public TrainRun(ServiceIntention train, Route route)
        {
            Train = train;
            Route = route;
        }

        [IgnoreDataMember]
        private ServiceIntention Train { get; set; }

        [IgnoreDataMember]
        private Route Route { get; set; }
        
        [DataMember(Name = "service_intention_id", Order = 1)]
        public string ServiceIntentionId => Train.Id;

        [DataMember(Name = "train_run_sections", Order = 2)]
        public List<TrainRunSection> TrainRunSections { get; private set; }

        public bool IsPathSelected => _selectedPathIndex < 0;

        [IgnoreDataMember]
        private int _selectedPathIndex = -1;

        public void SelectNextPath()
        {
            if (++_selectedPathIndex < Route.Graph.PossiblePathsOrderedByPenalty.Length)
            {
                TrainRunSections = Route.Graph.PossiblePathsOrderedByPenalty[_selectedPathIndex].Sections.Select(sec => new TrainRunSection(Route, Train, sec)).ToList();
            }
        }

        private Microsoft.Msagl.Drawing.Graph _msGraph;
        public Microsoft.Msagl.Drawing.Graph MSGraph => _msGraph;
        public void CreateGraph()
        {
            _msGraph = new Microsoft.Msagl.Drawing.Graph($"solution_graph")
            {
                Attr = { LayerDirection = LayerDirection.LR }
            };

            /*
            foreach (var e in _edges)
            {
                var edge = _msGraph.AddEdge(e.Entry.Marker, e.SectionMarker, e.Exit.Marker);
                edge.Attr.Id = e.SequenceNumber.ToString();
            }
            */
        }
    }
}