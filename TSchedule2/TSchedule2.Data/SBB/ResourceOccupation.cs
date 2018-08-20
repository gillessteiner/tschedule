using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB
{
   [DataContract]
   public class ResourceOccupation
   {
      [DataMember(Name = "resource")]
      public string ResourceId { get; private set; }
   }
}
