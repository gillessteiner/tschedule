using System;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Math = System.Math;

namespace TSchedule2.Views
{
   public partial class LogView : UserControl
   {
      public LogView() {
         InitializeComponent();
      }
      private void WriteLine(string text, Color? col = null) {
         txtArea.AppendText(text + "\r\n", col ?? Color.Black);
         Application.DoEvents();
      }

      private void Write(string text, Color? col = null) {
         txtArea.AppendText(text, col ?? Color.Black);
         Application.DoEvents();
      }

      private const int MinLineLength = 100;

      public void WriteTitle(string text) {
         var line = new string('*', Math.Max(text.Length + 60, MinLineLength));
         txtArea.AppendText(line + "\r\n", Color.Black, true);
         txtArea.AppendText(Extensions.AlignCenter(text, line.Length) + "\r\n", Color.Black, true);
         txtArea.AppendText(line + "\r\n", Color.Black, true);
      }

      public void ReportMessage(string msg, Logging.LogEventArgs.MessageType type = Logging.LogEventArgs.MessageType.Info, bool newline = true) {
         switch (type) {
            case Logging.LogEventArgs.MessageType.Info:
               ReportInfo(msg, newline);
               break;
            case Logging.LogEventArgs.MessageType.Warning:
               ReportWarning(msg, newline);
               break;
            case Logging.LogEventArgs.MessageType.Error:
               ReportError(msg, newline);
               break;
            case Logging.LogEventArgs.MessageType.Success:
               ReportSuccess(msg, newline);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(type), type, null);
         }
      }

      private void ReportGeneric(string msg, bool newline, Color color) {
         if (newline)
            WriteLine(msg, color);
         else
            Write(msg, color);
      }

      public void ReportError(string msg, bool newline = true) {
         ReportGeneric(msg, newline, Color.Red);
      }

      public void ReportWarning(string msg, bool newline = true) {
         ReportGeneric(msg, newline, Color.DarkOrange);
      }

      public void ReportSuccess(string msg, bool newline = true) {
         ReportGeneric(msg, newline, Color.Green);
      }

      public void ReportInfo(string msg, bool newline = true) {
         ReportGeneric(msg, newline, Color.Gray);
      }

      public void Clear() {
         txtArea.Clear();
      }
   }
}
