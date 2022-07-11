using System;
using System.Collections.Generic;

enum Flapjack
{
    Crispy,
    Soggy,
    Browned,
    Banana,
}

class Lumberjack
{
    public Lumberjack(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    private Stack<Flapjack> flapjackStack = new Stack<Flapjack>();

    public void TakeFlapjack(Flapjack randomFlapjack)
    {
        flapjackStack.Push(randomFlapjack);
    }
    public void EatFlapjacks()
    {
        while (flapjackStack.Count > 0)
        {
            Flapjack currentFlapjack = flapjackStack.Pop();
            Console.WriteLine($"{Name} ate a {currentFlapjack} flapjack.");
        }
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
        Lumberjack firstJack;
        string name;
        while (true)
        {
            // Valid data:
            if ((name = Console.ReadLine()).Length >= 2)
            {
                firstJack = new Lumberjack(name);

                // For each lumberjack, ask how many flapjacks they are taking and add to their count
                while (true)
                {
                    Console.Write("Number of flapjacks: ");
                    if (int.TryParse(Console.ReadLine(), out int numFirstJackFlapjacks))
                    {
                        for (int i = 0; i < numFirstJackFlapjacks; i++)
                        {
                            firstJack.TakeFlapjack((Flapjack)random.Next(4));
                        }
                        break;
                    }
                }
                lumberjacks.Enqueue(firstJack);
                Console.Write("\nNext Lumberjack's name (blank to continue): ");
                continue;
            }
            // Atleast one lumberjack entered and user chooses to move on
            else if (lumberjacks.Count > 0 && name == "")
                break;
            // No lumberjacks and user gives a blank entry, so retry for first lumberjack
            else if (lumberjacks.Count > 0 && name.Length < 2)
                Console.Write("Next Lumberjack's name (blank to continue): ");
            else
                Console.Write("First Lumberjack's name: ");
        }

        // State which lumberjacks is eating and then print which flapjacks they ate off the stack, then move on to next lumberjack
        while (lumberjacks.Count() != 0)
        {
            Console.WriteLine($"\n{lumberjacks.Peek().Name} is eating flapjacks.");
            lumberjacks.Dequeue().EatFlapjacks();
        }
    }
}
