using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class SaveGame
    {
        // Key = Opponent.Name, Value = LocationWithHidingPlace.HidingPlace
        public Dictionary<string, string> OpponentsInHidingLocations { get; set; }
        public List<string> FoundOpponents { get; set; } 
        public string CurrentLocationName { get; set; }
        public int MoveNumber { get; set; }
        public string Status { get; set; }
    }
}
