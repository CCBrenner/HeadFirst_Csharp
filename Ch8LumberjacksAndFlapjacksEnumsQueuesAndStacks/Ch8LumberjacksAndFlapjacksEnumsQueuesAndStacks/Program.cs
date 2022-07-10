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

    public void TakeFlapjack(int randomFlapjack)
    {
        Flapjack aFlap = (Flapjack)randomFlapjack;
        flapjackStack.Push(aFlap);
    }
    public void EatFlapjacks()
    {
        for (int i = 0; flapjackStack.Count() != 0; i++)
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

        while (true)
        {
            // Begin program by asking which Lumberjacks are eating in the cafeteria
            Console.Write("First Lumberjack's name: ");
            string firstJackName = Console.ReadLine();
            Lumberjack firstJack;
            if (firstJackName.Length >= 2)
            {
                firstJack = new Lumberjack(firstJackName);
                while (true)
                {
                    // For each lumberjack, ask how many flapjacks they are taking and add to their count
                    Console.Write("Number of flapjacks: ");
                    if (int.TryParse(Console.ReadLine(), out int numFirstJackFlapjacks))
                    {
                        for (int i = 0; i < numFirstJackFlapjacks; i++)
                        {
                            firstJack.TakeFlapjack(random.Next(4));
                        }
                        break;
                    }
                }
                lumberjacks.Enqueue(firstJack);
                break;
            }
            else
                Console.WriteLine("Please enter a name that is atleast 2 characters long.");
        }

        while (true)
        {
            // Begin program by asking which Lumberjacks are eating in the cafeteria
            Console.Write("\nNext Lumberjack's name (blank to end): ");
            string nextJackName = Console.ReadLine();
            Lumberjack nextJack;
            if (nextJackName.Length >= 2)
            {
                nextJack = new Lumberjack(nextJackName);
                while (true)
                {
                    // For each lumberjack, ask how many flapjacks they are taking and add to their count
                    Console.Write("Number of flapjacks: ");
                    if (int.TryParse(Console.ReadLine(), out int numNextJackFlapjacks))
                    {
                        for (int i = 0; i < numNextJackFlapjacks; i++)
                        {
                            nextJack.TakeFlapjack(random.Next(4));
                        }
                        break;
                    }
                }
                // Add lumberjack to the queue
                lumberjacks.Enqueue(nextJack);
            }
            else if (nextJackName.Length == 0)
                break;
            else
                Console.WriteLine("Please enter a name that is atleast 2 characters long.");
        }

        // State which lumberjacks is eating and then print which flapjacks they ate off the stack
        while (lumberjacks.Count() != 0)
        {
            Lumberjack currentJack = lumberjacks.Dequeue();
            Console.WriteLine($"\n{currentJack.Name} is eating flapjacks.");
            currentJack.EatFlapjacks();

            // When one is done, move on to the next lumberjack
        }

    }
}