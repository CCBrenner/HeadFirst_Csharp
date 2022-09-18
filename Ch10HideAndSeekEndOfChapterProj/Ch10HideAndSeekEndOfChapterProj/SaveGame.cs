using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class SaveGame
    {
        public SaveGame()
        {
            throw new NotImplementedException();
        }
        public int MoveNumber { get; private set; }
        public string Status { get; private set; }
        /// <summary>
        /// Opponents and their hiding locations
        /// </summary>
        /// <key>Opponent in hiding</key>
        /// <value>Opponent's hiding location</value>
        public Dictionary<string, string> opponentsInHidingLocations { get; private set; }
        public List<Opponent> foundOpponents { get; private set; }
    }
}
