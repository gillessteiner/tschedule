using System;
using System.Windows.Forms;

namespace TSchedule2
{
   static class Program
   {
      internal static MainForm MainForm { get; private set; }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main() {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         MainForm = new MainForm();
         Application.Run(MainForm);
      }
   }
}
