using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TrainScheduler.Data
{
    [DataContract]
    public abstract class Serializable : ISerializable
    {
        public string ToJson()
        {
            var ms = new MemoryStream();
            var ser = new DataContractJsonSerializer(this.GetType());
            ser.WriteObject(ms, this);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
