using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;

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

      public static void AppendText(this RichTextBox box, string text, Color color, bool bold = false) {
         if (!box.IsDisposed) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            var saveFont = (Font)box.Font.Clone();
            if (bold)
               box.SelectionFont = new Font(saveFont, FontStyle.Bold);
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            if (bold)
               box.SelectionFont = saveFont;
         }
      }

      public static string AlignCenter(this string text, int space) {
         return text.Length < space ? text.PadLeft((text.Length + space) / 2) : text;
      }

      public static T[] Shuffle<T>(this IEnumerable<T> source) {
         return source.OrderBy(x => Math.RndGenerator.Next()).ToArray();
      }
   }
}
