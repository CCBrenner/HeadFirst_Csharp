using System;
using System.Linq;

class PrintWhenGetting
{
    private int instanceNumber;
    public int InstanceNumber
    {
        set { instanceNumber = value; }
        get
        {
            Console.WriteLine($"Getting #{instanceNumber}");
            return instanceNumber;
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var listOfObjects = new List<PrintWhenGetting>();
        for (int i = 1; i < 5; i++)
            listOfObjects.Add(new PrintWhenGetting() { InstanceNumber = i });

        Console.WriteLine("Set up the query:");
        var result =
            from something in listOfObjects
            select something.InstanceNumber;

        var immediate = result.ToList();  // remove this and change foreach from 'immediate' to 'result' to demonstrate deferred evaluation of LINQ queries

        Console.WriteLine("Run the foreach");
        foreach (var number in immediate)
            Console.WriteLine($"Writing #{number}");
    }
}
