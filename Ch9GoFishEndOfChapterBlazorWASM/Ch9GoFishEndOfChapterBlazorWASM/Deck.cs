namespace Ch9GoFishEndOfChapterBlazorWASM
{
    using System.Collections.Generic;
    public class Deck : List<Card>
    {
        public Deck()
        {
            Reset();
        }

        private Random random = Player.Random;
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
            Card cardToDeal = base[index];  // Here base is refering to the Deck object just like 'this' would
            RemoveAt(index);
            return cardToDeal;
        }
        public Card GenerateNullCard()
        {
            return new Card(Value.Null, Suit.Clubs);
        }
    }
}
