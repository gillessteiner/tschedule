using System;

namespace TSchedule2.Data.Utils
{
   public static class Extensions
   {
      public static TimeSpan StringToDuration(this string str) {
         return string.IsNullOrEmpty(str) ? TimeSpan.Zero : System.Xml.XmlConvert.ToTimeSpan(str);
      }

      public static string DurationToString(this TimeSpan time) {
         return System.Xml.XmlConvert.ToString(time);
      }
   }
}
