using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch9GroupByLinqClauses
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
            return this;  // returning the shuffled Deck object permits method chaining with this method
        }
        public Card Deal(int index)
        {
            Card cardToDeal = base[index];  // Here base is refering to the Deck object just like 'this' would
            RemoveAt(index);
            return cardToDeal;
        }
    }
}
