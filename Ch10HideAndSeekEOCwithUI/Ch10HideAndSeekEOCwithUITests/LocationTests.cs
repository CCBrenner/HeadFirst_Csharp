using System;
using System.Linq;
using System.Collections;

namespace Ch10HideAndSeekEOCwithUI
{

    [TestClass]
    public class LocationTests
    {
        Location hallway;
        Location kitchen;
        Location entry;
        Location bathroom;
        Location livingRoom;
        Location landing;

        [TestInitialize]
        public void Initialize()
        {
            hallway = new Location("Hallway");
            Assert.AreEqual("Hallway", hallway.ToString());
            Assert.AreEqual(0, hallway.ExitList.Count());

            kitchen = new Location("Kitchen");
            entry = new Location("Entry");
            bathroom = new Location("Bathroom");
            livingRoom = new Location("Living Room");
            landing = new Location("Landing");

            hallway.AddExitsOfConnectedLocations(Direction.West, entry);
            hallway.AddExitsOfConnectedLocations(Direction.Northwest, kitchen);
            hallway.AddExitsOfConnectedLocations(Direction.North, bathroom);
            hallway.AddExitsOfConnectedLocations(Direction.South, livingRoom);
            hallway.AddExitsOfConnectedLocations(Direction.Up, landing);

            Assert.AreEqual(5, hallway.Exits.Count);
            Assert.AreEqual("Bathroom", hallway.Exits[Direction.North].ToString());
            Assert.AreEqual("Bathroom", hallway.Exits[(Direction)(-1)].ToString());
        }
        [TestMethod]
        public void TestGetExit()
        {
            Assert.AreEqual("Living Room", hallway.GetExit(Direction.South).ToString());
            Assert.AreSame(hallway, kitchen.GetExit(Direction.Southeast));
            Assert.AreSame(kitchen, hallway.GetExit(Direction.Northwest));
            Assert.AreEqual("Hallway", livingRoom.GetExit(Direction.North).ToString());
            Assert.AreEqual("Hallway", kitchen.GetExit(Direction.Southeast).ToString());
        }
        [TestMethod]
        public void TestExitList()
        {
            List<string> expectedDirectionPhrases = new List<string>()
            {
               " - the Bathroom is to the North",
               " - the Living Room is to the South",
               " - the Entry is to the West",
               " - the Kitchen is to the Northwest",
               " - the Landing is Up",
            };

            CollectionAssert.AreEqual(expectedDirectionPhrases, hallway.ExitList.ToList());
            Assert.AreEqual(5, hallway.ExitList.Count());
            Assert.AreEqual(" - the Hallway is to the South", bathroom.ExitList.ToList()[0]);
            Assert.AreEqual(" - the Hallway is to the North", livingRoom.ExitList.ToList()[0]);
        }
        [TestMethod]
        public void TestAddExitsOfConnectedLocations()
        {
            Assert.AreSame(kitchen, hallway.Exits[Direction.Northwest]);
            Assert.AreSame(hallway, kitchen.Exits[Direction.Southeast]);
            Assert.AreSame(entry, hallway.Exits[Direction.West]);
            Assert.AreSame(hallway, entry.Exits[Direction.East]);
            Assert.AreSame(livingRoom, hallway.Exits[Direction.South]);
            Assert.AreSame(hallway, livingRoom.Exits[Direction.North]);
            Assert.AreSame(bathroom, hallway.Exits[Direction.North]);
            Assert.AreSame(hallway, bathroom.Exits[Direction.South]);
            Assert.AreSame(landing, hallway.Exits[Direction.Up]);
            Assert.AreSame(hallway, landing.Exits[Direction.Down]);
        }
        [TestMethod]
        public void TestAddHall()
        {
            Location masterBedroom = new Location("Master Bedroom");
            hallway
                .Exits[Direction.Up]
                .AddExitsOfConnectedLocations(Direction.Northwest, masterBedroom);
            Assert.AreSame(masterBedroom, landing.Exits[Direction.Northwest]);
            Assert.AreSame(landing, masterBedroom.Exits[Direction.Southeast]);

            Location masterBathroom = new Location("Master Bathroom");
            hallway
                .Exits[Direction.Up]
                .Exits[Direction.Northwest]
                .AddExitsOfConnectedLocations(Direction.East, masterBathroom);
            Assert.AreSame(masterBathroom, masterBedroom.Exits[Direction.East]);
            Assert.AreSame(masterBedroom, masterBathroom.Exits[Direction.West]);
        }
    }
}