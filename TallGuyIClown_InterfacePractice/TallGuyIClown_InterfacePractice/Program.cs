using System;
using TallGuyIClown_InterfacePractice;


class TallGuy : IClown
{
    public string Name;
    public int Height;
    public string FunnyThingIHave { 
        get { return "big shoes"; }
    }

    public void TalkAboutYourself()
    {
        Console.WriteLine($"My name is {Name} and I'm {Height} inches tall.");
    }
    public void Honk()
    {
        Console.WriteLine("Honk honk!");
    }
}
class Program
{
    static void Main(string[] args)
    {
        // since not using constructor, omitting height and name will still compile
        TallGuy tallguy = new TallGuy() { Height = 76, Name = "Jimmy" };
        tallguy.TalkAboutYourself();
        Console.WriteLine($"The tall guy has {tallguy.FunnyThingIHave}.");
    }
}
