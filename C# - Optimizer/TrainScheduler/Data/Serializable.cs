﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace TrainScheduler.Data
{
    [DataContract]
    public abstract class Serializable : ISerializable
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

        public string ToJson()
        {
            var ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
            ser.WriteObject(ms, this);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
