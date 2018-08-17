using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainScheduler.Data {
   [DataContract]
   public class ServiceIntention : DeSerializable {
      public ServiceIntention() {
         SectionRequirements = new List<SectionRequirement>();
      }

      [DataMember(Name = "id")] public string Id { get; private set; }

      [DataMember(Name = "route")] public string Route { get; private set; }

      [IgnoreDataMember] private List<SectionRequirement> _sectionRequirements;

      [DataMember(Name = "section_requirements")]
      public List<SectionRequirement> SectionRequirements {
         get => _sectionRequirements;
         private set {
            _sectionRequirements = value;
            SectionRequirementsDic = _sectionRequirements.ToDictionary(sr => sr.SectionMarker);
            MaxDelayPenalty =
               _sectionRequirements.Max(sr => Math.Max(sr.EntryDelayWeight ?? 0, sr.ExitDelayWeight ?? 0));
            MinEntryEarliest = _sectionRequirements.Where(sr => sr.EntryEarliest.HasValue)
               .Min(sr => sr.EntryEarliest.Value);
         }
      }

      [IgnoreDataMember] public DateTime MinEntryEarliest { get; private set; } = DateTime.Today;

      [IgnoreDataMember] public double MaxDelayPenalty { get; private set; }

      [IgnoreDataMember] public Dictionary<string, SectionRequirement> SectionRequirementsDic { get; private set; }

      public (DateTime minEntryTime, TimeSpan minStoppingTime, DateTime minExitTime) GetRequirement(string sectionMarker) {
         var minEntryTime = DateTime.Today;
         var minExitTime = DateTime.Today;
         var minStoppingTime = TimeSpan.Zero;

         if (sectionMarker != null &&
             this.SectionRequirementsDic.ContainsKey(sectionMarker)) {
            if (this.SectionRequirementsDic[sectionMarker].EntryEarliest.HasValue) {
               minEntryTime = this.SectionRequirementsDic[sectionMarker].EntryEarliest.Value;
            }

            if (this.SectionRequirementsDic[sectionMarker].ExitEarliest.HasValue) {
               minExitTime = this.SectionRequirementsDic[sectionMarker].ExitEarliest.Value;
            }

            minStoppingTime = this.SectionRequirementsDic[sectionMarker].MinStoppingTime;
         }

         return (minEntryTime, minStoppingTime, minExitTime);
      }

      public (DateTime maxEntryTime, double entryDelayWeight, DateTime maxExitTime, double exitDelayWeight) GetLatestTime(string sectionMarker) {
         var maxEntryTime = DateTime.MaxValue;
         var maxExitTime = DateTime.MaxValue;
         double entryDelayWeight = 0.0;
         double exitDelayWeight = 0.0;

         if (sectionMarker != null &&
             this.SectionRequirementsDic.ContainsKey(sectionMarker)) {
            if (this.SectionRequirementsDic[sectionMarker].EntryLatest.HasValue) {
               maxEntryTime = this.SectionRequirementsDic[sectionMarker].EntryLatest.Value;
            }

            if (this.SectionRequirementsDic[sectionMarker].EntryDelayWeight.HasValue)
               entryDelayWeight = this.SectionRequirementsDic[sectionMarker].EntryDelayWeight.Value;


            if (this.SectionRequirementsDic[sectionMarker].ExitLatest.HasValue) {
               maxExitTime = this.SectionRequirementsDic[sectionMarker].ExitLatest.Value;
            }

            if (this.SectionRequirementsDic[sectionMarker].ExitDelayWeight.HasValue)
               exitDelayWeight = this.SectionRequirementsDic[sectionMarker].ExitDelayWeight.Value;

         }

         return (maxEntryTime, entryDelayWeight, maxExitTime, exitDelayWeight);
      }

      protected override void CopyFrom(object other) {
         if (other is ServiceIntention src) {
            Id = src.Id;
            Route = src.Route;
            SectionRequirements = src.SectionRequirements;
         }
         else {
            throw new InvalidCastException(
               $"Cannot copy from type {other.GetType().ToString()} to {this.GetType().ToString()}");
         }
      }
   }
}