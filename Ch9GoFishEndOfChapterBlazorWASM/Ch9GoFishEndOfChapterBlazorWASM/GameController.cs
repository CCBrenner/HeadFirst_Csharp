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
        public int SelectedOpponent { get; set; }
        public void NextRound(Player playerToAsk, Value valueToAskFor)
        {
            if(playerToAsk == HumanPlayer && valueToAskFor == Value.Null)
                GameStatus = $"{HumanPlayer.Name} has no cards and the stock is empty.";
            else
                GameStatus = $"{gameState.PlayRound(HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock)}";

            ComputerPlayersPlayNextRound();

            Console.WriteLine($"{Environment.NewLine}");

            foreach (Player player in gameState.Players)
                GameStatus += $"{Environment.NewLine}{player.Name} has {player.Hand.Count()} card{Player.S(player.Hand.Count())}";

            GameStatus += $"{Environment.NewLine}The stock has {gameState.Stock.Count()} card{Player.S(gameState.Stock.Count())}";

            UpdateCurrentStandings();
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
        public void NewGame()
        {
            GameStatus = "Starting a new game";
            gameState = new GameState(gameState.HumanPlayer.Name, Opponents.Count(), new Deck());
        }
    }
}
