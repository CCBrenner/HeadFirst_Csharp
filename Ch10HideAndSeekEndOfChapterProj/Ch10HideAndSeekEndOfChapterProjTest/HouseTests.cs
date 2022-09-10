using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    using Ch10HideAndSeekEndOfChapterProj;
    [TestClass]
    public class HouseTests
    {
        [TestMethod]
        public void TestLayout()
        {
            Assert.AreEqual("Entry", House.Entry.Name);

            var garage = House.Entry.GetExit(Direction.Out);
            var hallway = House.Entry.GetExit(Direction.East);
            Assert.AreEqual("Garage", garage.Name);
            Assert.AreEqual("Hallway", hallway.Name);

            var kitchen = hallway.GetExit(Direction.Northwest);
            var downstairsBathroom = hallway.GetExit(Direction.North);
            var livingRoom = hallway.GetExit(Direction.South);
            var landing = hallway.GetExit(Direction.Up);
            Assert.AreEqual("Kitchen", kitchen.Name);
            Assert.AreEqual("Downstairs Bathroom", downstairsBathroom.Name);
            Assert.AreEqual("Living Room", livingRoom.Name);
            Assert.AreEqual("Landing", landing.Name);

            var masterBedroom = landing.GetExit(Direction.Northwest);
            var upstairsBathroom = landing.GetExit(Direction.West);
            var nursery = landing.GetExit(Direction.Southwest);
            var pantry = landing.GetExit(Direction.South);
            var kidsRoom = landing.GetExit(Direction.Southeast);
            var attic = landing.GetExit(Direction.Up);
            Assert.AreEqual("Master Bedroom", masterBedroom.Name);
            Assert.AreEqual("Upstairs Bathroom", upstairsBathroom.Name);
            Assert.AreEqual("Nursery", nursery.Name);
            Assert.AreEqual("Pantry", pantry.Name);
            Assert.AreEqual("Kids Room", kidsRoom.Name);
            Assert.AreEqual("Attic", attic.Name);

            var masterBathroom = masterBedroom.GetExit(Direction.East);
            Assert.AreEqual("Master Bathroom", masterBathroom.Name);
        }
    }
}
