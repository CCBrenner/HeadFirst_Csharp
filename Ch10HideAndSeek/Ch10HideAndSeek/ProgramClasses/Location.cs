using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ch10HideAndSeek
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
        public void AddExit(Direction direction, Location connectingLocation) =>
            Exits.Add(direction, connectingLocation);
        private void AddConnectingLocationExit(Direction direction, Location connectingLocation) =>
            connectingLocation.Exits.Add((Direction)(-(int)direction), this);
        public void AddExitsOfConnectedLocations(Direction direction, Location connectingLocation)
        {
            AddExit(direction, connectingLocation);
            AddConnectingLocationExit(direction, connectingLocation);
        }
        public Location GetExit(Direction d) => Exits[d];
    }
}
