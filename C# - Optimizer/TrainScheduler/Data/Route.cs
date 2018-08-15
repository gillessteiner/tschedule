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
        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [DataMember(Name = "route_paths")]
        public List<RoutePath> RoutePaths { get; private set; }
        #endregion

        protected override void CopyFrom(object other)
        {
            if (other is Route src)
            {
                Id = src.Id;
                RoutePaths = src.RoutePaths;
                OrganizeInDictionaries();
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }

        [IgnoreDataMember]
        public Graph Graph { get; private set; }

        public void CreateGraph() { Graph = new Graph(this);}

        private void OrganizeInDictionaries()
        {
            RoutePathDic = RoutePaths.ToDictionary(p =>p.Id, p => p);
        }

        [IgnoreDataMember]
        public Dictionary<string, RoutePath> RoutePathDic { get; private set; }
    }
}