using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch9GoFishEndOfChapterProj
{
    public class Player
    {
        public Player(string name) => Name = name;

        public Player(string name, IEnumerable<Card> cards)
        {
            Name = name;
            hand.AddRange(cards);
        }

        public static Random Random = new Random();
        private List<Card> hand = new List<Card>();
        private List<Value> books = new List<Value>();

        public IEnumerable<Card> Hand => hand;
        public IEnumerable<Value> Books => books;

        public readonly string Name;

        public static string S(int s) => s == 1 ? "" : "s";

        public string Status => $"{Name} has {hand.Count} card{S(hand.Count)} and {books.Count} book{S(books.Count)}.";

        public void GetNextHand(Deck stock)
        {
            while(hand.Count < 5)
            {
                if (stock.Count() == 0) break;
                DrawCard(stock);
            }
        }
        public IEnumerable<Card> DoYouHaveAny(Value value, Deck deck)
        {
            // Pull and assign to temp var for return
            var matchingCards = hand
                .Where(card => card.Value == value)
                .OrderBy(card => card.Suit);

            // Delete pulled and assigned matching cards
            hand = hand
                .Where(card => card.Value != value)
                .ToList();

            // If no cards, draw a new hand
            if (hand.Count() == 0) GetNextHand(deck);

            return matchingCards;
        }
        public void AddCardsAndPullOutBooks(IEnumerable<Card> cards)
        {
            hand = hand.Concat(cards).ToList();
            var potentialBooks = 
                from card in hand
                orderby card.Value
                group card by card.Value into valueGroup
                select valueGroup;

            foreach (var potentialBook in potentialBooks)
            {
                if (potentialBook.Count() == 4)
                {
                    books.Add(potentialBook.Key);
                    /*foreach(Card card in hand)
                    {
                        if(card.Value == potentialBook.Key)
                            hand.Remove(card);  // Destruction of card happens here because book was created by adding the value to player's books
                    }*/
                }
            }
            foreach(Value value in books)
            {
                hand = hand
                    .Where(card => card.Value != value)
                    .ToList();
            }
            
        }
        public void DrawCard(Deck stock)
        {
            hand.Add(stock.Deal(0));
        }
        public Value RandomValueFromHand() => hand[Random.Next(hand.Count)].Value;
        public override string ToString() => Name;
    }
}
