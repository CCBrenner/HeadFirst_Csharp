using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.AreEqual("Garage", garage.Name);

            var hallway = House.Entry.GetExit(Direction.East);
            Assert.AreEqual("Hallway", hallway.Name);

            var kitchen = hallway.GetExit(Direction.Northwest);
            Assert.AreEqual("Kitchen", kitchen.Name);

            var downstairsBathroom = hallway.GetExit(Direction.North);
            Assert.AreEqual("Downstairs Bathroom", downstairsBathroom.Name);

            var livingRoom = hallway.GetExit(Direction.South);
            Assert.AreEqual("Living Room", livingRoom.Name);

            var landing = hallway.GetExit(Direction.Up);
            Assert.AreEqual("Landing", landing.Name);

            var masterBedroom = landing.GetExit(Direction.Northwest);
            Assert.AreEqual("Master Bedroom", masterBedroom.Name);

            var masterBathroom = masterBedroom.GetExit(Direction.East);
            Assert.AreEqual("Master Bathroom", masterBathroom.Name);

            var upstairsBathroom = landing.GetExit(Direction.West);
            Assert.AreEqual("Upstairs Bathroom", upstairsBathroom.Name);

            var nursery = landing.GetExit(Direction.Southwest);
            Assert.AreEqual("Nursery", nursery.Name);

            var pantry = landing.GetExit(Direction.South);
            Assert.AreEqual("Pantry", pantry.Name);

            var kidsRoom = landing.GetExit(Direction.Southeast);
            Assert.AreEqual("Kids Room", kidsRoom.Name);

            var attic = landing.GetExit(Direction.Up);
            Assert.AreEqual("Attic", attic.Name);
        }
    }
}
