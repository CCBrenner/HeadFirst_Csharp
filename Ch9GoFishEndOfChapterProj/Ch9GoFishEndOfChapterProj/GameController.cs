using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameController
    {
        public static Random Random = new Random();

        public GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }

        public string Status { get; private set; }
        public GameController(string humanPlayerName, IEnumerable<string> computerPLayerNames)
        {
            throw new NotImplementedException();
        }
        public void NextRound(Player playerToAsk, Value valueToAskFor)
        {
            throw new NotImplementedException();
        }
        public void ComputerPlayersPlayNextRound()
        {
            throw new NotImplementedException();
        }
        public void NewGame()
        {
            throw new NotImplementedException();
        }

    }
}
