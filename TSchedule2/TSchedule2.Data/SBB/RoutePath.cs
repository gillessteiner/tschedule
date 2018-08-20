using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB {
   [DataContract]
   public class RoutePath {
      [DataMember(Name = "id")]
      public string Id { get; private set; }

      [DataMember(Name = "route_sections")]
      public RouteSection[] RouteSections { get; private set; }
   }
}