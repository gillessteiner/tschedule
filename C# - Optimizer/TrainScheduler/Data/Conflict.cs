using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheduler.Data
{
    public class Conflict
    {
        public Conflict(string t1, string t2, string res)
        {
            Train1 = t1;
            Train2 = t2;
            Resource = res;
        }

        public string Train1 { get; private set; }
        public string Train2 { get; private set; }
        public string Resource { get; private set; }
    }
}
