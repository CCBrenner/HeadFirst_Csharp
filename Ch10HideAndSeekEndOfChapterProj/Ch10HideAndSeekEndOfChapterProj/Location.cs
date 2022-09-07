using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class Location
    {
        public Location(string name) => Name = name;
        public string Name { get; set; }
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();
        public override string ToString() => Name;
        public IEnumerable<string> ExitList => throw new NotImplementedException();
        public void AddExit(Direction direction, Location currentLocation) => Exits.Add(direction, currentLocation);
        public Location GetExit(Direction direction) => Exits[direction];
    }
}
