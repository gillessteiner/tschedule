using System;

namespace Data.Model
{
   public class Resource
   {
      public Resource(string id, TimeSpan releaseTime) {
         Id = id;
         ReleaseTime = releaseTime;
      }

      public string Id { get; private set; }
      public TimeSpan ReleaseTime { get; private set; }
   }
}
