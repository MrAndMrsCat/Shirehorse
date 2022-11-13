using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shirehorse.Core.Drawing;

namespace Shirehorse.Windows
{
    internal class WindowsConsolePixelConvertor : ConsolePixelConvertor
    {
#pragma warning disable CA1416 // Validate platform compatibility

        public static void LoadBitmap(string filepath)
        {
            var info = new FileInfo(filepath);

            if (!info.Exists) throw new Exception($"{filepath} does not exist");

            using Bitmap bitmap = new(filepath, true);
            var pixels = BitmapToPixels(bitmap);
            Save(Path.ChangeExtension(filepath, BinaryFileExtension), pixels);

            PrintToConsole(pixels);
        }

        private static ConsolePixel[,] BitmapToPixels(Bitmap bitmap)
        {
            bool update = true;

            if (update)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.White;
                for (int x = 0; x < bitmap.Height; x++) Console.Write(' ');
                Console.WriteLine();
            }

            ConsolePixel[,] pixels = new ConsolePixel[bitmap.Width, bitmap.Height];

            long ticks = DateTime.Now.Ticks;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixels[x, y] = ConsolePixel.FromColor(bitmap.GetPixel(x, y));
                }

                if (update)
                {
                    Console.Write(' ');
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine($"\nConversion took {(double)(DateTime.Now.Ticks - ticks) / 10000f} ms");

            return pixels;
        }

        public static string CalibrationFilePath { get; set; } = "c:\\temp\\splashTests\\calibration.bmp";
        public static Dictionary<char, double>? MaskValues { get; set; }


        internal static void ProcessCalibrationFile()
        {
            if (!File.Exists(CalibrationFilePath)) throw new Exception($"{CalibrationFilePath} does not exist");

            MaskValues = new();

            using Bitmap bitmap = new(CalibrationFilePath, true);
            int width = bitmap.Width / _printableCharacters.Length;
            int height = bitmap.Height;
            int xOffset = 0;

            for (int index = 0; index < _printableCharacters.Length; index++)
            {
                double maskValue = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = bitmap.GetPixel(x + xOffset, y);

                        // ToDo: check 
                        //maskValue += color.GetBrightness() * 255;
                        maskValue += (color.R + color.G + color.B) / 3;
                    }
                }

                MaskValues[_printableCharacters[index]] = maskValue / (width * height);

                xOffset += width;
            }

            // range of blend is 0-255 (0-100%)
            SortedDictionary<int, char> blendToChar = new();

            foreach (var (character, maskValue) in MaskValues)
            {
                int key = (int)(maskValue);

                if (!blendToChar.ContainsKey(key))
                {
                    blendToChar[key] = character;
                }
            }

            char previousChar = blendToChar.Last().Value;
            for (int blend = 128; blend > 0; --blend)
            {
                if (blendToChar.TryGetValue(blend, out char currentChar))
                {
                    previousChar = currentChar;
                }
                else
                {
                    blendToChar[blend] = previousChar;
                }
            }

            blendToChar = new SortedDictionary<int, char>(blendToChar.Where(kv => kv.Key < 128).ToDictionary(k => k.Key, v => v.Value));

            string orderedMasks = new(blendToChar.Values.ToArray());

            ConsolePixel.OrderedBlendingCharacters = orderedMasks;

            Console.WriteLine("Ordered mask calibration:");
            Console.WriteLine(orderedMasks);

        }

#pragma warning restore CA1416 // Validate platform compatibility
    }
}
