using System;

class ConstructorTest
{
    public int i = 1;
    public string Name;

    // Parameterless constructor
    public ConstructorTest(string name)
    {
        Console.WriteLine($"i is {i}.");
        this.Name = name;
    }

    public static void Main(string[] args)
    {
        ConstructorTest constructorTest = new ConstructorTest("Kyle");
        Console.WriteLine(constructorTest.Name);
    }
}
