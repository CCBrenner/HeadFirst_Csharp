using System;

interface IClown
{
    protected static Random random = new Random();
    string FunnyThingIHave { get; }
    void Honk();
}

interface IScaryClown : IClown
{
    string ScaryThingIHave { get; }
    void ScareLittleChildren();
}

class FunnyFunny : IClown
{
    public FunnyFunny(string funnyThing)
    {
        funnyThingIHave = funnyThing;
    }
    private string funnyThingIHave;
    public string FunnyThingIHave { get { return funnyThingIHave; } }

    public void Honk()
    {
        Console.WriteLine($"Hi kids, I have a {funnyThingIHave}.");
    }
}

class ScaryScary : FunnyFunny, IScaryClown
{
    public ScaryScary(string funnyThing, int scaryThingCount) : base(funnyThing)
    {
        ScaryThingIHave = $"{scaryThingCount} spiders";
    }
    public string ScaryThingIHave { get; set; }

    public void ScareLittleChildren()
    {
        Console.WriteLine($"Boo! Gotcha! Look at my {ScaryThingIHave}");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        IClown fingersTheClown = new ScaryScary("big red nose", 14);
        fingersTheClown.Honk();
        if (fingersTheClown is IScaryClown iScaryClownReference)
        {
            iScaryClownReference.ScareLittleChildren();
        }
    }
}
