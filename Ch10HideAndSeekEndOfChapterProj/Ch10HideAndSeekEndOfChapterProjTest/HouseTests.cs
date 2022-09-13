using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
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
        [TestMethod]
        public void TestGetLocationByName()
        {
            Assert.AreEqual("Entry", House.GetLocationByName("Entry").Name);
            Assert.AreEqual("Attic", House.GetLocationByName("Attic").Name);
            Assert.AreEqual("Garage", House.GetLocationByName("Garage").Name);
            Assert.AreEqual("Master Bedroom", House.GetLocationByName("Master Bedroom").Name);
            Assert.AreEqual("Null", House.GetLocationByName("Secret Library").Name);  // if location does not exist in House
        }
        [TestMethod]
        public void TestRandomExit()
        {
            Location landing = House.Entry.Exits[Direction.East].Exits[Direction.Up];

            House.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("Attic", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 1 };
            Assert.AreEqual("Hallway", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 2 };
            Assert.AreEqual("Kids Room", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 3 };
            Assert.AreEqual("Master Bedroom", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 4 };
            Assert.AreEqual("Nursery", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 5 };
            Assert.AreEqual("Pantry", House.RandomExit(landing).Name);

            House.Random = new MockRandom() { ValueToReturn = 6 };
            Assert.AreEqual("Upstairs Bathroom", House.RandomExit(landing).Name);

            var kitchen = House.GetLocationByName("Kitchen");
            House.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("Hallway", House.RandomExit(kitchen).Name);
        }
        [TestMethod]
        public void TestHidingPlaces()
        {
            Assert.IsInstanceOfType(House.GetLocationByName("Garage"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Kitchen"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Living Room"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Downstairs Bathroom"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Master Bedroom"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Master Bath"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Upstairs Bathroom"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Kids Room"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Nursery"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Pantry"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Attic"), typeof(LocationWithHidingPlace));
        }
        /*
        [TestMethod]
        public void TestClearHidingPlaces()
        {
            var garage = House.GetLocationByName("Garage") as LocationWithHidingPlace;
            garage.Hide(new Opponent("Opponent1"));

            var attic = House.GetLocationByName("Attic") as LocationWithHidingPlace;
            attic.Hide(new Opponent("Opponent2"));
            attic.Hide(new Opponent("Opponent3"));
            attic.Hide(new Opponent("Opponent4"));

            House.ClearHidingPlaces();
            Assert.AreEqual(0, garage.CheckHidingPlace().Count());
            Assert.AreEqual(0, attic.CheckHidingPlace().Count());
        }
        */
    }
}
