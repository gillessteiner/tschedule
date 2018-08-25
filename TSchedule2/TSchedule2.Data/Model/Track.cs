using System.Collections.Generic;
using System.Linq;
using Data.SBB;
using Microsoft.Msagl.Drawing;

namespace Data.Model {
   public class Track : Graph // A track can be visualized as a MSAGL graph
   {
      public Track(Route route) {
         Attr.LayerDirection = LayerDirection.LR;

         foreach (var path in route.RoutePaths) {
            for (var index = 0; index < path.RouteSections.Length; ++index) {
               AddSection(index + 1, route.Id, path.Id, path.RouteSections[index]);
            }
         }

         FindPossiblePaths();
      }

      private readonly Dictionary<string, Section> _sections = new Dictionary<string, Section>();
      private readonly Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
      private IEnumerable<Node> LeftNodes => _nodes.Values.Where(n => !n.InputEdgeIds.Any());

      public Section GetSection(string id) {
         return _sections.ContainsKey(id) ? _sections[id] : null;
      }

      private PossiblePath[] _paths;
      public int NbPaths => _paths?.Length ?? 0;

      public PossiblePath GetPath(int index) {
         return (index >= 0 && index < NbPaths) ? _paths[index] : null;
      }
      
      public void SelectPath(int i) {
         foreach (var edge in Edges)
            edge.Attr.Color = Color.Black;

         if (i >= 0 && i < NbPaths) {
            Highlight(_paths[i].Sections);
         }
      }

      public void Highlight(IEnumerable<Section> sections) {
         foreach (var edge in Edges)
            edge.Attr.Color = Color.Black;

         foreach (var edge in sections) {
            EdgeById(edge.Key).Attr.Color = Color.Red;
         }
      }

      public void AdjustLabels(IEnumerable<Section> sections) {
         foreach (var edge in Edges)
            edge.LabelText = _sections[edge.Attr.Id].SectionMarker;
         foreach (var edge in sections) {
            EdgeById(edge.Key).LabelText = $"{edge.EntryTimeStr} - {edge.ExitTimeStr}";
         }
      }

      private void AddSection(int index, string routeId, string pathId, RouteSection routeSection) {
         var key = Section.GetKey(routeId, routeSection.SequenceNumber);
         _sections.Add(key, new Section(routeId, pathId, index, routeSection));
         var section = _sections[key];
         var edge = this.AddEdge(section.EntryMarker, section.SectionMarker, section.ExitMarker);
         edge.Attr.Id = key;

         if (!_nodes.ContainsKey(section.EntryMarker)) {
            _nodes.Add(section.EntryMarker, new Node(section.EntryMarker));
         }

         if (!_nodes.ContainsKey(section.ExitMarker)) {
            _nodes.Add(section.ExitMarker, new Node(section.ExitMarker));
         }

         _nodes[section.EntryMarker].OutputEdgeIds.Add(key);
         _nodes[section.ExitMarker].InputEdgeIds.Add(key);
      }

      /// <summary>
      /// Given a graph, find the list of possible paths for a train
      /// </summary>
      private void FindPossiblePaths() {
         var activePaths = LeftNodes.Select(n => new PossiblePath(n.Marker)).ToList();
         var newPath = new List<PossiblePath>();
         var allPathsFinished = true;
         do {
            allPathsFinished = true;
            activePaths.AddRange(newPath);
            newPath.Clear();
            foreach (var path in activePaths) {
               // Last node
               var lastNode = _nodes[path.LastNodeId];
               if (lastNode.OutputEdgeIds.Any()) {
                  allPathsFinished = false;
                  for (var n = 1; n < lastNode.OutputEdgeIds.Count; ++n) {
                     newPath.Add(new PossiblePath(path, _sections[lastNode.OutputEdgeIds[n]]));
                  }

                  // Continue first path
                  path.Add(_sections[lastNode.OutputEdgeIds[0]]);
               }
            }
         } while (newPath.Any() || !allPathsFinished);

         _paths = activePaths.OrderBy(p => p.Penalty).ToArray();
      }
   }
}