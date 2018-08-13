using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheduler.Data
{
    public class EventNode
    {
        public EventNode(string marker)
        {
            Marker = marker;
        }

        public string Marker { get; private set; }

        public Dictionary<int, SectionEdge> InputEdges  = new Dictionary<int, SectionEdge>();
        public Dictionary<int, SectionEdge> OutputEdges = new Dictionary<int, SectionEdge>();

        public enum EdgeType { Input, Output }

        public void AttachEdge(SectionEdge edge, EdgeType type)
        {
            if (type == EdgeType.Input)
            {
                InputEdges.Add(edge.SequenceNumber, edge);
            }
            else
            {
                OutputEdges.Add(edge.SequenceNumber, edge);
            }
        }
    }
}
