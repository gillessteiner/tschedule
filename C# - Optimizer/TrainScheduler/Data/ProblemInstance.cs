using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class ProblemInstance : DeSerializable
    {
        public ProblemInstance()
        {
            ServiceIntentions = new List<ServiceIntention>();
            Routes = new List<Route>();
            Resources = new List<Resource>();
        }

        public ProblemInstance(string label, int hash) : this()
        {
            Label = label;
            Hash = hash;
        }

        #region SBB Data Model
        [DataMember(Name = "label", Order = 1)]
        public string Label { get; private set; }

        [DataMember(Name = "hash", Order = 2)]
        public int Hash { get; private set; }

        [DataMember(Name = "service_intentions", Order = 3)]
        public List<ServiceIntention> ServiceIntentions { get; private set; }

        [DataMember(Name = "routes", Order = 4)]
        public List<Route> Routes { get; private set; }

        [DataMember(Name = "resources", Order = 5)]
        public List<Resource> Resources { get; private set; }
        #endregion

        protected override void CopyFrom(object other)
        {
            if (other is ProblemInstance src)
            {
                Label = src.Label;
                Hash = src.Hash;
                ServiceIntentions = src.ServiceIntentions;
                Routes = src.Routes;
                Resources = src.Resources;

                OrganizeInDictionaries();
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }

        private void OrganizeInDictionaries()
        {
            RouteDic = Routes.ToDictionary(r => r.Id, r => r);
        }

        [IgnoreDataMember]
        public Dictionary<string, Route> RouteDic { get; private set; }

        public void CreateRouteGraph()
        {
            foreach (var route in RouteDic.Values)
            {
                route.CreateGraph();
            }
        }
    }
}
