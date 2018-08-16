using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainScheduler.Utils
{
    public class UsedResourceCollection
    {
        private readonly Dictionary<string, Dictionary<string, Dictionary<int, Tuple<DateTime, DateTime>>>> _container =
                     new Dictionary<string, Dictionary<string, Dictionary<int, Tuple<DateTime, DateTime>>>>();

        public Dictionary<string, Dictionary<int, Tuple<DateTime, DateTime>>> this[string resId] => _container[resId];

        public void Add(string resId, string trainId, int sequenceNumber, DateTime entry, DateTime exit)
        {
            if (!_container.ContainsKey(resId))
            {
                _container.Add(resId, new Dictionary<string, Dictionary<int, Tuple<DateTime, DateTime>>>());
            }

            if (!_container[resId].ContainsKey(trainId))
            {
                _container[resId].Add(trainId, new Dictionary<int, Tuple<DateTime, DateTime>> ());
            }

            _container[resId][trainId][sequenceNumber] = new Tuple<DateTime, DateTime>(entry, exit);
        }

        public IEnumerable<Tuple<string, int, DateTime, DateTime>> UsageByOtherThan(string resId, string trainId)
        {
            if (!_container.ContainsKey(resId))
            {
                yield break;
            }

            foreach (var trainDic in _container[resId].Where(kvp => kvp.Key != trainId))
            {
                foreach (var sectionDic in trainDic.Value)
                {
                    yield return new Tuple<string, int, DateTime, DateTime>(trainDic.Key, sectionDic.Key, sectionDic.Value.Item1, sectionDic.Value.Item2);
                }
            }
        }

        public Tuple<string, int, DateTime, DateTime> TryAdd(string resId, string trainId, int sequenceNumber, DateTime entry, DateTime exit)
        {
            if (!_container.ContainsKey(resId)) {
                _container.Add(resId, new Dictionary<string, Dictionary<int, Tuple<DateTime, DateTime>>>());
            }

            if (!_container[resId].ContainsKey(trainId)) {
                _container[resId].Add(trainId, new Dictionary<int, Tuple<DateTime, DateTime>>());
            }
            
            // Look for conflicts
            foreach (var collection in _container[resId].Where(kvp => kvp.Key != trainId))
            {
                foreach (var keyVal in collection.Value)
                {
                    if(Math.Intersect(entry, exit, keyVal.Value.Item1, keyVal.Value.Item2))
                    {
                        return new Tuple<string, int, DateTime, DateTime>(collection.Key, keyVal.Key, keyVal.Value.Item1, keyVal.Value.Item2);// Means this period intersect an existing one
                    }
                }
            }

            _container[resId][trainId][sequenceNumber] = new Tuple<DateTime, DateTime>(entry, exit);
            return null;
        }

        public void RemoveTrain(string trainId)
        {
            foreach (var kv in _container.Values)
            {
                if (kv.ContainsKey(trainId))
                {
                    kv.Remove(trainId);
                }
            }
        }
    }
}
 