using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSchedule2.Data.Model
{
   public class PossiblePath
   {
      public PossiblePath(string nodeId) {
         LastNodeId = nodeId;
      }
      public PossiblePath(PossiblePath basePath, Section edge) {
         _sections.AddRange(basePath._sections);
         Penalty = basePath.Penalty;
         Add(edge);
      }

      public void Add(Section edge) {
         _sections.Add(edge);
         LastNodeId = edge.ExitMarker;
         Penalty += edge.Penalty;
      }

      private readonly List<Section> _sections = new List<Section>();
      public IEnumerable<Section> Sections => _sections;
      public double Penalty { get; private set; } = 0.0;
      public string LastNodeId { get; set; }
   }
}
