using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class LocationWithHidingPlace : Location
    {
        public LocationWithHidingPlace(string name, string hidingPlace) : base(name)
        {
            HidingPlace = hidingPlace;
        }
        public string HidingPlace { get; private set; }
        public List<Opponent> OpponentsHiddenHere = new List<Opponent>();
        public void Hide(Opponent opponent) => OpponentsHiddenHere.Add(opponent);
        public IEnumerable<Opponent> CheckHidingPlace()
        {
            List<Opponent> returnEnumerable = new List<Opponent>();
            foreach (var opponent in OpponentsHiddenHere) returnEnumerable.Add(opponent);
            OpponentsHiddenHere.Clear();
            return returnEnumerable;
        }
    }
}
