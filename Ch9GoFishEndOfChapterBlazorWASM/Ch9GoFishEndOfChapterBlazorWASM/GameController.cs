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
        }
        public static Random Random = new Random();

        public GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }

        public string GameStatus { get; private set; }
        public string CurrentStandings { get; private set; }
        public int SelectedCard { get; set; }
        public Card ReturnSelectedCard(int i) => HumanPlayer.Hand.Skip(i).First();
        public int SelectedOpponent { get; set; }
        public Player ReturnSelectedOpponent(int i) => Opponents.Skip(i).First();
        public string WhatPlayerIsAskingFor()
        {
            string pluralAndIfSix = ReturnSelectedCard(SelectedCard).Value == Value.Six ? "es" : "s";
            return $"Ask for {ReturnSelectedCard(SelectedCard).Value}{pluralAndIfSix} from {ReturnSelectedOpponent(SelectedOpponent).Name}";
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

            if (gameState.GameOver)
            {
                GameStatus = $"Game Over.{Environment.NewLine}{Environment.NewLine}Final Standings:";
                int position = 1;
                int k = 0;
                for(int booksWon = 13; booksWon < gameState.Players.Count(); booksWon--)
                {
                    foreach(Player player in gameState.Players)
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
            }
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

        public void NewGame()
        {
            GameStatus = "Starting a new game";
            gameState = new GameState(gameState.HumanPlayer.Name, Opponents.Count(), new Deck());
        }
    }
}
