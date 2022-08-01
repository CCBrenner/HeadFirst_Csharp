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

class ManualSportSequence : IEnumerable<Sport>
{
    public IEnumerator<Sport> GetEnumerator()
    {
        return new ManualSportEnumerator();
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

    public void Dispose() {  return; }

    public bool MoveNext()
    {
        var maxEnumValue = Enum.GetValues(typeof(Sport)).Length;
        if ((int)current >= maxEnumValue)
            return false;
        current++;
        return true;
    }

    public void Reset() { current = 0; }
}