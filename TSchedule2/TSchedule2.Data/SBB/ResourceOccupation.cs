using System.Runtime.Serialization;

namespace Data.SBB
{
   [DataContract]
   internal class ResourceOccupation
   {
      [DataMember(Name = "resource")]
      internal string ResourceId { get; private set; }
   }
}
