using System;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        using (StreamWriter writer = new StreamWriter("hello.txt"))
        {
            writer.WriteLine("Hello!!!");
        }
        byte[] greetings;
        greetings = File.ReadAllBytes("hello.txt");
        Array.Reverse(greetings);
        File.WriteAllBytes("hello.txt", greetings);
    }
}