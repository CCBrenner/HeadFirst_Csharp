using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class LocationWithHidingPlace : Location
    {
        public LocationWithHidingPlace(string name, string hidingPlace) : base(name)
        {
            throw new NotImplementedException();
        }
        public string HidingPlace => throw new NotImplementedException();
        public void Hide(Opponent opponent)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Opponent> CheckHidingPlace()
        {
            throw new NotImplementedException();
        }
    }
}
