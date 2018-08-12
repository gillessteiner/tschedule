using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheduler.Data
{
    [DataContract]
    public class ProblemInstance : Serializable
    {
        public ProblemInstance()
        {
        }

        public ProblemInstance(string label, int hash) : this()
        {
            Label = label;
            Hash = hash;
        }

        [DataMember(Name ="label", Order=1)]
        public string Label { get; private set; }

        [DataMember(Name ="hash", Order =2)]
        public int Hash { get; private set; }

        [DataMember(Name ="service_intentions", Order =3)]
        public ServiceIntention[] serviceIntentions { get; private set; }

        [DataMember(Order =4)]
        public Route[] routes { get; private set; }

        [DataMember(Order =5)]
        public Resource[] resources { get; private set; }

        [DataMember(Order = 6)]
        public Parameters parameters { get; private set; }

        public static ProblemInstance FromJson(string json)
        {
            var res = new ProblemInstance();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(res.GetType());
            res = ser.ReadObject(ms) as ProblemInstance;
            ms.Close();
            return res;
        }
    }
}
