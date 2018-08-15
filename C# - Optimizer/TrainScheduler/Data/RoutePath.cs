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

        [DataMember(Name = "route_sections")]
        public List<RouteSection> RouteSections { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is RoutePath src)
            {
                Id = src.Id;
                RouteSections = src.RouteSections;
                OrganizeInDictionaries();
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }

        private void OrganizeInDictionaries()
        {
            RouteSectionDic = RouteSections.ToDictionary(p => p.SequenceNumber, p => p);
        }

        [IgnoreDataMember]
        public Dictionary<int, RouteSection> RouteSectionDic { get; private set; }
    }
}