using System.Collections.Generic;

namespace Ch8TwoDecksBlazorWASM
{
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
            while (copy.Count > 0)
            {
                Clear();
                int movingCard = random.Next(copy.Count);
                Add(copy[movingCard]);
                copy.RemoveAt(movingCard);
            }
        }
        public void Deal(int index)
        {
            // stoped here
        }
    }
}
