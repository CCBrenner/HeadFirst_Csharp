using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    using Ch10HideAndSeekEndOfChapterProj;
    [TestClass]
    public class GameControllerTests
    {
        GameController gameController;

        [TestInitialize]
        public void Initialize() => gameController = new GameController();
        [TestMethod]
        public void TestMovement()
        {
            Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
            Assert.IsFalse(gameController.Move(Direction.Up));

            Assert.IsTrue(gameController.Move(Direction.East));
            Assert.AreEqual("Hallway", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Up));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Northwest));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Southeast));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);
        }
        [TestMethod]
        public void TestParseInput()
        {
            string initialStatus = gameController.Status;

            Assert.AreEqual("That's not a valid direction.", gameController.ParseInput("x"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("There's no exit in that direction.", gameController.ParseInput("Up"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("Moving East", gameController.ParseInput("East"));
            Assert.AreEqual($"You are in the Hallway. You see the following exits:" +
                $"{Environment.NewLine} - the Donwstairs Bathroom is to the North" +
                $"{Environment.NewLine} - the Living Room is to the South" +
                $"{Environment.NewLine} - the Entry is to the West" +
                $"{Environment.NewLine} - the Kitchen is to the Northwest" +
                $"{Environment.NewLine} - the Landing is Up", gameController.Status);

            Assert.AreEqual("Moving South", gameController.ParseInput("south"));
            Assert.AreEqual($"You are in the Hallway. You see the following exits:" +
                $"{Environment.NewLine} - the Hallway is to the North", gameController.Status);

            Assert.AreEqual("Moving North", gameController.ParseInput("north"));
            Assert.AreEqual("Moving Up", gameController.ParseInput("UP"));
            Assert.AreEqual($"You are on the Landing. You see the following exits:" +
                $"{Environment.NewLine} - the Pantry is to the South" +
                $"{Environment.NewLine} - the Upstairs Bathroom is to the West" +
                $"{Environment.NewLine} - the Nursery is to the Southwest" +
                $"{Environment.NewLine} - the Master Bedroom is to the Northwest" +
                $"{Environment.NewLine} - the Kids Room is to the Southeast" +
                $"{Environment.NewLine} - the Hallway is Down", gameController.Status);

            Assert.AreEqual("That's not a valid direction.", gameController.ParseInput("Master dRoom"));
            Assert.AreEqual("There's no exit in that direction.", gameController.ParseInput("NOrth"));
            Assert.AreEqual($"You are on the Landing. You see the following exits:" +
                $"{Environment.NewLine} - the Pantry is to the South" +
                $"{Environment.NewLine} - the Upstairs Bathroom is to the West" +
                $"{Environment.NewLine} - the Nursery is to the Southwest" +
                $"{Environment.NewLine} - the Master Bedroom is to the Northwest" +
                $"{Environment.NewLine} - the Kids Room is to the Southeast" +
                $"{Environment.NewLine} - the Hallway is Down", gameController.Status);
            Assert.AreEqual("Moving Northwest", gameController.ParseInput("Master BedRoom"));
            Assert.AreEqual($"You are in the Master Bedroom. You see the following exits:" +
                $"{Environment.NewLine} - the Landing is to the Southeast" +
                $"{Environment.NewLine} - the Master Bathroom is to the East", gameController.Status);
        }
    }
}
