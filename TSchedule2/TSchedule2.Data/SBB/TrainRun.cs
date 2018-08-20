using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TSchedule2.Data.SBB
{
   [DataContract]
   public class TrainRun
   {
      #region SBB Data Model
      [DataMember(Name = "service_intention_id", Order = 1)]
      public string ServiceIntentionId { get; private set; }
     
      [IgnoreDataMember] private List<TrainRunSection> _trainRunSections;

      [DataMember(Name = "train_run_sections", Order = 2)]
      public List<TrainRunSection> TrainRunSections
      {
         get => _trainRunSections;
         private set => _trainRunSections = value;
      }
      #endregion
   }
}
