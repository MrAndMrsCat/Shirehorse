using System.Drawing;

namespace Shirehorse.Core.Configuration
{
    public class Option_Color : CustomOption
    {
        public override string Type => typeof(Color).ToString();
        public override object Deserialize(string data)
        {
            return Color.FromName(data);
        }
        public override string Serialize(object obj)
        {
            return ((Color)obj).ToString();
        }

        public override IOptionUserControl? Control => null;
    }


}

