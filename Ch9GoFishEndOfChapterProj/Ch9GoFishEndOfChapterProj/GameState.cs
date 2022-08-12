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
            // Initialize
            string pluralAndIfSix = valuesToAskFor == Value.Six ? "es" : "s";
            string beginningStatement = $"{player} asked {playerToAsk} for {valuesToAskFor}{pluralAndIfSix}";

            var matchingCards = playerToAsk.DoYouHaveAny(valuesToAskFor, stock);
            if(matchingCards.Count() == 0)
                player.DrawCard(stock);
            else
                player.AddCardsAndPullOutBooks(matchingCards);

            // If no matches and Deck is empty
            if ((matchingCards.Count() == 0) || (matchingCards.Count() == 0) && (stock.Count() == 0))
                return $"{beginningStatement}" + Environment.NewLine + $"The stock is out of cards";

            string s = matchingCards.Count() == 1 ? "" : "s";
            string numOfMatchingCards = matchingCards.Count().ToString();

            return $"{beginningStatement}" + Environment.NewLine + $"{playerToAsk} has {numOfMatchingCards} {valuesToAskFor} card{s}"; 
        }
        public string CheckForWinner()
        {
            throw new NotImplementedException();
        }
    }
}
