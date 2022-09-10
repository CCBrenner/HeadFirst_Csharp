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
        public IEnumerable<string> ExitList =>
            Exits
            .OrderBy(keyValuePair => (int)keyValuePair.Key)
            .OrderBy(keyValuePair => Math.Abs((int)keyValuePair.Key))
            .Select(keyValuePair => $" - the {keyValuePair.Value} is {ExitListDirection(keyValuePair.Key)}");
        public string ExitListDirection(Direction d) => d switch
        {
            Direction.Up => "Up",
            Direction.Down => "Down",
            Direction.In => "In",
            Direction.Out => "Out",
            _ => $"to the {d}",
        };
        public void AddExit(Direction direction, Location currentLocation) => Exits.Add(direction, currentLocation);
        public Location GetExit(Direction direction) => Exits[direction];
    }
}
