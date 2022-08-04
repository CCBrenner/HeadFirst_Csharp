namespace Ch9GoFishEndOfChapterProj
{
    using System.Collections.Generic;
    public class Deck : List<Card>
    {
        public Deck()
        {
            Reset();
        }

        private Random random = new Random();
        public void Reset()
        {
            Clear();
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Add(new Card((Value)i, (Suit)j));
                }
            }
        }
        public void Shuffle()
        {
            List<Card> copy = new List<Card>(this);
            Clear();
            while (copy.Count > 0)
            {
                int movingCard = random.Next(copy.Count);
                Add(copy[movingCard]);
                copy.RemoveAt(movingCard);
            }
        }
        public Card Deal(int index)
        {
            Card cardToDeal = base[index];  // Here base is refering to the Deck object just like 'this' would
            RemoveAt(index);
            return cardToDeal;
        }
    }
}
