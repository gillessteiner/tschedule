using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB {
   [DataContract]
   public class Route {
      #region SBB Data Model

      [DataMember(Name = "id")]
      internal string Id { get; private set; }

      [DataMember(Name = "route_paths")]
      internal RoutePath[] RoutePaths { get; private set; }

      #endregion
   }
}