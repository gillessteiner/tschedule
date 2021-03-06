﻿using System.Runtime.Serialization;

namespace Data.SBB {
   [DataContract]
   internal class RoutePath {
      [DataMember(Name = "id")]
      internal string Id { get; private set; }

      [DataMember(Name = "route_sections")]
      internal RouteSection[] RouteSections { get; private set; }
   }
}