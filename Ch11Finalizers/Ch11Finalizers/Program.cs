using System;
using System.Diagnostics;
class EvilClone
{
    public static int CloneCount = 0;
    public int CloneID { get; } = ++CloneCount;
    public EvilClone() => Console.WriteLine("Clone #{0} is wreaking havoc.", CloneID);
    ~EvilClone()
    {
        Console.WriteLine("Clone #{0} destroyed", CloneID);
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<EvilClone> clones = new List<EvilClone>();
        while (true)
        {
            switch (Console.ReadKey().KeyChar)
            {
                case 'a':
                    clones.Add(new EvilClone());
                    break;
                case 'c':
                    Console.WriteLine("Clearing list at time {0}", stopwatch.ElapsedMilliseconds);
                    break;
                case 'g':
                    Console.WriteLine("Collecting at time {0}", stopwatch.ElapsedMiliseconds);
            }
        }
    }
}
