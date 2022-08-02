class Program
{
    static IEnumerable<string> SimpleEnumerable()
    {
        yield return "apples";
        yield return "bananas";
        yield return "oranges";
        yield return "unicorns";
    }

    static void Main(string[] args)
    {
        foreach (var item in SimpleEnumerable()) { Console.WriteLine(item); }
    }
}
