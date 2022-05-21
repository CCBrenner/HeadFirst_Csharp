using System;

class ConstructorTest
{
    public int i = 1;

    // Parameterless constructor
    public ConstructorTest()
    {
        Console.WriteLine($"i is {i}.");
    }

    public static void Main(string[] args)
    {
        ConstructorTest constructorTest = new ConstructorTest();
    }
}
