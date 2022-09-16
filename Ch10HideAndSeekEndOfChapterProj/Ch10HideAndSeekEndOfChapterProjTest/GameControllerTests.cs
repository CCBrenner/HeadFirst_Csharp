using System;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
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
                $"{Environment.NewLine} - the Downstairs Bathroom is to the North" +
                $"{Environment.NewLine} - the Living Room is to the South" +
                $"{Environment.NewLine} - the Entry is to the West" +
                $"{Environment.NewLine} - the Kitchen is to the Northwest" +
                $"{Environment.NewLine} - the Landing is Up", gameController.Status);

            Assert.AreEqual("Moving South", gameController.ParseInput("south"));
            Assert.AreEqual($"You are in the Living Room. You see the following exits:" +
                $"{Environment.NewLine} - the Hallway is to the North", gameController.Status);

            string landingStatus = $"You are on the Landing. You see the following exits:" +
                $"{Environment.NewLine} - the Pantry is to the South" +
                $"{Environment.NewLine} - the Upstairs Bathroom is to the West" +
                $"{Environment.NewLine} - the Nursery is to the Southwest" +
                $"{Environment.NewLine} - the Master Bedroom is to the Northwest" +
                $"{Environment.NewLine} - the Kids Room is to the Southeast" +
                $"{Environment.NewLine} - the Attic is Up" +
                $"{Environment.NewLine} - the Hallway is Down";

            Assert.AreEqual("Moving North", gameController.ParseInput("north"));
            Assert.AreEqual("Moving Up", gameController.ParseInput("UP"));
            Assert.AreEqual(landingStatus, gameController.Status);

            Assert.AreEqual("That's not a valid direction.", gameController.ParseInput("Master dRoom"));
            Assert.AreEqual("There's no exit in that direction.", gameController.ParseInput("NOrth"));
            Assert.AreEqual(landingStatus, gameController.Status);

            Assert.AreEqual("Moving Northwest", gameController.ParseInput("Northwest"));
            Assert.AreEqual($"You are in the Master Bedroom. You see the following exits:" +
                $"{Environment.NewLine} - the Master Bathroom is to the East" +
                $"{Environment.NewLine} - the Landing is to the Southeast", gameController.Status);
        }
        [TestMethod]
        public void TestParseCheck()
        {
            Assert.IsFalse(gameController.GameOver);

            // Clear the hiding places and hide the opponents in different rooms
            House.ClearHidingPlaces();
            var joe = gameController.Opponents.ToList()[0];
            (House.GetLocationByName("Garage") as LocationWithHidingPlace).Hide(joe);
            var bob = gameController.Opponents.ToList()[1];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(bob);
            var ana = gameController.Opponents.ToList()[2];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(ana);
            var owen = gameController.Opponents.ToList()[3];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(owen);
            var jimmy = gameController.Opponents.ToList()[4];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(jimmy);

            // Check the Entry -- there are no hiding players there
            Assert.AreEqual(1, gameController.MoveNumber);
            Assert.AreEqual("There is no one hiding in the Entry", gameController.ParseInput("Check"));
            Assert.AreEqual(2, gameController.MoveNumber);

            // Move to the Garage
            gameController.ParseInput("Out");
            Assert.AreEqual(3, gameController.MoveNumber);

            // We hid Joe in the Garage, so validate ParseInput's return value and the properties
            Assert.AreEqual("You found 1 opponent hiding behind the car", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Garage. You see the following exits:" +
                $"{Environment.NewLine} - the Entry is In" +
                $"{Environment.NewLine}Someone could hide behind the car" +
                $"{Environment.NewLine}You have found 1 of 5 opponents: Joe", gameController.Status);
            Assert.AreEqual("4: Which direction do you want to go (or type 'check'): ", gameController.Prompt);
            Assert.AreEqual(4, gameController.MoveNumber);

            // Move to the bathroom, where no one is hiding
            gameController.ParseInput("In");
            gameController.ParseInput("East");
            gameController.ParseInput("North");

            // Check the Bathroom to make sure no one is hiding there
            Assert.AreEqual("Nobody was hiding behind the door", gameController.ParseInput("check"));
            Assert.AreEqual(8, gameController.MoveNumber);

            // Check the Donwstairs Bathroom to make sure no one is hiding there
            gameController.ParseInput("South");
            gameController.ParseInput("Northwest");
            Assert.AreEqual("You found 2 opponents hiding next to the stove", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Kitchen. You see the following exits:" +
                $"{Environment.NewLine} - the Hallway is to the Southeast" +
                $"{Environment.NewLine}Someone could hide next to the stove" +
                $"{Environment.NewLine}You have found 3 of 5 opponents: Joe, Bob, Jimmy", gameController.Status);
            Assert.AreEqual("11: Which direction do you want to go (or type 'check'): ", gameController.Prompt);
            Assert.AreEqual(11, gameController.MoveNumber);

            Assert.IsFalse(gameController.GameOver);

            // Head up to the Landing, then check the Pantry (no one is hiding there)
            gameController.ParseInput("Southeast");
            gameController.ParseInput("Up");
            Assert.AreEqual(13, gameController.MoveNumber);

            gameController.ParseInput("South");
            Assert.AreEqual("Nobody was hiding inside a cabinet", gameController.ParseInput("check"));
            Assert.AreEqual(15, gameController.MoveNumber);

            // Check the Attic to find the last two opponents
            gameController.ParseInput("North");
            gameController.ParseInput("Up");
            Assert.AreEqual(17, gameController.MoveNumber);

            Assert.AreEqual("You found 2 opponents hiding in a trunk", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Attic. You see the following exits:" +
                $"{Environment.NewLine} - the Landing is Down" +
                $"{Environment.NewLine}Someone could hide in a trunk" +
                $"{Environment.NewLine}You have found 5 of 5 opponents: Joe, Bob, Jimmy, Ana, Owen", gameController.Status);
            Assert.AreEqual("18: Which direction do you want to go (or type 'check'): ", gameController.Prompt);
            Assert.AreEqual(18, gameController.MoveNumber);

            Assert.IsTrue(gameController.GameOver);
        }
    }
}
