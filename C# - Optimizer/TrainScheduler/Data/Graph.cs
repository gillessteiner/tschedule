using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.InputOutputDataModel;

namespace TrainScheduler.Data
{
    public class Graph
    {
        public string RouteId { get; private set; }

        private readonly Dictionary<string, EventNode> _nodes = new Dictionary<string, EventNode>();
        private readonly List<SectionEdge> _edges = new List<SectionEdge>();

        private IEnumerable<EventNode> LeftNodes => _nodes.Values.Where(n => !n.InputEdges.Any());

        /// <summary>
        /// Convert a route as in the input datamodel into a graph
        /// </summary>
        /// <param name="route"></param>
        public Graph(Route route)
        {
            RouteId = route.Id;
            foreach (var path in route.RoutePaths)
            {
                foreach (var section in path.RouteSections)
                {
                    // Create entry/exit nodes/events if needed
                    var (entryId, exitId) = CreateEntryExitNodes(section);

                    // Create edge
                    _edges.Add(new SectionEdge(path.Id, section, _nodes[entryId], _nodes[exitId]));
                }
            }
        }

        private (string entryId, string exitId) CreateEntryExitNodes(RouteSection section)
        {
            var entryId = "event_" + _nodes.Count.ToString();
            if (section.RouteAlternativeMarkersAtEntry.Any()) // Either 0 or 1
            {
                if (!_nodes.ContainsKey(section.RouteAlternativeMarkersAtEntry[0]))
                    _nodes.Add(section.RouteAlternativeMarkersAtEntry[0], new EventNode(section.RouteAlternativeMarkersAtEntry[0]));

                entryId = section.RouteAlternativeMarkersAtEntry[0];
            }
            else
            {
                _nodes.Add(entryId, new EventNode(entryId));
            }

            var exitId = "event_" + _nodes.Count.ToString();
            if (section.RouteAlternativeMarkersAtExit.Any()) // Either 0 or 1
            {
                if (!_nodes.ContainsKey(section.RouteAlternativeMarkersAtExit[0]))
                    _nodes.Add(section.RouteAlternativeMarkersAtExit[0], new EventNode(section.RouteAlternativeMarkersAtExit[0]));

                exitId = section.RouteAlternativeMarkersAtExit[0];
            }
            else
            {
                _nodes.Add(exitId, new EventNode(exitId));
            }

            return (entryId, exitId);
        }
    }
}
