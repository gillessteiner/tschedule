using System;

namespace TSchedule2.Data.Model
{
   public class Resource
   {
      public Resource(TimeSpan releaseTime) {
         ReleaseTime = releaseTime;
      }

      internal TimeSpan ReleaseTime { get; private set; } = TimeSpan.Zero;
   }
}
