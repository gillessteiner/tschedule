using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data {
   [DataContract]
   public class ProblemInstance : DeSerializable {
      public ProblemInstance() {
         ServiceIntentions = new List<ServiceIntention>();
         Routes = new List<Route>();
         Resources = new List<Resource>();
      }

      public ProblemInstance(string label, int hash) : this() {
         Label = label;
         Hash = hash;
      }

      #region SBB Data Model

      [DataMember(Name = "label", Order = 1)]
      public string Label { get; private set; }

      [DataMember(Name = "hash", Order = 2)]
      public int Hash { get; private set; }

      [IgnoreDataMember] private List<ServiceIntention> _serviceIntentions;

      [DataMember(Name = "service_intentions", Order = 3)]
      public List<ServiceIntention> ServiceIntentions {
         get => _serviceIntentions;
         private set {
            _serviceIntentions = value;
            ServiceIntentionsDic = _serviceIntentions.ToDictionary(t => t.Id, t => t);
         }
      }

      [IgnoreDataMember]
      public Dictionary<string, ServiceIntention> ServiceIntentionsDic { get; private set; }

      [IgnoreDataMember] private List<Route> _routes;

      [DataMember(Name = "routes", Order = 4)]
      public List<Route> Routes {
         get => _routes;
         private set {
            _routes = value;
            RouteDic = _routes.ToDictionary(r => r.Id, r => r);

            RouteSectionDic = new Dictionary<string, RouteSection>();
            foreach (var r in _routes) {
               foreach (var p in r.RoutePaths) {
                  foreach (var section in p.RouteSections) {
                     RouteSectionDic.Add($"{r.Id}#{section.SequenceNumber}", section);
                  }
               }
            }
         }
      }

      [IgnoreDataMember]
      public Dictionary<string, Route> RouteDic { get; private set; }

      [IgnoreDataMember]
      private Dictionary<string, RouteSection> RouteSectionDic { get; set; }

      [IgnoreDataMember] private List<Resource> _resources;

      [DataMember(Name = "resources", Order = 5)]
      public List<Resource> Resources {
         get => _resources;
         private set {
            _resources = value;
            ResourcesDic = _resources.ToDictionary(r => r.Id);
         }
      }

      [IgnoreDataMember]
      private Dictionary<string, Resource> ResourcesDic { get; set; }

      #endregion

      protected override void CopyFrom(object other) {
         if (other is ProblemInstance src) {
            Label = src.Label;
            Hash = src.Hash;
            ServiceIntentions = src.ServiceIntentions;
            Routes = src.Routes;
            Resources = src.Resources;
         }
         else {
            throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
         }
      }

      public Resource TryGetResource(string id) {
         return ResourcesDic.ContainsKey(id) ? ResourcesDic[id] : null;
      }

      public RouteSection TryGetRouteSection(string id) {
         return RouteSectionDic.ContainsKey(id) ? RouteSectionDic[id] : null;
      }
   }
}