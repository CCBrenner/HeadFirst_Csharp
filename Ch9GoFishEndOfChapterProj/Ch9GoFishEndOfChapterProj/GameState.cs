using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameState
    {
        public GameState(string humanPlayerName, IEnumerable<string> opponentNames, Deck stock)
        {
            HumanPlayer = new Player(humanPlayerName);
            List<Player> tempOpponentsList = new List<Player>();

            foreach(String opponentName in opponentNames)
                tempOpponentsList.Add(new Player(opponentName));

            Opponents = tempOpponentsList;

            List<Player> tempPlayersList = new List<Player>();
            tempPlayersList.Add(HumanPlayer);
            tempPlayersList.AddRange(tempOpponentsList);

            foreach(Player player in tempPlayersList)
                player.GetNextHand(stock);

            Players = tempPlayersList;

            HumanPlayer.GetNextHand(stock);

            Stock = stock;
        }
        public readonly IEnumerable<Player> Players;
        public readonly IEnumerable<Player> Opponents;
        public readonly Player HumanPlayer;
        public bool GameOver { get; set; }
        public readonly Deck Stock;

        public Player RandomPlayer(Player currentPlayer) => throw new NotImplementedException();
        public string PlayRound(Player player, Player playerToAsk, Value valuesToAskFor, Deck stock)
        {
            throw new NotImplementedException();
        }
        public string CheckForWinner()
        {
            throw new NotImplementedException();
        }
    }
}
