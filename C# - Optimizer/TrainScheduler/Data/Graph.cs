using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;

namespace TrainScheduler.Data
{
    public class Graph
    {
        public string RouteId { get; private set; }

        private readonly Dictionary<string, EventNode> _nodes = new Dictionary<string, EventNode>();
        private readonly List<SectionEdge> _edges = new List<SectionEdge>();

        private IEnumerable<EventNode> LeftNodes => _nodes.Values.Where(n => !n.InputEdges.Any());

        private Microsoft.Msagl.Drawing.Graph _msGraph;
        public Microsoft.Msagl.Drawing.Graph MSGraph => _msGraph;

        /// <summary>
        /// Convert a route as in the input datamodel into a graph
        /// </summary>
        /// <param name="route"></param>
        public Graph(Route route)
        {
            RouteId = route.Id;
            foreach (var path in route.RoutePaths)
            {
                // foreach (var section in path.RouteSections)
                for(int s=0; s < path.RouteSections.Count; ++s)
                {
                    var section = path.RouteSections[s];

                    // Create entry/exit nodes/events if needed
                    var (entryId, exitId) = CreateEntryExitNodes(section, path.Id, s+1);

                    // Create edge
                    _edges.Add(new SectionEdge(path.Id, section, _nodes[entryId], _nodes[exitId]));
                }
            }

            BuildMSGraph();

            FindPossiblePaths();
        }

        private (string entryId, string exitId) CreateEntryExitNodes(RouteSection section, string pathId, int ind)
        {
            var entryId = section.AlternativeMarkerAtEntry ?? $"{pathId}{ind-1}";
            if (!_nodes.ContainsKey(entryId))
                _nodes.Add(entryId, new EventNode(entryId));


            var exitId = section.AlternativeMarkerAtExit ?? $"{pathId}{ind}";
            if (!_nodes.ContainsKey(exitId))
                _nodes.Add(exitId, new EventNode(exitId));

            return (entryId, exitId);
        }

        private void BuildMSGraph()
        {
            _msGraph = new Microsoft.Msagl.Drawing.Graph($"route_{RouteId}")
            {
                Attr = {LayerDirection = LayerDirection.LR}
            };

            foreach (var e in _edges)
            {
                var edge = _msGraph.AddEdge(e.Entry.Marker, e.SectionMarker, e.Exit.Marker);
                edge.Attr.Id = e.SequenceNumber.ToString();
            }
        }

        public PossiblePath[] PossiblePathsOrderedByPenalty { get; private set; }

        /// <summary>
        /// Given a graph, find the list of possible paths for a train
        /// </summary>
        private void FindPossiblePaths()
        {
            var activePaths = LeftNodes.Select(n=> new PossiblePath(n)).ToList();
            var newPath = new List<PossiblePath>();
            var allPathsFinished = true;
            do
            {
                allPathsFinished = true;
                activePaths.AddRange(newPath);
                newPath.Clear();
                foreach (var path in activePaths)
                {
                    if (path.lastNode.OutputEdges.Any())
                    {
                        allPathsFinished = false;
                        
                        for (int n = 1; n < path.lastNode.OutputEdges.Count; ++n)
                        {
                            newPath.Add(new PossiblePath(path, path.lastNode.OutputEdges[n]));
                        }

                        // Continue first path
                        path.Add(path.lastNode.OutputEdges[0]);
                    }
                }

            } while (newPath.Any() || !allPathsFinished);

            PossiblePathsOrderedByPenalty = activePaths.OrderBy(p => p.Penalty).ThenBy(p => p.NbResourcesOccupied).ToArray();
        }
    }
}
