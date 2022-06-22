using System;

interface IClown
{
    protected static Random random = new Random();
    string FunnyThingIHave { get; }
    private static int carCapacity = 12;
    public static int CarCapacity
    {
        get { return carCapacity; }
        set
        {
            if (value > 10) carCapacity = value;
            else Console.Error.WriteLine($"Warning: Car capacity {value} is too small.");
        }
    }
    void Honk();
    static string ClownCarDescription()
    {
        return $"A clown car with {random.Next(CarCapacity / 2, CarCapacity)} clowns.";
    }
}

interface IScaryClown : IClown
{
    string ScaryThingIHave { get; }
    void ScareLittleChildren();
     void ScareAdults()
    {
        Console.WriteLine($@"I am an ancient evil that will haunt your dreams. 
Behold my terrifying necklace with {random.Next(4, 10)} of my last victim's fingers.

Oh, also, before I forget...");
        ScareLittleChildren();
    }
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
        IClown.CarCapacity = 18;
        Console.WriteLine(IClown.ClownCarDescription());
        IClown fingersTheClown = new ScaryScary("big red nose", 14);
        fingersTheClown.Honk();
        if (fingersTheClown is IScaryClown iScaryClownReference)
        {
            iScaryClownReference.ScareAdults();
        }
    }
}
