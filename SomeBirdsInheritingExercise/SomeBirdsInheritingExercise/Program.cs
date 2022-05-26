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

class Bird
{
    public static Random Randomizer = new Random();
    public virtual Egg[] LayEggs(int numberOfEggs)
    {
        Console.Error.WriteLine("Bird.LayEggs should never get called");
        return new Egg[0];
    }
}

class Pigeon : Bird
{
    public override Egg[] LayEggs(int numberOfEggs)
    {
        // left off here! Egg[] array needs to be given a specified length and
        // then each added Egg object assigned to their own unique position (no simple append possible in Csharp)
        Egg[] eggs = new Egg[numberOfEggs];
        for (int i = 0; i < numberOfEggs; i++)
        {
            double size = Randomizer.Next(1, 4);
            eggs[i] = new Egg(size, "white");
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
            if (key == 'O') bird = new Ostrich();
            else return;
            Console.Write("\nHow many eggs should I lay?");
            if (!int.TryParse(Console.ReadLine(), out int numberOfEggs)) return;
            Egg[] eggs = bird.LayEggs(numberOfEggs);
            foreach (Egg egg in eggs)
            {
                Console.WriteLine(egg.Description);
            }
        }
    }
}

