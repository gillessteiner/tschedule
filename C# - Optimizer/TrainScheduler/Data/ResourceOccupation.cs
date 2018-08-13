using System;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class ResourceOccupation : Serializable
    {
        public ResourceOccupation()
        {
            
        }

        [DataMember(Name="resource")]
        public string Resource { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is ResourceOccupation src)
            {
                Resource = src.Resource;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}