using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirehorse.Core.Drawing
{
    public class ConsolePixelConvertor
    {
        public const string BinaryFileExtension = "bci";

        public static void LoadBinaryFile(string filepath)
        {
            PrintToConsole(LoadBinaryConsoleImage(filepath));
        }

        public static void LoadBinaryFile(Stream stream)
        {
            PrintToConsole(LoadBinaryConsoleImage(stream));
        }



        internal static void PrintCalibrate()
        {
            Console.WriteLine("Printable character calibration:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.White;

            for (int i = 0; i < _printableCharacters.Length +2 ; i++) Console.Write(' ');
            Console.WriteLine();
            Console.Write(' ');

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(_printableCharacters);

            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(' ');
            Console.WriteLine();

            for (int i = 0; i < _printableCharacters.Length + 2; i++) Console.Write(' ');

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        protected static readonly string _printableCharacters = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£☼¥▌§cª«¬-r_°±²3'µ¶·,1º»¼½_¿AAAAÄÅÆÇEÉEEIIIIDÑOOOOÖxOUUUÜY_ßàáâaäåæçèéêëìíîïdñòóôoö÷oùúûüy_";



        protected static void PrintToConsole(ConsolePixel[,] pixels)
        {
            long ticks = DateTime.Now.Ticks;

            ConsolePixel pixel;

            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                for (int x = 0; x < pixels.GetLength(0); x++)
                {
                    pixel = pixels[x, y];
                    Console.ForegroundColor = pixel.ForeColor;
                    Console.BackgroundColor = pixel.BackColor;
                    Console.Write(pixel.Character);
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            //Console.WriteLine($"Draw took {(double)(DateTime.Now.Ticks - ticks) / 10000f} ms");
        }

        protected static void Save(string filepath, ConsolePixel[,] pixels)
        {
            if (File.Exists(filepath)) File.Delete(filepath);

            using BinaryWriter writer = new(File.OpenWrite(filepath));

            writer.Write(pixels.GetLength(0));
            writer.Write(pixels.GetLength(1));

            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                for (int x = 0; x < pixels.GetLength(0); x++)
                {
                    writer.Write(pixels[x, y].ToBytes());
                }
            }
        }

        private static ConsolePixel[,] LoadBinaryConsoleImage(Stream stream)
        {
            using BinaryReader reader = new(stream);

            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            ConsolePixel[,] pixels = new ConsolePixel[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixels[x, y] = ConsolePixel.FromBytes(reader.ReadBytes(ConsolePixel.SerializationSize));
                }
            }

            return pixels;
        }

        private static ConsolePixel[,] LoadBinaryConsoleImage(string filepath) => LoadBinaryConsoleImage(File.OpenRead(filepath));
    }
}
