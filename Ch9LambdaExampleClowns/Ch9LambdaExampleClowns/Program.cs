interface IClown
{
    string FunnyThingIHave { get; }
    void Honk();
}

class TallGuy : IClown
{
    public string Name;
    public int Height;

    public string FunnyThingIHave => "big red shoes";

    public void Honk() => Console.WriteLine("Honk honk!");

    public void TalkAboutYourself()
    {
        Console.WriteLine($"My name is {Name} and I am {Height} inches tall.");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        TallGuy tallGuy = new TallGuy() { Name = "Jimmy", Height = 76 };
        tallGuy.TalkAboutYourself();
        Console.WriteLine($"The tall guy has {tallGuy.FunnyThingIHave}.");
        tallGuy.Honk();
    }
}
/* 
// Example of how you can use lambdas in place of Func<int,int> 
class Program
{
    public static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4 };
        IEnumerable<int>? result = array.Select( i => i * 2 );
        foreach(int i in result)
            Console.WriteLine(i);
    }
}
*/
