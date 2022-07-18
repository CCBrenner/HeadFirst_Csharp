/*
 * Prompt: "Build a console app that creates a list of cards in random order, 
 * prints them to the console, 
 * uses a comparer object to sort the cards, 
 * and then prints the sorted list."
 */

using System;
using System.Collections.Generic;

enum CardSuit
{
    Spades = 0,
    Clubs = 1,
    Hearts = 2,
    Diamonds = 3,
}

enum CardNumber
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

class Card : IComparable<Card>
{
    public Card(CardSuit suit, CardNumber number)
    {
        this.Suit = suit;
        this.Number = number;
    }
    public CardSuit Suit { get; private set; }
    public CardNumber Number { get; private set; }
    public string Description
    {
        get { return $"{Number} of {Suit}";  }
    }

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
        for(int number = 0; number < 13; number++)
        {
            // for CardSuites
            for(int suit = 0; suit < 4; suit++)
            {
                Cards[index++] = new Card((CardSuit)suit, (CardNumber)number);
            }
        }
    }
    public Card[] Cards = new Card[52];
    public string PrintDeckOrder()
    {
        string deckOrder = "";
        for(int i = 0; i < Cards.Length; i++)
        {
            deckOrder += ($"\n#{i+1}: {Cards[i].Description}");
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
    public static void Main(string[] args)
    {
        // First, print the deck in a randomized order:
        Deck deck = new Deck();
        deck.ShuffleCards(3);
        Console.WriteLine($"Shuffled Deck of Cards:{deck.PrintDeckOrder()}");

        // Second, print the deck in a sorted order
        deck.SortCards();
        Console.WriteLine($"\nSame Deck of Cards, only Sorted: {deck.PrintDeckOrder()}");
    }
}