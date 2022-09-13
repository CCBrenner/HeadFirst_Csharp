using System;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class Opponent
    {
        public Opponent(string name) => Name = name;
        public readonly string Name;
        public override string ToString() => Name;
        public void Hide()
        {
            // at some point calls a LocationWit5hHidingPlace.Hide() method with this Opponent as argument
            throw new NotImplementedException();
        }
    }
}
