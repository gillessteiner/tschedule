using System.Collections.Generic;

namespace TSchedule2.Data.Model {
   internal class Node {
      internal Node(string marker) {
         Marker = marker;
         InputEdgeIds = new List<string>();
         OutputEdgeIds = new List<string>();
      }

      public string Marker { get; set; }

      internal List<string> InputEdgeIds { get; private set; }
      internal List<string> OutputEdgeIds { get; private set; }
   }
}
