using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class Location
    {
        public Location(string name) => Name = name;
        public string Name { get; set; }
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();
        public override string ToString() => Name;
        public IEnumerable<string> ExitList
        {
            get
            {
                /*
                if (Exits is Dictionary<Direction, Location> tempDictExits)
                {
                    int[] map = new[] { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };

                    var sortedList = tempDictExits
                        .OrderBy(x => map[(int)tempDictExits]);

                    var tempDirectionList = tempDictExits
                        .OrderBy(tempDictExits => tempDictExits.Key)
                        .Select(x => x.Key)
                        .ToList();

                    foreach (Direction direction in tempDirectionList)
                    {
                        if (direction == Exits.Key)
                        {

                        }
                    }
                }
                foreach(object direction in Exits.Keys)
                {
                    returnSequence.Add($" - the {item.Name}")
                }
                */

                List<string> returnSequence = new List<string>();
                foreach (KeyValuePair<Direction, Location> pair in Exits)
                {
                    returnSequence.Add($" - the {pair.Value} is to the {pair.Key}");
                }
                return returnSequence;
                // goal: return IEnumerabe<string> with sorted options by direction
            }
        }
        public void AddExit(Direction direction, Location currentLocation) => Exits.Add(direction, currentLocation);
        public Location GetExit(Direction direction) => Exits[direction];
    }
}
