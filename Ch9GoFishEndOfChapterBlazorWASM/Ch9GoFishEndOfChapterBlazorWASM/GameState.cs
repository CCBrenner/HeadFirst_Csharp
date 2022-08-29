using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterBlazorWASM
{
    public class GameState
    {
        public GameState(string humanPlayerName, int numberOfOpponents, Deck stock)
        {
            List<Player> tempPlayersList = new List<Player>();
            List<Player> tempOpponentsList = new List<Player>();
            Player human = new Player(humanPlayerName);
            for(int i = 0; i < numberOfOpponents; i++)
                tempOpponentsList.Add(new Player($"Computer {i+1}"));

            tempPlayersList.Add(human);
            tempPlayersList.AddRange(tempOpponentsList);

            foreach (Player player in tempPlayersList) player.GetNextHand(stock);

            Stock = stock;
            HumanPlayer = human;
            Players = tempPlayersList;
            Opponents = tempOpponentsList;
            GameOver = false;
            GameStatus = $"Starting a new game with players {string.Join(", ", Players)}";
            SelectedCardIndex = -1;
        }

        public readonly Deck Stock;
        public readonly Player HumanPlayer;
        public readonly IEnumerable<Player> Players;
        public readonly IEnumerable<Player> Opponents;

        public bool GameOver { get; set; }

        public string GameStatus { get; set; }
        public string CurrentStandings { get; set; }
        public string AskStatus { get; set; }
        public string ButtonText { get; set; }

        private int selectedCardIndex;
        public int SelectedCardIndex
        {
            get { return selectedCardIndex; }
            set
            {
                selectedCardIndex = value;
                UpdateGameState();
            }
        }
        public Card SelectedCard { get { return SelectedCardIndex == -1 ? new Card(Value.Null, Suit.Spades) : HumanPlayer.Hand.Skip(SelectedCardIndex).First(); } }
        public int SelectedOpponentIndex { get; set; }
        public Player SelectedOpponent { get { return Opponents.Skip(SelectedOpponentIndex).First(); } }

        public Player RandomPlayer(Player currentPlayer) =>
            Players
                .Where(player => player != currentPlayer)
                .Skip(Player.Random.Next(Players.Count() - 1))
                .First();

        public void PlayRound(Player player, Player playerToAsk, Value valuesToAskFor, Deck stock)
        {
            string statusMessage = "";
            if (playerToAsk == HumanPlayer && valuesToAskFor == Value.Null)
                statusMessage += $"{HumanPlayer.Name} has no cards and the stock is empty.";
            else
            {
                statusMessage = $"{player} asked {playerToAsk} for {valuesToAskFor}{Card.PluralAndIfSix(valuesToAskFor)}{Environment.NewLine}";

                var matchingCards = playerToAsk.DoYouHaveAny(valuesToAskFor, stock);

                if (matchingCards.Count() > 0)
                {
                    player.AddCardsAndPullOutBooks(matchingCards);
                    statusMessage += $"{playerToAsk} has {matchingCards.Count()} {valuesToAskFor} card{Player.S(matchingCards.Count())}";
                }
                else if (stock.Count == 0)
                    statusMessage += $"The stock is out of cards";
                else
                {
                    player.DrawCard(stock);
                    statusMessage += $"{player} drew a card";
                }

                if (player.Hand.Count() == 0)
                {
                    player.GetNextHand(stock);
                    statusMessage += $"{Environment.NewLine}{player} ran out of cards" +
                        $"{Environment.NewLine}{player} drew {player.Hand.Count()} " +
                        $"card{Player.S(player.Hand.Count())} from the stock";
                    if (stock.Count == 0)
                        statusMessage += $"{Environment.NewLine}The stock is out of cards";
                }
            }
            GameStatus += statusMessage;
        }
        public void UpdateGameState()
        {
            UpdateGameOver();
            UpdateCurrentStandings();
            UpdateAskStatusAndButtontext();
        }
        private void UpdateGameOver()
        {
            int numCompletedBooks = 0;
            int totalNumOfBooks = 13;
            foreach (Player player in Players)
                numCompletedBooks += player.Books.Count();
            GameOver = numCompletedBooks == totalNumOfBooks;
        }
        private void UpdateCurrentStandings()
        {
            CurrentStandings = "";
            foreach (Player player in Players)
                CurrentStandings += $"{player.Name} has {player.Books.Count()} Book{Player.S(player.Books.Count())}{Environment.NewLine}";
        }
        private void UpdateAskStatusAndButtontext()
        {
            if (SelectedCard.Value == Value.Null && HumanPlayer.Hand.Count() == 0 && GameOver)
            {
                AskStatus = "See Game Status";
                ButtonText = "Click to start a New Game";
            }
            else if (SelectedCard.Value == Value.Null && HumanPlayer.Hand.Count() == 0)
            {
                AskStatus = "You have no more cards and the deck is empty";
                ButtonText = "Next Round";
            }
            else if (SelectedCard.Value == Value.Null)
            {
                AskStatus = "Choose a card from your hand";
                ButtonText = "";
            }
            else
            {
                AskStatus = $"Ask for {SelectedCard.Value}{Card.PluralAndIfSix(SelectedCard.Value)} from {SelectedOpponent.Name}";
                ButtonText = "Ask Player for Cards";
            }
        }
    }
}
