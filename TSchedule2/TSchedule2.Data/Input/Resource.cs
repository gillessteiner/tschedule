using System;
using System.Runtime.Serialization;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.Input {
   [DataContract]
   public class Resource {
      [DataMember(Name = "id", Order = 1)]
      public string Id { get; private set; }

      [IgnoreDataMember]
      public TimeSpan ReleaseTime { get; private set; }

      [DataMember(Name = "release_time", Order = 2)]
      private string ReleaseTimeStr {
         get => ReleaseTime.DurationToString();
         set => ReleaseTime = value.StringToDuration();
      }

      [DataMember(Name = "following_allowed", Order = 3)]
      public bool FollowingAllowed { get; private set; }
   }
}