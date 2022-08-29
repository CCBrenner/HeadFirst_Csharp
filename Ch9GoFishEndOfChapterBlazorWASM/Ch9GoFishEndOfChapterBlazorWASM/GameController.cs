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

            ButtonText = "Ask Player for Cards";
        }
        public static Random Random = new Random();

        public GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } private set { gameState.GameOver = value; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }

        public string GameStatus { get; private set; }
        public string CurrentStandings { get; private set; }
        public string ButtonText { get; private set; }
        public int SelectedCardIndex { get; set; }
        public Card SelectedCard { get { return SelectedCardIndex == -1 ? new Card(Value.Null, Suit.Spades) : HumanPlayer.Hand.Skip(SelectedCardIndex).First(); } }
        public int SelectedOpponentIndex { get; set; }
        public Player SelectedOpponent { get { return Opponents.Skip(SelectedOpponentIndex).First(); } }

        public string WhatPlayerIsAskingFor()
        {
            string pluralAndIfSix = SelectedCard.Value == Value.Six ? "es" : "s";

            if (SelectedCard.Value == Value.Null && HumanPlayer.Hand.Count() == 0)
            {
                if (GameOver)
                {
                    ButtonText = "Click to start a New Game";
                    return "See Game Status";
                }
                else
                {
                    ButtonText = "Next Round";
                    return "You have no more cards and the deck is empty";
                }
            }
            else if (SelectedCard.Value == Value.Null)
            {
                ButtonText = "";
                return "Choose a card from your hand";
            }
            else
            {
                ButtonText = "Ask Player for Cards";
                return $"Ask for {SelectedCard.Value}{pluralAndIfSix} from {SelectedOpponent.Name}";
            }
        }
        public void StartNextTurn()
        {
            if (GameOver) NewGame();
            else
            {
                Player nextRoundPlayer;
                Value nextRoundValue;

                if (HumanPlayer.Hand.Count() > 0 || gameState.Stock.Count() > 0)
                {
                    nextRoundPlayer = SelectedOpponent;
                    nextRoundValue = SelectedCard.Value;
                }
                else
                {
                    nextRoundPlayer = HumanPlayer;
                    nextRoundValue = Value.Null;
                }

                if (SelectedCard.Value != Value.Null || HumanPlayer.Hand.Count() == 0)
                    NextRound(nextRoundPlayer, nextRoundValue);
            }
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

            SelectedCardIndex = -1;
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
            GameOver = numCompletedBooks == totalNumOfBooks;
        }
        public void IfGameOver()
        {
            if (GameOver)
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
            GameStatus = $"Starting a new game with players {string.Join(", ", gameState.Players)}";
            UpdateCurrentStandings();
            gameState = new GameState(HumanPlayer.Name, Opponents.Count(), new Deck());
        }
    }
}
