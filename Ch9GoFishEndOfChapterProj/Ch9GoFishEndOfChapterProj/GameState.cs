using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameState
    {
        public GameState(string humanPlayerName, IEnumerable<string> opponentNames, Deck stock)
        {
            throw new NotImplementedException();
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
