using System;
using AmazingExtensions;

class Program
{
    public static void Main(string[] args)
    {
        string message = "Evil clones are wrecking havoc. Help!";
        Console.WriteLine(message.IsDistressCall());
    }
}
