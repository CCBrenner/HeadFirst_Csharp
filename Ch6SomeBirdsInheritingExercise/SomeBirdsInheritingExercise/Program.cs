using System;

class Egg
{
    public double Size { get; private set; }
    public string Color { get; private set; }
    public Egg(double size, string color)
    {
        Size = size;
        Color = color;
    }
    public string Description
    {
        get { return $"A {Size:0.0}cm {Color} egg."; }
    }
}

class BrokenEgg : Egg
{
    // LINE OF INTEREST: subclass uses its own constructor method but it is defined based on the base class' paramters. So we still need to pass the proper paramters to the base class as the constructor method being inherited
    public BrokenEgg(string color) : base(0, $"broken {color}")
    {
        Console.WriteLine("A bird laid a broken egg.");
    }
}

abstract class Bird
{
    public static Random Randomizer = new Random();
    public abstract Egg[] LayEggs(int numberOfEggs);  //abstract classes do not have bodies
}

class Pigeon : Bird
{
    public override Egg[] LayEggs(int numberOfEggs)
    {
        Egg[] eggs = new Egg[numberOfEggs];
        for (int i = 0; i < numberOfEggs; i++)
        {
            // double size = Bird.Randomizer.NextDouble() * 2 + 1;
            // eggs[i] = new Egg(size, "white");

            double size = Bird.Randomizer.Next(0, 4);
            if (Bird.Randomizer.Next(4) == 0)
                eggs[i] = new BrokenEgg("white");
            else
                eggs[i] = new Egg(size, "white");
        }
        return eggs;
    }
}

class Ostrich : Bird
{
    public override Egg[] LayEggs(int numberOfEggs)
    {
        Egg[] eggs = new Egg[numberOfEggs];
        for (int i = 0; i < numberOfEggs; ++i)
        {
            eggs[i] = new Egg(Bird.Randomizer.NextDouble() * 2 + 1, "speckled");
        }
        return eggs;
    }
}


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Bird bird;
            Console.Write("\nPress P for pigeon, O for ostrich: ");
            char key = Char.ToUpper(Console.ReadKey().KeyChar);
            if (key == 'P') bird = new Pigeon();
            else if (key == 'O') bird = new Ostrich();
            else return;
            Console.Write("\nHow many eggs should I lay? ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfEggs)) return;
            Egg[] eggs = bird.LayEggs(numberOfEggs);
            foreach (Egg egg in eggs)
            {
                Console.WriteLine(egg.Description);
            }
        }
    }
}

