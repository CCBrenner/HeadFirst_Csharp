using System.Collections;

class PowerOfTwo : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 2; i > 0; i = (int)Math.Pow(i, 2))
        {
            yield return i;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var sequence = new PowerOfTwo();
        foreach (var element in sequence)
        {
            Console.Write($"{element} ");
        };
        Console.WriteLine($"\n\n{Math.Round(Math.Log(int.MaxValue, 2))}");
    }
}