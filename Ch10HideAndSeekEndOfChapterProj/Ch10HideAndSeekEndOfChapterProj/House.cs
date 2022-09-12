using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public static class House
    {
        static House()
        {
            Entry = new Location("Entry");
            Entry.AddExitsOfConnectedLocations(Direction.Out, new Location("Garage"));
            Entry.AddExitsOfConnectedLocations(Direction.East, new Location("Hallway"));

            Location hallway = Entry.Exits[Direction.East];
            hallway.AddExitsOfConnectedLocations(Direction.Northwest, new Location("Kitchen"));
            hallway.AddExitsOfConnectedLocations(Direction.North, new Location("Downstairs Bathroom"));
            hallway.AddExitsOfConnectedLocations(Direction.South, new Location("Living Room"));
            hallway.AddExitsOfConnectedLocations(Direction.Up, new Location("Landing"));

            Location landing = Entry.Exits[Direction.East].Exits[Direction.Up];
            landing.AddExitsOfConnectedLocations(Direction.Northwest, new Location("Master Bedroom"));
            landing.AddExitsOfConnectedLocations(Direction.West, new Location("Upstairs Bathroom"));
            landing.AddExitsOfConnectedLocations(Direction.Southwest, new Location("Nursery"));
            landing.AddExitsOfConnectedLocations(Direction.South, new Location("Pantry"));
            landing.AddExitsOfConnectedLocations(Direction.Southeast, new Location("Kids Room"));
            landing.AddExitsOfConnectedLocations(Direction.Up, new Location("Attic"));

            Location masterBedroom = Entry.Exits[Direction.East].Exits[Direction.Up].Exits[Direction.Northwest];
            masterBedroom.AddExitsOfConnectedLocations(Direction.East, new Location("Master Bathroom"));
        }
        public static Location Entry { get; private set; }
        public static Location GetLocationByName(string name)
        {
            List<Location> locationHubs = new List<Location>()
            {
                Entry,  // Entry
                Entry.Exits[Direction.East],  // Hallway
                Entry.Exits[Direction.East].Exits[Direction.Up],  // Landing
                Entry.Exits[Direction.East].Exits[Direction.Up].Exits[Direction.Northwest],  // Master Bedroom
            };

            foreach(Location locationHub in locationHubs)
                foreach (KeyValuePair<Direction, Location> pair in locationHub.Exits)
                    if (pair.Value.Name == name)
                        return pair.Value;
            return new Location("Null");
        }
    }
}
