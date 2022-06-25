using System;

class Card
{
    public Card(Values value, Suits suit)
    {
        this.Value = value;
        this.Suit = suit;
    }

    public Values Value { get; private set; }
    public Suits Suit { get; private set; }
    public string Name
    {
        get { return $"{Value} of {Suit}."; }
    }
}
class Program
{
    public static Random random = new Random();
    public Card card = new Card((Values)random.Next(2, 15), (Suits)random.Next(4));
    public static void Main(string[] args)
    {
        

    }
}