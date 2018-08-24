using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TSchedule2.Data.Model;

namespace TSchedule2.Data.SBB {
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

      public void AssignPath(IEnumerable<Section> sections) {
         TrainRunSections = sections.Select(s => new Section(s)).ToList();
      }
   }
}
