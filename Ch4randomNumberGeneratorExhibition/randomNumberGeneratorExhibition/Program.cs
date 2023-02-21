// See https://aka.ms/new-console-template for more information

using System;

class Program
{
    public static void Main()
    {
        Random random = new Random();
        int randomInt = random.Next();
        Console.WriteLine(randomInt.ToString());

        int zeroToNine = random.Next(10);
        Console.WriteLine(zeroToNine);

        int dieRoll = random.Next(1, 7);
        Console.WriteLine(dieRoll);

        double randomDouble = random.NextDouble();
        Console.WriteLine(randomDouble);
        Console.WriteLine(randomDouble * 100);
        Console.WriteLine((float)randomDouble * 100F);
        Console.WriteLine((decimal)randomDouble * 100M);

        int zeroToOne = random.Next(2);
        bool coinToss = Convert.ToBoolean(zeroToOne);
        Console.WriteLine(coinToss);
    }
}
