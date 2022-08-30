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

        public void NextRound()
        {
            gameState.GameStatus = "";

            HumanPlayerPlaysNextRound();

            ComputerPlayersPlayNextRound();

            gameState.GameStatus += $"{Environment.NewLine}";

            foreach (Player player in gameState.Players)
                gameState.GameStatus += $"{Environment.NewLine}{player.Name} has {player.Hand.Count()} card{Player.S(player.Hand.Count())}";

            gameState.GameStatus += $"{Environment.NewLine}{Environment.NewLine}The stock has {gameState.Stock.Count()} card{Player.S(gameState.Stock.Count())}";

            gameState.SelectedCardIndex = -1;

            gameState.UpdateGameState();

            if (gameState.GameOver) RenderFinalStandings();
        }
        public void HumanPlayerPlaysNextRound()
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
                    PlayRound(gameState.HumanPlayer, nextRoundPlayer, nextRoundValue, gameState.Stock);
            }
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
                    Player randomPlayer;
                    while (true)
                    {
                        randomPlayer = gameState.RandomPlayer(opponent);
                        if (randomPlayer.Hand.Count() > 0) break;
                    }
                    PlayRound(opponent, randomPlayer, opponent.RandomValueFromHand(), gameState.Stock);
                }
            }
        }
        public void PlayRound(Player player, Player playerToAsk, Value valuesToAskFor, Deck stock)
        {
            string statusMessage = "";
            if (playerToAsk == gameState.HumanPlayer && valuesToAskFor == Value.Null)
                statusMessage += $"{gameState.HumanPlayer.Name} has no cards and the stock is empty.";
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
            gameState.GameStatus += statusMessage;
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
