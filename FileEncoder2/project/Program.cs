using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncoder2
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 6)
            {
                String source = args[4];
                String path = args[5];

                int[] key = new int[4];

                for(int i = 0; i < 4; i++)
                {
                    if (!int.TryParse(args[i], out key[i]) ) writeError(0);
                }

                if (File.Exists(source) && !File.Exists(path))
                {
                    byte[] buffer = File.ReadAllBytes(source);

                    writeLine("File (" + buffer.Length / 1000 + " kb):"+ source);
                    writeLine("Processing file...");

                    File.WriteAllBytes(path, Crypt(buffer, key));

                    writeColouredLine("Done !\n", ConsoleColor.Green);
                }
                else
                {
                    writeError(1);
                }

            }
            else
            {
                writeError(0);
            }

        }

        private static byte[] Crypt(byte[] raw, int[] key)
        {
            int m = 256;
            byte[] buffer = new byte[raw.Length];
            
            for (int i = 0; i < buffer.Length; i += 2)
            {
                if (i + 1 == raw.Length)
                {
                    buffer[i] = raw[i];
                }
                else
                {
                    buffer[i] = (byte)(((key[0] * raw[i]) + (key[1] * raw[i + 1])) % m);
                    buffer[i + 1] = (byte)(((key[2] * raw[i]) + (key[3] * raw[i + 1])) % m);
                }
            }

            return buffer;
        }

        public static void writeLine(string text)
        {
            Console.Write("[" + DateTime.Now.ToLongTimeString() + "] ");
            Console.WriteLine(text);
        }

        public static void writeColouredLine(string text, ConsoleColor color)
        {
            Console.Write("[" + DateTime.Now.ToLongTimeString() + "] ");

            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = old;
        }

        public static void writeErrorLine(string text)
        {
            Console.Write("[" + DateTime.Now.ToLongTimeString() + "] ");

            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERROR: ");
            Console.ForegroundColor = old;
            Console.WriteLine(text);
        }

        public static void writeError(int error)
        {
            switch (error)
            {
                case 0:
                    writeErrorLine("Invalid argument");
                    break;
                case 1:
                    writeErrorLine("Source file not found or target file already exists.");
                    break;
                default:
                    break;
            }
        }
    }
}
