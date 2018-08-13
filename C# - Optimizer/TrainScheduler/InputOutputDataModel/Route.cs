using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class Route : Serializable
    {
        public Route()
        {
            RoutePaths = new List<RoutePath>();
        }

        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [DataMember(Name = "route_paths")]
        public List<RoutePath> RoutePaths { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is Route src)
            {
                Id = src.Id;
                RoutePaths = src.RoutePaths;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}