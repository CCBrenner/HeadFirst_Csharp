using System;
using System.Collections.Generic;

enum Flapjack
{
    Crispy = 0,
    Soggy = 1,
    Browned = 2,
    Banana = 3,
}

class Lumberjack
{
    public Lumberjack(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    private Stack<Flapjack> flapjackStack;

    public void TakeFlapjack(int numFlapjacks, int randomFlap)
    {
        Flapjack aFlap = (Flapjack)randomFlap;
        flapjackStack.Push(aFlap);
    }
    public void EatFlapjacks()
    {

    }
}

class Program
{
    public static void Main(string[] args)
    {
        // Initialize
        Queue<Lumberjack> lumberjacks = new Queue<Lumberjack>();
        Random random = new Random();

        // Begin program by asking which Lumberjacks are eating in the cafeteria
        Console.Write("First Lumberjack's name: ");
        Lumberjack firstJack = new Lumberjack(Console.ReadLine());

        // For each lumberjack, ask how many flapjacks they are taking and add to their count
        Console.Write("Number of flapjacks: ");
        if (int.TryParse(Console.ReadLine(), out int firstJackFlaps)) {
            firstJack.TakeFlapjack(firstJackFlaps, random.Next(4));
        }

        lumberjacks.Enqueue(firstJack);

        while (true)
        {
            // Begin program by asking which Lumberjacks are eating in the cafeteria
            Console.Write("Next Lumberjack's name (blank to end): ");

            // STOPPED HERE!! - NEED TO HANDLE IF BLANK THEN BREAK OR SOMETHING LIKE THAT

            Lumberjack nextJack = new Lumberjack(Console.ReadLine());

            // For each lumberjack, ask how many flapjacks they are taking and add to their count
            Console.Write("Number of flapjacks: ");
            if (int.TryParse(Console.ReadLine(), out int nextJackFlaps))
            {
                nextJack.TakeFlapjack(nextJackFlaps, random.Next(4));
            }

            lumberjacks.Enqueue(nextJack);
        }

        // State which lumberjacks is eating and then print which flapjacks they ate off the stack
        // When one is done, move on to the next lumberjack
    }
}