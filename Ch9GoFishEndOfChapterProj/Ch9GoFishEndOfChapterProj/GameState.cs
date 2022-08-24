using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameState
    {
        public GameState(string humanPlayerName, IEnumerable<string> opponentNames, Deck stock)
        {
            List<Player> tempPlayersList = new List<Player>();
            List<Player> tempOpponentsList = new List<Player>();
            Player human = new Player(humanPlayerName);
            foreach (String opponentName in opponentNames) tempOpponentsList.Add(new Player(opponentName));

            tempPlayersList.Add(human);
            tempPlayersList.AddRange(tempOpponentsList);

            foreach (Player player in tempPlayersList) player.GetNextHand(stock);

            Players = tempPlayersList;
            Opponents = tempOpponentsList;
            HumanPlayer = human;
            GameOver = false;
            Stock = stock;
        }

        public readonly IEnumerable<Player> Players;
        public readonly IEnumerable<Player> Opponents;
        public readonly Player HumanPlayer;
        public bool GameOver { get; set; }
        public readonly Deck Stock;

        public Player RandomPlayer(Player currentPlayer) =>
            Players
                .Where(player => player != currentPlayer)
                .Skip(Player.Random.Next(Players.Count() - 1))
                .First();

        public string PlayRound(Player player, Player playerToAsk, Value valuesToAskFor, Deck stock)
        {
            string pluralAndIfSix = valuesToAskFor == Value.Six ? "es" : "s";
            string statusMessage = $"{player} asked {playerToAsk} for {valuesToAskFor}{pluralAndIfSix}{Environment.NewLine}";  // We use Environment.NewLIne instead of \n because of its added support on Macs

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
            return statusMessage;
        }
        public string CheckForWinner()
        {
            int topScore = 0;
            List<Player> winnerPlayers = new List<Player>();
            foreach(Player player in Players)
            {
                if (player.Books.Count() > topScore)
                {
                    topScore = player.Books.Count();
                    winnerPlayers.Clear();
                    winnerPlayers.Add(player);
                }
                else if (player.Books.Count() == topScore)
                    winnerPlayers.Add(player);
            }
            if (winnerPlayers.Count() == 1) return $"The winner is {winnerPlayers.First()}";
            else
            {
                string winners = $"The winners are {winnerPlayers.First()}";
                for (int i = 1; i < winnerPlayers.Count(); i++)
                    winners += $" and {winnerPlayers[i]}";
                return winners;
            }
        }
    }
}
