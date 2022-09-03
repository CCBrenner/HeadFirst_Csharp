using System;
using System.IO;
using System.Text;

class Program 
{
    public static void Main(string[] args)
    {
        File.WriteAllText("eureka.txt", "Eureka!");
        byte[] eurekaBytes = File.ReadAllBytes("eureka.txt");
        foreach (byte b in eurekaBytes)
        {
            Console.Write("{0} ", b);  // Binary
        }
        Console.WriteLine();
        foreach (byte b in eurekaBytes)
        {
            Console.Write("{0:x2} ", b);  // Hex
        }
        Console.WriteLine();
        Console.WriteLine(Encoding.UTF8.GetString(eurekaBytes));
    }
}
