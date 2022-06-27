using System;
using System.Collections.Generic;

enum KindOfDuck
{
    Mallard,
    Muscovy,
    Loon,
}

class Duck : IComparable<Duck>
{
    public int Size { get; set; }
    public KindOfDuck Kind { get; set; }
    public int CompareTo(Duck duckToCompare)
    {
        if (this.Size > duckToCompare.Size)
        {
            return 1;
        }
        else if (this.Size < duckToCompare.Size)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public override string ToString()
    {
        return $"A {Size} inch {Kind}.";
    }

    enum SortCriteria
{
    SizeThenKind,
    KindThenSize,
}

class DuckComparer : IComparer<Duck>
{
    public SortCriteria SortBy = SortCriteria.SizeThenKind;
    public int Compare(Duck x, Duck y)
    {
        if (SortBy == SortCriteria.KindThenSize)
        {
            if (x.Kind > y.Kind)
                return 1;
            else if (x.Kind < y.Kind)
                return -1;
            else
            {
                if (x.Size > y.Size)
                    return 1;
                else if (x.Size < y.Size)
                    return -1;
                else
                    return 0;
            }
        }
        else
        {
            if (x.Size > y.Size)
                return 1;
            else if (x.Size < y.Size)
                return -1;
            else
            {
                if (x.Kind > y.Kind)
                    return 1;
                else if (x.Kind < y.Kind)
                    return -1;
                else
                    return 0;
            }
        }
    }
}

class DuckComparerBySize : IComparer<Duck>
{
    public int Compare(Duck x, Duck y)
    {
        if (x.Size > y.Size)
            return 1;
        else if (x.Size < y.Size)
            return -1;
        else
            return 0;
    }
}

class DuckComparerByKind : IComparer<Duck>
{
    public int Compare(Duck x, Duck y)
    {
        if (x.Kind > y.Kind)
            return 1;
        else if (x.Kind < y.Kind)
            return -1;
        else
            return 0;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        List<Duck> ducks = new List<Duck>() {
            new Duck() { Kind = KindOfDuck.Mallard, Size = 17 },
            new Duck() { Kind = KindOfDuck.Muscovy, Size = 18 },
            new Duck() { Kind = KindOfDuck.Loon, Size = 14 },
            new Duck() { Kind = KindOfDuck.Muscovy, Size = 11 },
            new Duck() { Kind = KindOfDuck.Mallard, Size = 14 },
            new Duck() { Kind = KindOfDuck.Loon, Size = 13 },
        };


        /* 
        This uses IComparable with CompareTo() method defined in the Duck class
        (can be thought of as the Duck class' default ordering to sort by) 
        */
        // ducks.Sort();  
        /* 
        IComparer object can be passed to List<Duck>.Sort() due to the method being overloaded.
        */
        /*
        IComparer<Duck> ducksBySize = new DuckComparerBySize();
        IComparer<Duck> ducksByKind = new DuckComparerByKind();
        ducks.Sort(ducksByKind);
        PrintDucks(ducks);
        */

        DuckComparer comparer = new DuckComparer();
        Console.WriteLine("Sorting by Size then Kind: ");
        comparer.SortBy = SortCriteria.SizeThenKind;
        ducks.Sort(comparer);
        PrintDucks(ducks);
        Console.WriteLine();
        Console.WriteLine("Sorting by Kind then Size: ");
        comparer.SortBy = SortCriteria.KindThenSize;
        ducks.Sort(comparer);
        PrintDucks(ducks);
        Console.ReadLine();
    }
    public static void PrintDucks(List<Duck> ducks)
    {
        foreach (Duck duck in ducks)
        {
            Console.WriteLine($"{duck.Size} inch {duck.Kind}");
        }
    }
}
}
