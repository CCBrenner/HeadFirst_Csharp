using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterBlazorWASM
{
    public class GameController
    {
        public GameController(string humanPlayerName, int numberOfOpponents)
        {
            Deck shuffledDeck = new Deck();
            gameState = new GameState(humanPlayerName, numberOfOpponents, shuffledDeck.Shuffle());

            GameStatus = $"Starting a new game with players {string.Join(", ", gameState.Players)}";
            UpdateCurrentStandings();

            buttonText = "Ask Player for Cards";
        }
        public static Random Random = new Random();

        public GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }

        public string GameStatus { get; private set; }
        public string CurrentStandings { get; private set; }
        private string buttonText;
        public string ButtonText() => buttonText;
        public int SelectedCard { get; set; }
        public Card ReturnSelectedCard(int i)
        {
            if (i == -1)
                return new Card(Value.Null, Suit.Spades);
            else 
                return HumanPlayer.Hand.Skip(i).First();
        }
        public int SelectedOpponent { get; set; }
        public Player ReturnSelectedOpponent(int i) => Opponents.Skip(i).First();
        public string WhatPlayerIsAskingFor()
        {
            string pluralAndIfSix = ReturnSelectedCard(SelectedCard).Value == Value.Six ? "es" : "s";

            if (ReturnSelectedCard(SelectedCard).Value == Value.Null && HumanPlayer.Hand.Count() == 0)
            {
                if (GameOver)
                {
                    buttonText = "See Game Status";
                    return "See Game Status";
                }
                else
                {
                    buttonText = "Next Round";
                    return "You have no more cards and the deck is empty";
                }
            }
            else if (ReturnSelectedCard(SelectedCard).Value == Value.Null)
            {
                buttonText = "";
                return "Choose a card from your hand";
            }
            else
            { 
                buttonText = "Ask Player for Cards";
                return $"Ask for {ReturnSelectedCard(SelectedCard).Value}{pluralAndIfSix} from {ReturnSelectedOpponent(SelectedOpponent).Name}";
            }
        }
        public void AskPlayerForCards()
        {
            Player nextRoundPlayer;
            Value nextRoundValue;

            if (HumanPlayer.Hand.Count() > 0 || gameState.Stock.Count() > 0)
            {
                nextRoundPlayer = Opponents.Skip(SelectedOpponent).First();
                nextRoundValue = HumanPlayer.Hand.Skip(SelectedCard).First().Value;
            }
            else
            {
                nextRoundPlayer = HumanPlayer;
                nextRoundValue = Value.Null;
            }

            if (ReturnSelectedCard(SelectedCard).Value != Value.Null || HumanPlayer.Hand.Count() == 0)
                NextRound(nextRoundPlayer, nextRoundValue);
        }
        public void NextRound(Player playerToAsk, Value valueToAskFor)
        {
            if(playerToAsk == HumanPlayer && valueToAskFor == Value.Null)
                GameStatus = $"{HumanPlayer.Name} has no cards and the stock is empty.";
            else
                GameStatus = $"{gameState.PlayRound(HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock)}";

            ComputerPlayersPlayNextRound();

            GameStatus += $"{Environment.NewLine}";

            foreach (Player player in gameState.Players)
                GameStatus += $"{Environment.NewLine}{player.Name} has {player.Hand.Count()} card{Player.S(player.Hand.Count())}";

            GameStatus += $"{Environment.NewLine}{Environment.NewLine}The stock has {gameState.Stock.Count()} card{Player.S(gameState.Stock.Count())}";

            UpdateCurrentStandings();

            CheckWinner();

            IfGameOver();

            SelectedCard = -1;
        }
        public void ComputerPlayersPlayNextRound()
        {
            foreach (Player opponent in gameState.Opponents)
            {
                if(opponent.Hand.Count() == 0 && gameState.Stock.Count() == 0)
                    GameStatus += $"{Environment.NewLine}{opponent.Name} has no cards and the stock is empty.";
                else
                {
                    string statusAddition = gameState.PlayRound(opponent, gameState.RandomPlayer(opponent), opponent.RandomValueFromHand(), gameState.Stock);
                    GameStatus += $"{Environment.NewLine}{statusAddition}";
                }
            }
        }
        public void UpdateCurrentStandings()
        {
            CurrentStandings = "";
            foreach(Player player in gameState.Players)
                CurrentStandings += $"{player.Name} has {player.Books.Count()} Book{Player.S(player.Books.Count())}{Environment.NewLine}";
        }
        public void CheckWinner()
        {
            int numCompletedBooks = 0;
            int totalNumOfBooks = 13;
            foreach(Player player in gameState.Players)
                numCompletedBooks += player.Books.Count();
            gameState.GameOver = numCompletedBooks == totalNumOfBooks;
        }
        public void IfGameOver()
        {
            if (gameState.GameOver)
            {
                GameStatus = $"Game Over.{Environment.NewLine}{Environment.NewLine}Final Standings:";
                int position = 1;
                int k = 0;
                for (int booksWon = 13; booksWon >= 0; booksWon--)
                {
                    foreach (Player player in gameState.Players)
                    {
                        if (player.Books.Count() == booksWon)
                        {
                            GameStatus += $"{Environment.NewLine}#{position} with {player.Books.Count()} Book{Player.S(player.Books.Count())}: {player.Name}";
                            k++;
                        }
                    }
                    position += k;
                    k = 0;
                }
                CurrentStandings = "";
            }
        }
        public void NewGame()
        {
            GameStatus = "Starting a new game";
            gameState = new GameState(gameState.HumanPlayer.Name, Opponents.Count(), new Deck());
        }
    }
}
