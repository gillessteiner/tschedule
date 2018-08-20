using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using TSchedule2.Data.Utils;

namespace TSchedule2.Data.SBB
{
   public class Solution : Serializable
   {
      public Solution() { }

      public Solution(ProblemInstance problem) {
         Problem = problem;
      }

      [IgnoreDataMember]
      public ProblemInstance Problem { get; set; }

      #region SBB Data Model

      [DataMember(Name = "problem_instance_label", Order = 1)]
      private string ProblemLabel
      {
         get => Problem?.Label;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "problem_instance_hash", Order = 2)]
      private int ProblemHash
      {
         get => Problem?.Hash ?? 0;
         set { /* Ignore setter */ }
      }

      [DataMember(Name = "hash", Order = 3)] public int Hash { get; private set; } = 101;

      [IgnoreDataMember]
      private TrainRun[] _trainRuns;

      [DataMember(Name = "train_runs", Order = 4)]
      internal TrainRun[] TrainRuns
      {
         get => _trainRuns;
         set => _trainRuns = value;
      }

      #endregion

      public static Solution FromJson(string json) {
         var res = new Solution();
         var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
         var ser = new DataContractJsonSerializer(res.GetType());
         res = (Solution)ser.ReadObject(ms);
         ms.Close();
         return res;
      }
   }
}
