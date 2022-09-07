using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class Location
    {
        public Location(string Name) => throw new NotImplementedException();
        public string Name { get; set; }
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<string> ExitList => throw new NotImplementedException();
        public void AddExit(Direction direction, Location currentLocation)
        {
            throw new NotImplementedException();
        }
        public Location GetExit(Direction direction) => throw new NotImplementedException();
    }
}
