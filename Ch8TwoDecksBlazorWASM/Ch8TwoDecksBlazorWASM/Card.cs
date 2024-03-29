﻿namespace Ch8TwoDecksBlazorWASM
{
    public class Card : IComparable<Card>
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

        public int CompareTo(Card other)
        {
            return new CardComparerByValue().Compare(this, other);
        }
    }
}
