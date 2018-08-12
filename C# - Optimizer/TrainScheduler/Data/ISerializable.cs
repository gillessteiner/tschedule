using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheduler.Data
{
    interface ISerializable
    {
        // Implemented in base class Serializable
        string ToJson();
        void FromJson(string json);
    }
}
