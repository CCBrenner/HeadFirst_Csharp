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
        private List<Opponent> opponentsHiddenHere = new List<Opponent>();
        public void Hide(Opponent opponent) => opponentsHiddenHere.Add(opponent);
        public IEnumerable<Opponent> CheckHidingPlace()
        {
            List<Opponent> returnEnumerable = new List<Opponent>();
            foreach (var opponent in opponentsHiddenHere) returnEnumerable.Add(opponent);
            opponentsHiddenHere.Clear();
            return returnEnumerable;
        }
    }
}
