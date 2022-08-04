using System;
namespace Ch9GoFishEndOfChapterProj
{
    public class Book
    {
        public List<Card> Cards;
        public Book(Card card1, Card card2, Card card3, Card card4)
        {
            Cards = new List<Card>()
            {
                card1,
                card2,
                card3,
                card4,      
            };
        }
    }
}

