using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class RoutePath : Serializable
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
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}