class Program
{
    public static void Main(string[] args)
    {
        int[] values = new int[] { 0, 12, 44, 36, 92, 54, 13, 8 };
        IEnumerable<int> result = values
            .Where(i => i < 37)
            .OrderByDescending(i => i);
        // Could use .OrederBy(i => -i) instead as well
        /*
        from v in values
        where v < 37
        orderby -v
        select v;
        */
        foreach(int i in result)
            Console.WriteLine(i);

        /* SImilar thing is possible for groupby:
            var grouped =
            deck.GroupBy(card => card.Suit)
            .OrderByDescending(group => group.Key);
         */
    }
}
