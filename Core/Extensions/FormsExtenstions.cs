using System.Drawing;

namespace Shirehorse.Core.Extensions
{
    public static class FormsExtensions
    {
        public static void Parse(this Rectangle rect, string text)
        {
            var rct = text
                .Trim('}')
                .Split(',')
                .Select(x => int.Parse(x.Split('=')[1]))
                .ToArray();

            rect.X = rct[0];
            rect.Y = rct[1];
            rect.Width = rct[2];
            rect.Height = rct[3];
        }
    }
}
