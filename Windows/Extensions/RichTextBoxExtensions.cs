namespace Shirehorse.Core.Extensions
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AppendText(this RichTextBox box, string text, Color color, Color back_color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionBackColor = back_color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
        }

        public static void AppendText(this RichTextBox box, string text, Color color, Color back_color, FontStyle font_style)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionBackColor = back_color;
            box.SelectionFont = new Font(box.Font, font_style);
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
            box.SelectionFont = box.Font;
        }

        public static void InvertedText(this RichTextBox box, string text)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = box.BackColor;
            box.SelectionBackColor = box.ForeColor;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
        }

        public static void AppendBinaryText(this RichTextBox box, byte[] bytes)
        {
            box.SuspendLayout();
            foreach (var b in bytes)
            {
                if (b > 0x1F && b < 0x7F)
                    box.AppendText(((char)b).ToString(), Color.Black);
                else
                    box.AppendText(BitConverter.ToString(new byte[1] { b }), Color.White, Color.Blue);
            }
            box.ResumeLayout();
        }
    }
}
