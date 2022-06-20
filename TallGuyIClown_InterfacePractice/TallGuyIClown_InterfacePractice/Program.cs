using System;

class TallGuy
{
    public string Name;
    public int Height;

    public void TalkAboutYourself()
    {
        Console.WriteLine($"My name is {Name} and I'm {Height} inches tall.");
    }
}
class Program
{
    static void Main(string[] args)
    {
        TallGuy tallguy = new TallGuy() { Height = 76, Name = "Jimmy" };
        tallguy.TalkAboutYourself();
    }
}
