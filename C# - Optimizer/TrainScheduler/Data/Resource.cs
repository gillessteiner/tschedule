using System;
using System.Runtime.Serialization;

namespace TrainScheduler.Data
{
    [DataContract]
    public class Resource : DeSerializable
    {
        [DataMember(Name="id", Order=1)]
        public string Id { get; private set; }

        [IgnoreDataMember]
        public TimeSpan ReleaseTime { get; private set; }

        [DataMember(Name = "release_time", Order = 2)]
        private string ReleaseTimeStr
        {
            get => IsoDuration.ToString(ReleaseTime);
            set => ReleaseTime = IsoDuration.FromString(value);
        }

        [DataMember(Name = "following_allowed", Order = 3)]
        public bool FollowingAllowed { get; private set; }

        protected override void CopyFrom(object other)
        {
            if (other is Resource src)
            {
                Id = src.Id;
                ReleaseTime = src.ReleaseTime;
                FollowingAllowed = src.FollowingAllowed;
            }
            else
            {
                throw new InvalidCastException($"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
            }
        }
    }
}