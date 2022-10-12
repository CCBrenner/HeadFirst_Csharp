using System;
using System.Text;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        // In a shell that can browse the file tree, cd to the project's location and enter this command:
        // dotnet run bin/Debug/net6.0/binarydata.dat

        var position = 0;
        using (Stream input = GetInputStream(args))
        {
            // Read up to the next 16 bytes from the file into a byte array:
            var buffer = new byte[16];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                

                // Write the position (or offset) in hex, followed by a colon and space
                Console.Write("{0:x4} : ", position);
                position += bytesRead;

                // Write the hex value of each byte in the byte array
                for (int i = 0; i < 16; i++)
                {
                    if (i < bytesRead)
                        Console.Write("{0:x2} ", (byte)buffer[i]);
                    else
                        Console.Write("   ");
                    if (i == 7) Console.Write("-- ");

                    if (buffer[i] < 0x20 || buffer[i] > 0x7F) buffer[i] = (byte)'.';
                }

                // Write the actual characters in the byte array
                var bufferContents = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("   {0}", bufferContents.Substring(0, bytesRead));
            }
        }
    }

    static Stream GetInputStream(string[] args)
    {
        if ((args.Length != 1) || !File.Exists(args[0]))
            return Console.OpenStandardInput();
        else
        {
            try
            {
                return File.OpenRead(args[0]);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("Unable to read {0}, dumping from stdin: {1}", args[0], ex.Message);
                return Console.OpenStandardInput();
            }
        }
    }
}