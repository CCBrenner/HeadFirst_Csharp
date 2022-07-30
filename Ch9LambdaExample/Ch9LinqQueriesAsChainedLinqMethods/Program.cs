class Program
{
    public static void Main(string[] args)
    {
        int[] values = new int[] { 0, 12, 44, 36, 92, 54, 13, 8 };
        IEnumerable<int> result = values
            .Where(i => i < 37)
            .OrderBy(i => -i)
            .Select(i => i);
        /*
        from v in values
        where v < 37
        orderby -v
        select v;
        */
        foreach(int i in result)
            Console.WriteLine(i);
    }
}
