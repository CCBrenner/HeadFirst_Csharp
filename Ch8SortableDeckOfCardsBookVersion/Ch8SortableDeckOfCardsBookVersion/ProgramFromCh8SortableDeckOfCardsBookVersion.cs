using System;

enum Values
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
}

enum Suits
{
    Spades,
    Clubs,
    Hearts,
    Diamonds,
}

namespace Ch8TwoDecksBlazorWASM
{
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
        public override string ToString()
        {
            return Name;
        }
    }

    class CardComparerByValue : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.Suit > y.Suit)
                return 1;
            else if (x.Suit < y.Suit)
                return -1;
            else if (x.Value > y.Value)
                return 1;
            else if (x.Value < y.Value)
                return -1;
            else
                return 0;
        }
    }
}

/*
class Program
{
    public static Random random = new Random();
    public Card card = new Card((Values)random.Next(2, 15), (Suits)random.Next(4));
    public static void Main(string[] args)
    {
        // Create list of cards (a deck, in order)
        List<Card> cards = new List<Card>();

        // Print shuffled list of cards
        Console.WriteLine("Enter number of random cards: ");
        if (int.TryParse(Console.ReadLine(), out int numCards))
        {
            for (int i = 0; i < numCards; i++)
            {
                cards.Add(RandomCard());
            }
        }
        PrintCards(cards);

        // Print ordered list of cards from same shuffled list
        cards.Sort(new CardComparerByValue());
        Console.WriteLine("\n... sorting the cards ...\n");

        PrintCards(cards);
    }
    static Card RandomCard()
    {
        return new Card((Values)random.Next(13), (Suits)random.Next(4));
    }
    static void PrintCards(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            Console.WriteLine($"List of Cards: {card.Name}");
        }
    }
}
*/