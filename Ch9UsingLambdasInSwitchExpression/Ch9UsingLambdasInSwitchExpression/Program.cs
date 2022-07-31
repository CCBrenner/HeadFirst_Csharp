using System;
using System.Linq;

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
class Card : IComparable<Card>
{
    public Card(Suit suit, Value number)
    {
        this.Suit = suit;
        this.Number = number;
    }
    public Suit Suit { get; private set; }
    public Value Number { get; private set; }
    public string Description { get => $"{Number} of {Suit}"; }
    public int CompareTo(Card comparissonCard)
    {
        if (this.Suit > comparissonCard.Suit)
            return 1;
        else if (this.Suit < comparissonCard.Suit)
            return -1;
        else
        {
            if (this.Number > comparissonCard.Number)
                return 1;
            else if (this.Number < comparissonCard.Number)
                return -1;
            else
                return 0;
        }
    }
}
class Deck
{
    public Deck()
    {
        int index = 0;
        // for CardNumbers
        for (int number = 0; number < 13; number++)
        {
            // for CardSuites
            for (int suit = 0; suit < 4; suit++)
            {
                Cards[index++] = new Card((Suit)suit, (Value)number);
            }
        }
    }
    public Card[] Cards = new Card[52];
    public string PrintDeckOrder()
    {
        string deckOrder = "";
        for (int i = 0; i < Cards.Length; i++)
        {
            deckOrder += ($"\n#{i + 1}: {Cards[i].Description}");
        }
        return deckOrder;
    }
    public void ShuffleCards(int shuffles)
    {
        for (int j = shuffles; j > 0; j--)
        {
            Random random = new Random();
            List<Card> cards = Cards.ToList();
            List<Card> tempCards = new List<Card>();
            for (int i = cards.Count; i > 0; i--)
            {
                int movingCard = random.Next(i);
                tempCards.Add(cards[movingCard]);
                cards.RemoveAt(movingCard);
            }
            Cards = tempCards.ToArray();
        }
    }
    public void SortCards()
    {
        List<Card> tempCards = Cards.ToList();
        tempCards.Sort();
        Cards = tempCards.ToArray();
    }

}

class Program
{
    static string Output(Suit suit, int number) => $"Suit is {suit} and number is {number}.";
    static void Main(string[] args)
    {
        var deck = new Deck();
        var processedCards = deck.Cards
            .Take(3)
            .Concat(deck.Cards.Take(3))
            .OrderByDescending(card => card)
            .Select(card => card.Number switch
            {
                Value.King => Output(card.Suit, 7),
                Value.Ace => $"It's an ace! {card.Suit}",
                Value.Jack => Output((Suit)card.Suit - 1, 9),
                Value.Two => Output(card.Suit, 18),
                _ => card.ToString(),
            });
        foreach (var output in processedCards)
            Console.WriteLine(output);  
    }

}