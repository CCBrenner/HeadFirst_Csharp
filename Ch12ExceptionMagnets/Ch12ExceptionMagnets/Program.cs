using System;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("When it ");
        ExTestDrive.Zero("yes");
        Console.Write(" it ");
        ExTestDrive.Zero("no");
        Console.WriteLine(".");
    }
}

class MyException : Exception { }

class ExTestDrive
{
    public static void Zero(string test)
    {
        try
        {
            Console.Write("t");
            DoRisky(test);
            Console.Write("r");
            Console.Write("o");
        }
        catch (MyException)
        {
            Console.Write("a");
        }
        finally
        {
            Console.Write("w");
            Console.Write("s");
        }

    }
    static void DoRisky(String t)
    {
        Console.Write("h");
        if (t == "yes")
        {
            throw new MyException();
        }
    }
}