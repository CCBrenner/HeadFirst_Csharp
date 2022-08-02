using System.Collections;

enum Sport
{
    Football,
    Basketball,
    Baseball,
    Hockey,
    Boxing,
    Rugby,
    Fencing,
}

// Improved on ManulaSportSequence by utilizing yield return statement
class BetterSportSequence : IEnumerable<Sport>
{
    public IEnumerator<Sport> GetEnumerator()
    {
        int maxEnumValue = Enum.GetValues(typeof(Sport)).Length - 1;
        for (int i = 0; i <= maxEnumValue; i++)
        {
            yield return (Sport)i;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class ManualSportEnumerator : IEnumerator<Sport>
{
    int current = -1;
    public Sport Current { get { return (Sport)current; } }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        var maxEnumValue = Enum.GetValues(typeof(Sport)).Length;
        if ((int)current >= maxEnumValue - 1)
            return false;
        current++;
        return true;
    }

    public void Reset() { current = 0; }

    public void Dispose() { return; }
}

class Program
{
    public static void Main(string[] args)
    {
        var sports = new BetterSportSequence();
        foreach (var sport in sports)
            Console.WriteLine(sport);
    }
}