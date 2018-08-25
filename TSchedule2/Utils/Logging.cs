using System;

namespace Utils
{
   public static class Logging
   {
      public class LogEventArgs : EventArgs
      {
         public enum MessageType { Info, Warning, Error, Success }

         public MessageType Type { get; set; } = MessageType.Info;
         public string Message { get; set; } = string.Empty;
         public bool Newline { get; set; } = true;
      }

      public delegate void LogEventHandler(object sender, LogEventArgs e);

      public static readonly string HorizontalLine = new string('-', 100);

   }
}
