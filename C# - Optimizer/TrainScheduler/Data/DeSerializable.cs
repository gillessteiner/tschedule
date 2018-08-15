using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TrainScheduler.Data
{
    [DataContract]
    public abstract class DeSerializable : IDeSerializable
    {
        // Do not expose outside 
        // Copy from temporary object, stealing references to collections
        protected abstract void CopyFrom(object other);

        public void FromJson(string json)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var ser = new DataContractJsonSerializer(this.GetType());
            this.CopyFrom( ser.ReadObject(ms) );
            ms.Close();
        }

      
    }
}
