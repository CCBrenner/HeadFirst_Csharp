namespace Ch8TwoDecksBlazorWASM
{
    public class TwoDecks
    {
        private Deck leftDeck = new Deck();
        private Deck rightDeck = new Deck();
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
            rightDeck = new Deck();
        }
        public void Clear()
        {

        }
        public void Sort()
        {

        }
    }
}
