using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TSchedule2.Data.SBB;

namespace TSchedule2.Data.Model
{
   public class Problem
   {
      public Problem(ProblemInstance problemInstance) {
         Hash = problemInstance.Hash;
         Label = problemInstance.Label;

         _tracks = problemInstance.Routes.ToDictionary(r => r.Id, r => new Track(r));
         _resources = problemInstance.Resources.ToDictionary(r => r.Id, r => new Resource(r.ReleaseTime));
         _trains = problemInstance.ServiceIntentions.ToDictionary(s => s.Id, s => new Train(s.Id, _tracks[s.Route], s.SectionRequirements));
      }

      #region private members
      public int Hash { get; private set; }
      public string Label { get; private set; }

      private readonly Dictionary<string, Track> _tracks;
      private readonly Dictionary<string, Resource> _resources;
      private readonly Dictionary<string, Train> _trains;
      #endregion

      public int NbRoutes => _tracks?.Count ?? 0;
      public int NbResources => _resources?.Count ?? 0;
      public int NbTrains => _trains?.Count ?? 0;

      public IEnumerable<string> RouteKeys => _tracks?.Keys;
      public IEnumerable<string> TrainKeys => _trains?.Keys;

      public Track GetTrack(string id) {
         return (id != null && _tracks.ContainsKey(id)) ? _tracks[id] : null;
      }
      public Resource GetResource(string id) {
         return (id != null && _resources.ContainsKey(id)) ? _resources[id] : null;
      }
      public Train GetTrain(string id) {
         return (id != null && _trains.ContainsKey(id)) ? _trains[id] : null;
      }
   }
}
