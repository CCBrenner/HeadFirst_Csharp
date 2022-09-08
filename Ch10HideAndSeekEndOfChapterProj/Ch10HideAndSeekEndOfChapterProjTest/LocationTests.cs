using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections;
using Ch10HideAndSeekEndOfChapterProj;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    [TestClass]
    public class LocationTests
    {
        Location hallway = new Location("Hallway");
        Location kitchen = new Location("Kitchen");
        Location entry = new Location("Entry");
        Location bathroom = new Location("Bathroom");
        Location livingRoom = new Location("Living Room");
        Location landing = new Location("Landing");

        [TestMethod]
        public void Initialize()
        {
            hallway.AddExit(Direction.West, entry);
            entry.AddExit(Direction.East, hallway);
            hallway.AddExit(Direction.Northwest, kitchen);
            kitchen.AddExit(Direction.Southeast, hallway);
            hallway.AddExit(Direction.North, bathroom);
            bathroom.AddExit(Direction.South, hallway);
            hallway.AddExit(Direction.South, livingRoom);
            livingRoom.AddExit(Direction.North, hallway);
            hallway.AddExit(Direction.Up, landing);
            landing.AddExit(Direction.Down, hallway);

            Assert.AreEqual("Hallway", hallway.Name.ToString());
            Assert.AreEqual(5, hallway.Exits.Count);
            Assert.AreEqual("Bathroom", hallway.Exits[Direction.North].ToString());
            Assert.AreEqual("Bathroom", hallway.Exits[(Direction)(-1)].ToString());
        }
        [TestMethod]
        public void TestGetExit()
        {
            hallway.AddExit(Direction.West, entry);
            entry.AddExit(Direction.East, hallway);
            hallway.AddExit(Direction.Northwest, kitchen);
            kitchen.AddExit(Direction.Southeast, hallway);
            hallway.AddExit(Direction.North, bathroom);
            bathroom.AddExit(Direction.South, hallway);
            hallway.AddExit(Direction.South, livingRoom);
            livingRoom.AddExit(Direction.North, hallway);
            hallway.AddExit(Direction.Up, landing);
            landing.AddExit(Direction.Down, hallway);

            Assert.AreEqual("Living Room", hallway.GetExit(Direction.South).Name.ToString());
            Assert.AreEqual("Hallway", livingRoom.GetExit(Direction.North).Name.ToString());
            Assert.AreEqual("Hallway", kitchen.GetExit(Direction.Southeast).Name.ToString());
        }
        [TestMethod]
        public void TestExitList()
        {
            hallway.AddExit(Direction.West, entry);
            entry.AddExit(Direction.East, hallway);
            hallway.AddExit(Direction.Northwest, kitchen);
            kitchen.AddExit(Direction.Southeast, hallway);
            hallway.AddExit(Direction.North, bathroom);
            bathroom.AddExit(Direction.South, hallway);
            hallway.AddExit(Direction.South, livingRoom);
            livingRoom.AddExit(Direction.North, hallway);
            hallway.AddExit(Direction.Up, landing);
            landing.AddExit(Direction.Down, hallway);

            Assert.AreEqual(5, hallway.ExitList.Count());
            Assert.AreEqual(" - the Bathroom is to the North", hallway.ExitList.ToList()[0]);
            Assert.AreEqual(" - the Hallway is to the South", bathroom.ExitList.ToList()[0]);
            Assert.AreEqual(" - the Living Room is to the South", hallway.ExitList.ToList()[1]);
            Assert.AreEqual(" - the Hallway is to the North", livingRoom.ExitList.ToList()[0]);
        }
        /*
        [TestMethod]
        public void TestReturnExits()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void TestAddHall()
        {
            throw new NotImplementedException();
        }
        */
    }
}