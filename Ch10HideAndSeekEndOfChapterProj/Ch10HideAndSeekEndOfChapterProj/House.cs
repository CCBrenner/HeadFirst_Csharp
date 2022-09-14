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
            Entry.AddExitsOfConnectedLocations(Direction.East, new LocationWithHidingPlace("Hallway", ""));
            Entry.AddExitsOfConnectedLocations(Direction.Out, new LocationWithHidingPlace("Garage", ""));

            Location hallway = Entry.Exits[Direction.East];
            hallway.AddExitsOfConnectedLocations(Direction.Northwest, new LocationWithHidingPlace("Kitchen", ""));
            hallway.AddExitsOfConnectedLocations(Direction.North, new LocationWithHidingPlace("Downstairs Bathroom", ""));
            hallway.AddExitsOfConnectedLocations(Direction.South, new LocationWithHidingPlace("Living Room", ""));
            hallway.AddExitsOfConnectedLocations(Direction.Up, new LocationWithHidingPlace("Landing", ""));

            Location landing = Entry.Exits[Direction.East].Exits[Direction.Up];
            landing.AddExitsOfConnectedLocations(Direction.Northwest, new LocationWithHidingPlace("Master Bedroom", ""));
            landing.AddExitsOfConnectedLocations(Direction.West, new LocationWithHidingPlace("Upstairs Bathroom", ""));
            landing.AddExitsOfConnectedLocations(Direction.Southwest, new LocationWithHidingPlace("Nursery", ""));
            landing.AddExitsOfConnectedLocations(Direction.South, new LocationWithHidingPlace("Pantry", ""));
            landing.AddExitsOfConnectedLocations(Direction.Southeast, new LocationWithHidingPlace("Kids Room", ""));
            landing.AddExitsOfConnectedLocations(Direction.Up, new LocationWithHidingPlace("Attic", ""));

            Location masterBedroom = Entry.Exits[Direction.East].Exits[Direction.Up].Exits[Direction.Northwest];
            masterBedroom.AddExitsOfConnectedLocations(Direction.East, new LocationWithHidingPlace("Master Bathroom", ""));

            // Define/Assign locations field:
            List<Location> locationHubs = new List<Location>()
            {
                Entry,  // Entry
                Entry.Exits[Direction.East],  // Hallway
                Entry.Exits[Direction.East].Exits[Direction.Up],  // Landing
                Entry.Exits[Direction.East].Exits[Direction.Up].Exits[Direction.Northwest],  // Master Bedroom
            };

            List<Location> tempLocations = new List<Location>();

            foreach (Location locationHub in locationHubs)
                foreach (KeyValuePair<Direction, Location> pair in locationHub.Exits)
                    if (!tempLocations.Contains(pair.Value)) 
                        tempLocations.Add(pair.Value);
            locations = tempLocations;
        }
        public static Random Random = new Random();
        public static Location Entry { get; private set; }
        private static IEnumerable<Location> locations = new List<Location>();
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
        public static Location RandomExit(Location location)
        {
            IOrderedEnumerable<Location>? locations = 
                location.Exits
                .Select(exit => exit.Value)
                .OrderBy(selectLocation => selectLocation.Name);
            return locations.ToList()[House.Random.Next(locations.Count())];
        }
        public static void ClearHidingPlaces()
        {
            foreach (Location location in locations)
                if (location is LocationWithHidingPlace locationWithHidingPlace)
                    locationWithHidingPlace.CheckHidingPlace();
        }
    }
}
