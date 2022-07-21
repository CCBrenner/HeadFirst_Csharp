namespace Ch8TwoDecksBlazorWASM
{
    public class Card
    {
        public Card(Values value, Suits suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public Values Value { get; private set; }
        public Suits Suit { get; private set; }
        public string Name
        {
            get { return $"{Value} of {Suit}."; }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
