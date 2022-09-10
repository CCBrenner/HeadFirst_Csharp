using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    using Ch10HideAndSeekEndOfChapterProj;

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
            Assert.AreSame("Hallway", hallway.ToString());
            Assert.AreEqual(0, hallway.ExitList.Count());

            kitchen = new Location("Kitchen");
            entry = new Location("Entry");
            bathroom = new Location("Bathroom");
            livingRoom = new Location("Living Room");
            landing = new Location("Landing");

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

            Assert.AreEqual(5, hallway.Exits.Count);
            Assert.AreEqual("Bathroom", hallway.Exits[Direction.North].ToString());
            Assert.AreEqual("Bathroom", hallway.Exits[(Direction)(-1)].ToString());
        }
        [TestMethod]
        public void TestGetExit()
        {
            Assert.AreEqual("Living Room", hallway.GetExit(Direction.South).Name.ToString());
            Assert.AreEqual("Hallway", livingRoom.GetExit(Direction.North).Name.ToString());
            Assert.AreEqual("Hallway", kitchen.GetExit(Direction.Southeast).Name.ToString());
        }
        [TestMethod]
        public void TestExitList()
        {
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