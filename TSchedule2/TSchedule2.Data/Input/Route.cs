using System.Runtime.Serialization;

namespace TSchedule2.Data.Input {
   [DataContract]
   public class Route {
      #region SBB Data Model

      [DataMember(Name = "id")]
      public string Id { get; private set; }

      [DataMember(Name = "route_paths")]
      public RoutePath[] RoutePaths { get; private set; }

      #endregion
   }
}