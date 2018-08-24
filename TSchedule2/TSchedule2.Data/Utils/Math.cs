﻿using System;

namespace TSchedule2.Data.Utils
{
   public static class Math
   {
      public static DateTime Min(DateTime a, DateTime b) {
         return a <= b ? a : b;
      }

      public static DateTime Max(DateTime a, DateTime b) {
         return a >= b ? a : b;
      }

      public static TimeSpan Min(TimeSpan a, TimeSpan b) {
         return a <= b ? a : b;
      }

      public static TimeSpan Max(TimeSpan a, TimeSpan b) {
         return a >= b ? a : b;
      }

      public struct Period
      {
         public DateTime Start { get; set; }
         public DateTime End { get; set; }
      }

      public static bool Intersect(Period A, Period B) {
         return Intersect(A.Start, A.End, B.Start, B.End);
      }

      public static bool Intersect(DateTime a1, DateTime a2, DateTime b1, DateTime b2) {
         return (!((a1 >= b2) || (a2 <= b1)));
      }

      public static Random RndGenerator { get; } = new Random(123456789);
   }
}
