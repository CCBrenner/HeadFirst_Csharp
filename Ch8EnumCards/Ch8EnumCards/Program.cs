using System;

class Card
{
    public Card(Values value, Suits suit, string name)
    {
        Value = value;
        Suit = suit;
        Name = name;
    }

    public Values Value { get; set; }
    public Suits Suit { get; set; }
    public string Name
    {
        get { return $"{Value} of {Suit}."; }
    }
}
class Program
{
    public static Random random = new Random();
    public Card card = new Card(random.Next(2, 15), random) // <- left off here
    public static void Main(string[] args)
    {
        

    }
}