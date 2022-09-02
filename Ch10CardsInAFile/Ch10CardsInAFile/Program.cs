using System;

enum Value
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

enum Suit
{
    Spades,
    Clubs,
    Hearts,
    Diamonds,
}
class Card
{
    public Card(Value value, Suit suit)
    {
        this.Value = value;
        this.Suit = suit;
    }

    public Value Value { get; private set; }
    public Suit Suit { get; private set; }
    public string Name
    {
        get { return $"{Value} of {Suit}"; }
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

class Deck : List<Card>
{
    public Deck()
    {
        Reset();
    }
    public Deck(string filename)
    {
        StreamReader sr = new StreamReader(filename);
        while (!sr.EndOfStream)
        {
            string? nextCard = sr.ReadLine();
            string[] cardParts = nextCard.Split(new char[] {' '});
            Value value = cardParts[0] switch
            {
                "Ace" => Value.Ace,
                "Two" => Value.Two,
                "Three" => Value.Three,
                "Four" => Value.Four,
                "Five" => Value.Five,
                "Six" => Value.Six,
                "Seven" => Value.Seven,
                "Eight" => Value.Eight,
                "Nine" => Value.Nine,
                "Ten" => Value.Ten,
                "Jack" => Value.Jack,
                "Queen" => Value.Queen,
                "King" => Value.King,
                _ => throw new InvalidDataException($"Unrecognized card value: {cardParts[0]}"),
            };
            Suit suit = cardParts[2] switch
            {
                "Spades" => Suit.Spades,
                "Clubs" => Suit.Clubs,
                "Hearts" => Suit.Hearts,
                "Diamonds" => Suit.Diamonds,
                _ => throw new InvalidDataException($"Unrecognized card suit: {cardParts[2]}"),
            };
            Add(new Card(value, suit));
        }
        sr.Close();
    }

    public static Random random = new Random();
    public Deck Reset()
    {
        Clear();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                Add(new Card((Value)j, (Suit)i));
            }
        }
        return this;
    }
    public Deck Shuffle()
    {
        List<Card> copy = new List<Card>(this);
        Clear();
        while (copy.Count > 0)
        {
            int movingCard = random.Next(copy.Count);
            Add(copy[movingCard]);
            copy.RemoveAt(movingCard);
        }
        return this;
    }
    public Card Deal(int index)
    {
        Card cardToDeal = base[index];
        RemoveAt(index);
        return cardToDeal;
    }
    public Card NewRandomCard() => new Card((Value)random.Next(13), (Suit)random.Next(4));
    public void PrintAllCards()
    {
        foreach (Card card in this)
            Console.WriteLine(card.Name);
    }
    public void WriteCards(string filename)
    {
        using (StreamWriter sr = new StreamWriter(filename))
        {
            foreach (Card card in this)
                sr.WriteLine(card.ToString());
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var filename = "deckofcards.txt";
        Deck deck = new Deck();
        deck.Shuffle();
        for (int i = deck.Count() - 1; i > 9; i--)
            deck.RemoveAt(i);
        deck.WriteCards(filename);

        Deck cardsToRead = new Deck(filename);
        foreach (var card in cardsToRead)
            Console.WriteLine(card.Name);
    }
}
