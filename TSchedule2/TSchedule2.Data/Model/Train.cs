using System;
using System.Collections.Generic;
using System.Linq;
using TSchedule2.Data.SBB;

namespace TSchedule2.Data.Model
{
   public class Train
   {
      public Train(string id, Track track, IEnumerable<SectionRequirement> requirements) {
         Id = id;
         Track = track;
         Requirements = requirements.ToDictionary(r => r.SectionMarker);

         // Make sure default time values are inforced
         foreach (var req in Requirements.Values) {
            if (req.EntryLatest == DateTime.MinValue) req.EntryLatest = DateTime.MaxValue;
            if (req.ExitLatest == DateTime.MinValue)  req.ExitLatest = DateTime.MaxValue;
         }
      }
      
      public string Id { get; private set; }
      public Track Track { get; private set; }

      private Dictionary<string, SectionRequirement> Requirements { get; }

      public SectionRequirement GetRequirement(string sectionMarker) {
         return (sectionMarker != null && Requirements.ContainsKey(sectionMarker)) ? Requirements[sectionMarker] : new SectionRequirement();
      }

      public IEnumerable<string> RequirementKeys => Requirements?.Keys;

      public IEnumerable<Section> GetRandomPath() {
         return Track.GetPath(Data.Utils.Math.RndGenerator.Next(Track.NbPaths)).Sections;
      }
   }
}
 