using System.Drawing;
using System.Windows.Forms;

namespace TrainScheduler.UI
{
    public static class Extensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool bold = false)
        {
            if (!box.IsDisposed)
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                Font saveFont = (Font) box.Font.Clone();
                if (bold)
                    box.SelectionFont = new Font(saveFont, FontStyle.Bold);
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
                if (bold)
                    box.SelectionFont = saveFont;
            }
        }

        public static string AlignCenter(string text, int space)
        {
            return text.Length < space ? text.PadLeft((text.Length + space) / 2) : text;
        }
    }
}
