enum myEnum
{
    bertram,
    derkaloid,
    fantizmo,
    happilnap,
    zilch,
}

class Card : IComparable<Card>
{
    public Card(List<myEnum> flozzle)
    {
        Jibberish = flozzle;
    }
    public List<myEnum> Jibberish;

    public int CompareTo(Card otherCard)
    {
        if (this.Jibberish > otherCard.Jibberish)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}

class Deck : List<myEnum>
{
    public Deck(List<Card> one)
    {
        Stock = one;
    }

    public readonly List<Card> Stock;
    
}

class Program
{
    public static void Main(string[] args)
    {
        List<myEnum> one = new List<myEnum>()
        {
            myEnum.fantizmo,
            myEnum.bertram,
            myEnum.derkaloid,
            myEnum.happilnap,
            myEnum.zilch,
        };
        Deck myDeck = new Deck(one);

    }
}