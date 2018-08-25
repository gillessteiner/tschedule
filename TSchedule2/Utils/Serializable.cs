using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Utils
{
   /// <summary>
   /// Method to serialize to json
   /// </summary>
   [DataContract]
   public abstract class Serializable
   {
      public string ToJson() {
         var ms = new MemoryStream();
         var ser = new DataContractJsonSerializer(GetType());
         ser.WriteObject(ms, this);
         var json = ms.ToArray();
         ms.Close();
         return Encoding.UTF8.GetString(json, 0, json.Length);
      }
   }
}
