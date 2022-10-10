using System;

#nullable enable
class Program
{
    public static void Main(string[] args)
    {
        using (var stringReader = new StringReader(""))
        {
            string nextline = stringReader.ReadLine() ?? String.Empty;
            Console.WriteLine("Line length is {0}", nextline.Length);
        }
    }
}