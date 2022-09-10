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
        private string ExitListDirection(Direction d) => d switch
        {
            Direction.Up => "Up",
            Direction.Down => "Down",
            Direction.In => "In",
            Direction.Out => "Out",
            _ => $"to the {d}",
        };
        // this next method violates separation of concerns, but I am following and trusting the textbook
        public void AddExit(Direction direction, Location connectingLocation)
        {
            // adds this location's exit direction and connecting location
            Exits.Add(direction, connectingLocation);
            // adds the connected location's exit direction and this object as it's connecting location
            connectingLocation.Exits.Add((Direction)(-(int)direction), this);
        }
        public Location GetExit(Direction direction) => Exits[direction];
    }
}
