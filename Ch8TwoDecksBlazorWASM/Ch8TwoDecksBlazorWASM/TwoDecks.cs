namespace Ch8TwoDecksBlazorWASM
{
    public class TwoDecks
    {
        public TwoDecks()
        {
            leftDeck = new Deck();
            rightDeck = new Deck();
            rightDeck.Clear();
        }
        private Deck leftDeck;
        private Deck rightDeck;
        public int LeftDeckCount { get { return leftDeck.Count; } }
        public int RightDeckCount { get { return rightDeck.Count; } }
        public int LeftCardSelected { get; set; }
        public int RightCardSelected { get; set; }
        public string LeftDeckCardName(int i)
        {
            return leftDeck[i].ToString();
        }
        public string RightDeckCardName(int i)
        {
            return rightDeck[i].ToString();
        }
        public void Shuffle()
        {
            leftDeck.Shuffle();
        }
        public void Reset()
        {
            leftDeck = new Deck();
            rightDeck.Clear();
        }
        public void Clear()
        {
            rightDeck.Clear();
        }
        public void Sort()
        {
            rightDeck.Sort();
        }
        public void MoveCard(Direction direction)
        {
            if (direction == Direction.LeftToRight)
            {
                rightDeck.Add(leftDeck[LeftCardSelected]);
                leftDeck.RemoveAt(LeftCardSelected);
            }
            else
            {
                leftDeck.Add(rightDeck[RightCardSelected]);
                rightDeck.RemoveAt(RightCardSelected);
            }
        }
    }
}
