using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Data.Model;

namespace Data.SBB {
   [DataContract]
   public class TrainRun {
      public TrainRun(Train train) {
         ServiceIntentionId = train.Id;
      }

      [IgnoreDataMember]
      public double ObjectiveValue { get; set; } = 10000;
      
      #region SBB Data Model

      [DataMember(Name = "service_intention_id", Order = 1)]
      public string ServiceIntentionId { get; private set; }

      [DataMember(Name = "train_run_sections", Order = 2)]
      public List<Section> TrainRunSections { get; private set; }

      #endregion

      public void AssignPath(Train train, IEnumerable<Section> sections, bool reinit = true) {
         TrainRunSections = sections.Select(s => new Section(s, reinit)).ToList();

         foreach (var section in TrainRunSections) {
            section.SectionRequirement = train.RequirementKeys.Contains(section.SectionMarker) ? section.SectionMarker : null;
         }
      }
   }
}
