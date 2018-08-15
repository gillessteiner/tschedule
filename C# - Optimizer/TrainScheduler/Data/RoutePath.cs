using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class RoutePath : DeSerializable
    {
        public RoutePath()
        {
            RouteSections = new List<RouteSection>();
        }

        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [IgnoreDataMember] private List<RouteSection> _routeSections;

        [DataMember(Name = "route_sections")]
        public List<RouteSection> RouteSections
        {
            get => _routeSections;
            private set
            {
                _routeSections = value;
                RouteSectionDic = _routeSections.ToDictionary(s => s.SequenceNumber);
            }
        }

        [IgnoreDataMember]
        public Dictionary<int, RouteSection> RouteSectionDic { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is RoutePath src)
            {
                Id = src.Id;
                RouteSections = src.RouteSections;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }

    }
}