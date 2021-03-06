﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Data.SBB {
   [DataContract]
   public class ProblemInstance {

      #region SBB Data Model

      [DataMember(Name = "label", Order = 1)]
      public string Label { get; private set; }

      [DataMember(Name = "hash", Order = 2)]
      public int Hash { get; private set; }

      [DataMember(Name = "service_intentions", Order = 3)]
      internal ServiceIntention[] ServiceIntentions { get; private set; }

      [DataMember(Name = "routes", Order = 4)]
      internal Route[] Routes { get; private set; }

      [DataMember(Name = "resources", Order = 5)]
      internal Resource[] Resources { get; private set; }

      #endregion

      public static ProblemInstance FromJson(string json) {
         var res = new ProblemInstance();
         var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
         var ser = new DataContractJsonSerializer(res.GetType());
         res = (ProblemInstance) ser.ReadObject(ms);
         ms.Close();
         return res;
      }
   }
}