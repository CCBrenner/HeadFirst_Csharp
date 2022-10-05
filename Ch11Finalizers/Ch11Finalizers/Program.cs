using System;
using System.Diagnostics;

// This program demonstrates how finalizer works for objects that are garbage collected
// Here we are also forcing garbage collection when the user presses 'g'

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
                    clones.Clear();
                    break;
                case 'g':
                    Console.WriteLine("Collecting at time {0}", stopwatch.ElapsedMilliseconds);
                    GC.Collect();
                    break;
            }
        }
    }
}
