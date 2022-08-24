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

        public IEnumerable<Card> Hand { get { return hand; } }
        public IEnumerable<Value> Books => books;

        public readonly string Name;

        public static string S(int s) => s == 1 ? "" : "s";

        public string Status => $"{Name} has {hand.Count} card{S(hand.Count)} and {books.Count} book{S(books.Count)}.";

        public override string ToString() => Name;

        public Value RandomValueFromHand() => hand[Random.Next(hand.Count)].Value;

        /// <summary>
        /// If this player has any cards that match the value, return them. If this player runs out of cards, get the next hand from the deck.
        /// </summary>
        /// <param name="value">Value I'm asked for.</param>
        /// <param name="deck">Deck to draw my next hand from.</param>
        /// <returns>The cards that were pulled out my hand (as this player object) that will be given to the asking player.</returns>
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

        public void GetNextHand(Deck stock)
        {
            while ((hand.Count < 5) && (stock.Count() != 0))
                hand.Add(stock.Deal(0));
        }

        /// <summary>
        /// Draws card, puts it into players hand and pulls book from hand if necessary.
        /// </summary>
        /// <param name="stock">Deck to pull a card from.</param>
        public void DrawCard(Deck stock)
        {
            if(stock.Count() > 0)
                AddCardsAndPullOutBooks(new List<Card>() { stock.Deal(0) });
        }

        /// <summary>
        /// If player that is asked for cards has any cards to give, add those cards to hand. Then if hand has any books, remove those cards from hand and add the associated Value to books.
        /// </summary>
        /// <param name="cards"></param>
        public void AddCardsAndPullOutBooks(IEnumerable<Card> cards)
        {
            // Add cards to hand (if any)
            // hand = hand.Concat(cards).ToList();  // The implicit upcasting of hand here to IEnumerable<Card> requires the result of Concat() to be converted to a List<Card> again before assigning to hand
            // Other note: I really don't like how they kept this code which violates the principle of Separation of Concerns;
            // It would be much easier to use if they were two methods instead of one
            hand.AddRange(cards);  // Does the same thing as the line above this one

            // Puts the cards in a state that can be checked by grouping them by value and counting the number of cards in each group
            var potentialBooks = 
                from card in hand
                orderby card.Value
                group card by card.Value into valueGroup
                select valueGroup;

            // Adds Value to books if group of cards by value equals 4 (definition of a book)
            foreach(var potentialBook in potentialBooks)
            {
                if (potentialBook.Count() == 4)
                    books.Add(potentialBook.Key);
            }

            // Remove all cards from hand that have any of the values in books
            foreach(Value value in books)
            {
                hand = hand
                    .Where(card => card.Value != value)
                    .ToList();
            }
        }
    }
}
