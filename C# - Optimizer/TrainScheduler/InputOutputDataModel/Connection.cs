using System;
using System.Runtime.Serialization;

namespace TrainScheduler.InputOutputDataModel
{
    [DataContract]
    public class Connection : Serializable
    {
        [DataMember(Name = "id", Order = 1)]
        public string Id { get; private set; }

        [DataMember(Name = "onto_service_intention", Order = 2)]
        public string OntoServiceIntention { get; private set; }

        [DataMember(Name = "onto_section_marker", Order = 3)]
        public string OntoSectionMarker { get; private set; }

        [IgnoreDataMember]
        public TimeSpan MinConnectionTime { get; private set; }

        [DataMember(Name = "min_connection_time", Order = 4)]
        private string MinConnectionTimeStr
        {
            get => IsoDuration.ToString(MinConnectionTime);
            set => MinConnectionTime = IsoDuration.FromString(value);
        }

        protected override void CopyFrom(object other)
        {
            if (other is Connection src)
            {
                Id = src.Id;
                OntoServiceIntention = src.OntoServiceIntention;
                OntoSectionMarker = src.OntoSectionMarker;
                MinConnectionTime = src.MinConnectionTime;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}