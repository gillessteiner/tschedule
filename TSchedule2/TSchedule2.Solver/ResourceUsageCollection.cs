using System;
using System.Collections.Generic;
using System.Linq;
using Math = Utils.Math;

namespace Solver
{
   public class ResourceUsageCollection
   {
      private readonly Dictionary<string, Dictionary<string, Math.Period>> _container = new Dictionary<string, Dictionary<string, Math.Period>>();

      public IEnumerable<Math.Period> UsageByOtherThan(string resourceId, string trainId) {
         return _container.ContainsKey(resourceId) ? _container[resourceId].Where(kvp => kvp.Key != trainId).Select(kvp => kvp.Value) : Enumerable.Empty<Math.Period>();
      }

      public void Add(string resourceId, string trainId, DateTime start, DateTime end) {

         if (!_container.ContainsKey(resourceId)) {
            _container.Add(resourceId, new Dictionary<string, Math.Period>() { { trainId, new Math.Period(start, end) }});
         }
         else {
            if (!_container[resourceId].ContainsKey(trainId)) {
               _container[resourceId].Add(trainId, new Math.Period(start, end));
            }
            else {
               // Extend occupation period
               // Here we assume that a given train occupies a single resource in a continuous way
               _container[resourceId][trainId].Start = Math.Min(_container[resourceId][trainId].Start, start);
               _container[resourceId][trainId].End = Math.Max(_container[resourceId][trainId].End, end);
            }
         }
      }
   }
}
