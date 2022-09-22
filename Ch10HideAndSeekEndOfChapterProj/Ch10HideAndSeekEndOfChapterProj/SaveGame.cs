using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class SaveGame
    {
        // Key = Opponent.Name, Value = LocationWithHidingPlace.HidingPlace
        public Dictionary<string, string> OpponentsInHidingLocations { get; set; }
        public List<Opponent> FoundOpponents { get; set; } 
        public LocationWithHidingPlace CurrentLocation { get; set; }
        public int MoveNumber { get; set; }
        public string Status { get; set; }
    }
}
