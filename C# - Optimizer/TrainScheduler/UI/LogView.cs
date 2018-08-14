using System.Drawing;
using System.Windows.Forms;

namespace TrainScheduler.UI
{
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
        }

        private void WriteLine(string text, Color? col = null)
        {
            txtArea.AppendText(text + "\r\n", col ?? Color.Black);
            Application.DoEvents();
        }

        private void Write(string text, Color? col = null)
        {
            txtArea.AppendText(text, col ?? Color.Black);
            Application.DoEvents();
        }

        public void WriteTitle(string text)
        {
            var line = new string('*', text.Length + 20);
            txtArea.AppendText(line + "\r\n", Color.Black, true);
            txtArea.AppendText(Extensions.AlignCenter(text, line.Length) + "\r\n", Color.Black, true);
            txtArea.AppendText(line + "\r\n", Color.Black, true);
        }

        public void ReportError(string msg, bool newline = true)
        {
            if (newline)
                WriteLine(msg, Color.Red);
            else
                Write(msg, Color.Red);
        }

        public void ReportWarning(string msg, bool newline = true)
        {
            if (newline)
                WriteLine(msg, Color.DarkOrange);
            else
                Write(msg, Color.DarkOrange);
        }

        public void ReportSuccess(string msg, bool newline = true)
        {
            if (newline)
                WriteLine(msg, Color.Green);
            else
                Write(msg, Color.Green);
        }

        public void ReportInfo(string msg, bool newline = true)
        {
            if (newline)
                WriteLine(msg, Color.Gray);
            else
                Write(msg, Color.Gray);
        }

        public void Clear()
        {
            txtArea.Clear();
        }
    }
}
