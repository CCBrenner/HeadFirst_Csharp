﻿using System;

class Q
{
    public Q(bool add)
    {
        if (add) Op = "+";
        else Op = "*";
        N1 = R.Next(1, 10);
        N2 = R.Next(1, 10);
    }
    public static Random R = new Random();
    public int N1 { get; private set; }
    public string Op { get; private set; }
    public int N2 { get; private set; }

    public bool Check(int a)
    {
        if (Op == "+") return (a == N1 + N2);
        else return (a == N1 * N2);
    }

}
class Program
{
    public static void Main(string[] args)
    {
        // CH 5, Pool Puzzle (options that can be used once, more than once, or not at all):
        // Generates addition or multiplication problem with
        // ints 1 to 9 inclusive, checks if your int submission is correct, if wrong 
        // keeps asking for the answer until correct, if non-int entered the program quits
        // (Collyn got all answers correct the first time with 1 minor mistype of
        // the constructor paramter type being missing)
        Q q = new Q(Q.R.Next(2) == 1);
        while (true)
        {
            Console.Write($"{q.N1} {q.Op} {q.N2} = ");
            // Anything other than int kills the game
            if (!int.TryParse(Console.ReadLine(), out int i))
            {
                Console.WriteLine("Thanks for playing!");
                return;
            }
            // If player is right
            if (q.Check(i))
            {
                Console.WriteLine("Right!");
                q = new Q(Q.R.Next(2) == 1);
            }
            // If player is wrong
            else Console.WriteLine("Wrong! Try again.");
        }
    }
}
