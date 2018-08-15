using System;
using System.Windows.Forms;
using TrainScheduler.UI;

namespace TrainScheduler
{
    static class Program
    {
        /// <summary>
        /// MainForm instance accessible as a singleton
        /// </summary>
        internal static MainForm MainForm { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm();
            Application.Run(MainForm);
        }
    }
}
