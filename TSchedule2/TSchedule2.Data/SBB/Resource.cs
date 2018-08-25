using System;
using System.Runtime.Serialization;
using Utils;

namespace Data.SBB {
   [DataContract]
   internal class Resource {
      [DataMember(Name = "id", Order = 1)]
      internal string Id { get; private set; }

      [IgnoreDataMember]
      internal TimeSpan ReleaseTime { get; private set; }

      [DataMember(Name = "release_time", Order = 2)]
      private string ReleaseTimeStr {
         get => ReleaseTime.DurationToString();
         set => ReleaseTime = value.StringToDuration();
      }

      [DataMember(Name = "following_allowed", Order = 3)]
      internal bool FollowingAllowed { get; private set; }
   }
}