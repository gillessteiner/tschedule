using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class Route : DeSerializable
    {
        public Route()
        {
            RoutePaths = new List<RoutePath>();
        }

        #region SBB Data Model

        [DataMember(Name = "id")] public string Id { get; private set; }

        [IgnoreDataMember] private List<RoutePath> _routePaths;

        [DataMember(Name = "route_paths")]
        public List<RoutePath> RoutePaths
        {
            get => _routePaths;
            private set
            {
                _routePaths = value;
                RoutePathsDic = _routePaths.ToDictionary(r => r.Id);
            }
        }

        [IgnoreDataMember] public Dictionary<string, RoutePath> RoutePathsDic { get; private set; }

        #endregion

        protected override void CopyFrom(object other)
        {
            if (other is Route src)
            {
                Id = src.Id;
                RoutePaths = src.RoutePaths;
            }
            else
            {
                throw new InvalidCastException(
                    $"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }

        [IgnoreDataMember] public Graph Graph { get; private set; }

        public void CreateGraph()
        {
            Graph = new Graph(this);
        }
    }
}