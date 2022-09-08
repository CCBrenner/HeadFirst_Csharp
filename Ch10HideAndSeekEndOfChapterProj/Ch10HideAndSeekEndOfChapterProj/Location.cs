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
                List<string> returnSequence = new List<string>();
                var tempExits = Exits
                    .OrderBy(x => x.Key)  // have this take the negative values, abs() them, and subtract 0.5 from them, then sort the order
                    .ToList();
                foreach (KeyValuePair<Direction, Location> pair in tempExits)
                    returnSequence.Add($" - the {pair.Value} is to the {pair.Key}");
                return returnSequence;
                // goal: return IEnumerabe<string> with sorted options by direction
            }
        }
        public void AddExit(Direction direction, Location currentLocation) => Exits.Add(direction, currentLocation);
        public Location GetExit(Direction direction) => Exits[direction];
    }
}
