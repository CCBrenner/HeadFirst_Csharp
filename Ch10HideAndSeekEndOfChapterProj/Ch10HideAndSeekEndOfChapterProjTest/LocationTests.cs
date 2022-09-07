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

            Assert.AreEqual(hallway.Name.ToString(), "Hallway");
            Assert.AreEqual(hallway.Exits.Count, 5);
            Assert.AreEqual(hallway.Exits[Direction.North].ToString(), "Bathroom");
            Assert.AreEqual(hallway.Exits[(Direction)(-1)].ToString(), "Bathroom");
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

            Assert.AreEqual(hallway.GetExit(Direction.South).Name.ToString(), "Living Room");
            Assert.AreEqual(livingRoom.GetExit(Direction.North).Name.ToString(), "Hallway");
            Assert.AreEqual(kitchen.GetExit(Direction.Southeast).Name.ToString(), "Hallway");
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

            Assert.AreEqual(hallway.ExitList.Count(), 5);
            Assert.AreEqual(hallway.ExitList.ToList()[0], " - the Bathroom is to the North");
            Assert.AreEqual(bathroom.ExitList.ToList()[0], " - the Hallway is to the South");
            Assert.AreEqual(hallway.ExitList.ToList()[1], " - the Living Room is to the South");
            Assert.AreEqual(livingRoom.ExitList.ToList()[0], " - the Hallway is to the North");
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