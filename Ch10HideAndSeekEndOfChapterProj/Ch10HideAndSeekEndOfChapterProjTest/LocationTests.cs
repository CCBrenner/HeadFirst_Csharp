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
        [TestMethod]
        public void Initialize()
        {
            Location hallway = new Location("Hallway");
            Location kitchen = new Location("Kitchen");
            Location entry = new Location("Entry");
            Location bathroom = new Location("Bathroom");
            Location livingRoom = new Location("Living Room");
            Location landing = new Location("Landing");
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
        }
        /*
        [TestMethod]
        public void TestGetExit()
        {
            
        }
        [TestMethod]
        public void TestExitList()
        {
            throw new NotImplementedException();
        }
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