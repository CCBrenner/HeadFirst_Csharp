using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        // This used to be a sequence of Comics. Now it is a sequence of strings.
        IEnumerable<string> mostExpensive =
            from comic in Comic.Catelog
            where Comic.Prices[comic.Issue] > 500
            // orderby -Comic.Prices[comic.Issue]
            orderby Comic.Prices[comic.Issue] descending  // 'descending' keyword replaces the minus sign here so that the claus is more readable/understandable
            select $"{comic} is worth {Comic.Prices[comic.Issue]:c}";

        foreach (string comic in mostExpensive)
            Console.WriteLine(comic);
    }
}
