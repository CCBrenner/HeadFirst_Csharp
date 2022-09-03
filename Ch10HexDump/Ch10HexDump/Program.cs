using System;
using System.Text;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        var position = 0;
        using (var reader = new StreamReader("textdata.txt"))
        {
            while (!reader.EndOfStream)
            {
                // Read up to the next 16 bytes from the file into a byte array:
                var buffer = new char[16];
                var bytesRead = reader.ReadBlock(buffer, 0, 16);

                // Write the position (or offset) in hex, followed by a colon and space
                for (int i = 0; i < 16; i++)
                {
                    if (i < bytesRead)
                        Console.Write("{0:x2} ", (byte)buffer[i]);
                    else
                        Console.Write("   ");
                    if (i == 7) Console.Write("-- ");
                }

                // Write the actual characters in the byte array
                var bufferContents = new string(buffer);
                Console.WriteLine("   {0}", bufferContents.Substring(0, bytesRead));
            }
        }
    }
}