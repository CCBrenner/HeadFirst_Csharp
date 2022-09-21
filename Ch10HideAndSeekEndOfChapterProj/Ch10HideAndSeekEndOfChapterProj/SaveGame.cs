using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class SaveGame
    {
        public SaveGame(LocationWithHidingPlace currentLocation, int moveNumber, string status, List<Opponent> foundOpponents)
        {
            foreach (Location location in House.Locations)
                if ((location as LocationWithHidingPlace).HidingPlace != "")
                    foreach (Opponent opponent in (location as LocationWithHidingPlace).OpponentsHiddenHere)
                        OpponentsInHidingLocations.Add(opponent.Name, location.Name);
            CurrentLocation = new LocationWithHidingPlace(currentLocation.Name, currentLocation.HidingPlace);
            MoveNumber = moveNumber;
            Status = status;
            foreach (Opponent opp in foundOpponents) FoundOpponents.Add(new Opponent(opp.Name));
        }
        public LocationWithHidingPlace CurrentLocation = new LocationWithHidingPlace("", "");
        public int MoveNumber { get; private set; }
        public string Status { get; private set; }
        /// <summary>
        /// Opponents and their hiding locations
        /// </summary>
        /// <key>Opponent</key>
        /// <value>Hiding location</value>
        public Dictionary<string, string> OpponentsInHidingLocations { get; private set; } = new Dictionary<string, string>();
        public List<Opponent> FoundOpponents = new List<Opponent>();
    }
}
