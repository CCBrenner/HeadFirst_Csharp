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
        }

        public static Random Random = new Random();
        public GameState gameState;

        public void StartNextRound()
        {
            if (gameState.GameOver) NewGame();
            else
            {
                Player nextRoundPlayer;
                Value nextRoundValue;

                if (gameState.HumanPlayer.Hand.Count() > 0 || gameState.Stock.Count() > 0)
                {
                    nextRoundPlayer = gameState.SelectedOpponent;
                    nextRoundValue = gameState.SelectedCard.Value;
                }
                else
                {
                    nextRoundPlayer = gameState.HumanPlayer;
                    nextRoundValue = Value.Null;
                }

                if (gameState.SelectedCard.Value != Value.Null || gameState.HumanPlayer.Hand.Count() == 0)
                    NextRound(nextRoundPlayer, nextRoundValue);
            }
        }
        public void NextRound(Player playerToAsk, Value valueToAskFor)
        {
            gameState.GameStatus = "";

            gameState.PlayRound(gameState.HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock);

            ComputerPlayersPlayNextRound();

            gameState.GameStatus += $"{Environment.NewLine}";

            foreach (Player player in gameState.Players)
                gameState.GameStatus += $"{Environment.NewLine}{player.Name} has {player.Hand.Count()} card{Player.S(player.Hand.Count())}";

            gameState.GameStatus += $"{Environment.NewLine}{Environment.NewLine}The stock has {gameState.Stock.Count()} card{Player.S(gameState.Stock.Count())}";

            gameState.SelectedCardIndex = -1;

            gameState.UpdateGameState();

            if (gameState.GameOver) RenderFinalStandings();
        }
        public void ComputerPlayersPlayNextRound()
        {
            foreach (Player opponent in gameState.Opponents)
            {
                if(opponent.Hand.Count() == 0 && gameState.Stock.Count() == 0)
                    gameState.GameStatus += $"{Environment.NewLine}{opponent.Name} has no cards and the stock is empty.";
                else
                {
                    gameState.GameStatus += $"{Environment.NewLine}";
                    gameState.PlayRound(opponent, gameState.RandomPlayer(opponent), opponent.RandomValueFromHand(), gameState.Stock);
                }
            }
        }
        public void RenderFinalStandings()
        {
            gameState.GameStatus = $"Game Over.{Environment.NewLine}{Environment.NewLine}Final Standings:";
            int position = 1;
            int k = 0;
            for (int booksWon = 13; booksWon >= 0; booksWon--)
            {
                foreach (Player player in gameState.Players)
                {
                    if (player.Books.Count() == booksWon)
                    {
                        gameState.GameStatus += $"{Environment.NewLine}#{position} with {player.Books.Count()} Book{Player.S(player.Books.Count())}: {player.Name}";
                        k++;
                    }
                }
                position += k;
                k = 0;
            }
            gameState.CurrentStandings = "";
        }
        public void NewGame()
        {
            gameState = new GameState(gameState.HumanPlayer.Name, gameState.Opponents.Count(), new Deck());
            gameState.GameStatus = $"Starting a new game with players {string.Join(", ", gameState.Players)}";
            gameState.UpdateGameState();
        }
    }
}
