using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirehorse.Core.Drawing
{
    public class ConsolePixel
    {
        static ConsolePixel()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    OrderedBlendingCharacters = "            ....````',---;;^^!~\"\"÷++++</*cTLYY(1jJ7[32Skpq4569%GD#ÅR$$¶¶M§§@ÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑ";
                    break;

                default:
                    OrderedBlendingCharacters = " ```````-':::::,\"\"\"; ; ; ;= !²</*LLr)(7J{FI1[Zo2SEVù4KA96G½ê¼D#BNN§0$%%&&&ÑÑÑÑÑ¶¶¶¶¶¶@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
                    break;
            }


            BuildColorMap();
            BuildPalette();
        }

        public Color Color { get; private set; }
        public ConsoleColor ForeColor { get; private set; }
        public ConsoleColor BackColor { get; private set; }
        //public char Character => _orderedMixingChars[_characterIndex];
        public char Character => OrderedBlendingCharacters[(int)(OrderedBlendingCharacters.Length * 2 * Blend)];


        /// <summary> Blend up to 50% </summary>
        public static string OrderedBlendingCharacters = "                  `-'''':_,,\" ^ ^^;!²>*+cLr(7çsFI31Z2yEaV4KU96OG¼ÄM#ÆW§0$%%%&&&&&&ÑÑÑÑÑ¶¶¶¶¶¶@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public double Blend { get; private set; }

        //public static ConsolePixel FromColor(Color color)
        //{
        //    ConsolePixel result = new() { Color = color };

        //    var consoleColors = Enum.GetValues(typeof(ConsoleColor));
        //    double minimumResidual = double.MaxValue;
        //    double targetRed = color.R / 255f;
        //    double targetGreen = color.G / 255f;
        //    double targetBlue = color.B / 255f;



        //    double Residual(double target, double forecolor, double backcolor, double blend)
        //    {
        //        double error = target - ((forecolor * blend) + (backcolor * (1 - blend))) / 2f;
        //        return error * error;
        //    }

        //    //double Residual(double target, double forecolor, double backcolor, double blend)
        //    //{
        //    //    double foreError = target - (forecolor * blend);
        //    //    double backError = target - (backcolor * (1 - blend));

        //    //    return foreError * foreError + backError * backError;
        //    //}

        //    // brute force result
        //    foreach (ConsoleColor forecolor in consoleColors)
        //    {
        //        foreach (ConsoleColor backcolor in consoleColors)
        //        {
        //            if (!Adjacent(forecolor, backcolor)) continue;

        //            for (int blendIndex = 0; blendIndex < OrderedBlendingCharacters.Length; blendIndex++)
        //            {
        //                double blend = (double)blendIndex / (OrderedBlendingCharacters.Length * 2);
        //                var (foreRed, foreGreen, foreBlue) = _colorMap[forecolor];
        //                var (backRed, backGreen, backBlue) = _colorMap[backcolor];

        //                double residuals =
        //                    Residual(targetRed, foreRed, backRed, blend) +
        //                    Residual(targetGreen, foreGreen, backGreen, blend) +
        //                    Residual(targetBlue, foreBlue, backBlue, blend);

        //                if (residuals < minimumResidual)
        //                {
        //                    minimumResidual = residuals;
        //                    result.ForeColor = forecolor;
        //                    result.BackColor = backcolor;
        //                    result.Blend = blend;
        //                }
        //            }
        //        }
        //    }

        //    return result;

        //}

        public static ConsolePixel FromColor(Color color)
        {
            ConsolePixel result = new();

            double minimumResidual = double.MaxValue;

            double Residual(ConsolePixel consolePixel)
            {
                double red = color.R - consolePixel.Color.R;
                double green = color.G - consolePixel.Color.G;
                double blue = color.B - consolePixel.Color.B;

                return red * red + green * green + blue * blue;
            }

            foreach (var pixel in _paletteList)
            {
                double residual = Residual(pixel);

                if (residual < minimumResidual)
                {
                    minimumResidual = residual;
                    result = pixel;
                }
            }

            return result;
        }



        private static void BuildPalette()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));

            long ticks = DateTime.Now.Ticks;

            double RMS(double first, double second) => Math.Sqrt((first * first + second * second) / 2);

            foreach (ConsoleColor forecolor in consoleColors)
            {
                Color fore = Color.FromName(forecolor.ToString());

                foreach (ConsoleColor backcolor in consoleColors)
                {
                    // non-adjacent colors don't mix well, skip them
                    if (!Adjacent(forecolor, backcolor)) continue;

                    Color back = Color.FromName(backcolor.ToString());

                    if (forecolor == backcolor)
                    {
                        _paletteList.Add(new()
                        {
                            ForeColor = forecolor,
                            BackColor = backcolor,
                            Blend = 0,
                            Color = back
                        });
                    }
                    else
                    {
                        for (int blendByte = 0; blendByte < 128; blendByte++)
                        {
                            double blend = blendByte / 255f;
                            byte red = (byte)RMS((fore.R * blend), (back.R * (1 - blend)));
                            byte green = (byte)RMS((fore.G * blend), (back.G * (1 - blend)));
                            byte blue = (byte)RMS((fore.B * blend), (back.B * (1 - blend)));

                            Color mixed = Color.FromArgb(red, green, blue);

                            _paletteList.Add(new()
                            {
                                ForeColor = forecolor,
                                BackColor = backcolor,
                                Blend = blend,
                                Color = mixed
                            });
                        }
                    }


                }
            }

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine($"Build palette took {(double)(DateTime.Now.Ticks - ticks) / 10000f} ms");
        }

        private static bool Adjacent(ConsoleColor color1, ConsoleColor color2)
        {
            switch (color1)
            {
                case ConsoleColor.Red:
                case ConsoleColor.DarkRed:
                    switch (color2)
                    {

                        case ConsoleColor.Green:
                        case ConsoleColor.DarkGreen:
                        case ConsoleColor.Cyan:
                        case ConsoleColor.DarkCyan:
                        case ConsoleColor.Blue:
                        case ConsoleColor.DarkBlue:
                            return false;

                        default:
                            return true;
                    }

                case ConsoleColor.Green:
                case ConsoleColor.DarkGreen:
                    switch (color2)
                    {
                        case ConsoleColor.Red:
                        case ConsoleColor.DarkRed:
                        case ConsoleColor.Magenta:
                        case ConsoleColor.DarkMagenta:
                        case ConsoleColor.Cyan:
                        case ConsoleColor.DarkCyan:
                            return false;

                        default:
                            return true;
                    }

                case ConsoleColor.Blue:
                case ConsoleColor.DarkBlue:
                    switch (color2)
                    {
                        case ConsoleColor.Red:
                        case ConsoleColor.DarkRed:
                        case ConsoleColor.Yellow:
                        case ConsoleColor.DarkYellow:
                        case ConsoleColor.Green:
                        case ConsoleColor.DarkGreen:
                            return false;

                        default:
                            return true;
                    }

                default:
                    return true;
            }
        }

        private static readonly List<ConsolePixel> _paletteList = new();

        public const int SerializationSize = 2;

        public byte[] ToBytes()
        {
            int colors = ((int)ForeColor << 4) + (int)BackColor;
            int blend = (int)(Blend * 255);

            return new byte[]
            {
                (byte)colors,
                (byte)blend,
            };
        }

        public static ConsolePixel FromBytes(byte[] bytes)
        {
            double blend = (double)bytes[1] / 255f;

            return new()
            {
                ForeColor = (ConsoleColor)(bytes[0] >> 4),
                BackColor = (ConsoleColor)(bytes[0] & 0xF),
                Blend = blend,
            };
        }

        private static readonly Dictionary<ConsoleColor, (double red, double green, double blue)> _colorMap = new();

        private static void BuildColorMap()
        {
            foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
            {
                Color color = Color.FromName(consoleColor.ToString());

                _colorMap[consoleColor] = (color.R / 255f, color.G / 255f, color.B / 255f);
            }
        }

        public override string ToString() => $"Fore: {ForeColor}, Back: {BackColor}, Char: '{Character}'";



    }
}
