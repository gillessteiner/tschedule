using System;

namespace TrainScheduler.Data {
   public struct Conflict {
      public int Section1 { get;  set; }
      public int Section2 { get;  set; }
      public string Resource { get;  set; }

      public DateTime Entry1 { get; set; }
      public DateTime Entry2 { get; set; }
      public DateTime Exit1 { get; set; }
      public DateTime Exit2 { get; set; }
   }
}
