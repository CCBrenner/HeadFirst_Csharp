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
            from o in listOfObjects
            select o.InstanceNumber;

        Console.WriteLine("Run the foreach");
        foreach (var number in listOfObjects)
            Console.WriteLine($"Writing #{number}");
    }
}
