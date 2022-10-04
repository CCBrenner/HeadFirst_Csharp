using System;
using System.Text.Json;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    [TestClass]
    public class SaveGameTests
    {
        GameController gameController;
        Opponent joe;
        Opponent bob;
        Opponent ana;
        Opponent owen;
        Opponent jimmy;

        [TestInitialize]
        public void Initializer()
        {
            gameController = new GameController();

            Assert.IsFalse(gameController.GameOver);

            // Clear the hiding places and hide the opponents in different rooms
            House.ClearHidingPlaces();
            joe = gameController.Opponents.ToList()[0];
            (House.GetLocationByName("Garage") as LocationWithHidingPlace).Hide(joe);
            bob = gameController.Opponents.ToList()[1];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(bob);
            ana = gameController.Opponents.ToList()[2];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(ana);
            owen = gameController.Opponents.ToList()[3];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(owen);
            jimmy = gameController.Opponents.ToList()[4];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(jimmy);

            Assert.AreEqual(1, gameController.MoveNumber);
            gameController.ParseInput("Check");
            gameController.ParseInput("Out");
            gameController.ParseInput("check");
            gameController.ParseInput("In");
            gameController.ParseInput("East");
            gameController.ParseInput("North");
            gameController.ParseInput("check");
            gameController.ParseInput("South");
            gameController.ParseInput("Northwest");
            Assert.AreEqual("You found 2 opponents hiding next to the stove", gameController.ParseInput("check"));
            Assert.AreEqual($"You are in the Kitchen. You see the following exits:" +
                $"{Environment.NewLine} - the Hallway is to the Southeast" +
                $"{Environment.NewLine}Someone could hide next to the stove" +
                $"{Environment.NewLine}You have found 3 of 5 opponents: Joe, Bob, Jimmy", gameController.Status);
            Assert.AreEqual("11: Which direction do you want to go (or type 'check'): ", gameController.Prompt);
            Assert.AreEqual(11, gameController.MoveNumber);
        }

        [TestMethod]
        public void TestParseInput()
        {
            // Verify the hiding locations of the remaining opponents
            List<Opponent> atticHiders = (House.GetLocationByName("Attic") as LocationWithHidingPlace).OpponentsHiddenHere;
            Assert.AreEqual(ana, atticHiders[0]);
            Assert.AreEqual(owen, atticHiders[1]);

            // Provide correct response if the saved file is not there
            Assert.AreEqual("Could not load game: Saved file of name \"my_saved_game.json\" not found", gameController.ParseInput("load my_saved_game"));

            // Save the state of the game to a file with given name located in 
            Assert.AreEqual("Saved current game to my_saved_game.json", gameController.ParseInput("save my_saved_game"));

            // Start a new game
            gameController = new GameController();
            Assert.IsFalse(gameController.GameOver);
            House.ClearHidingPlaces();

            // Hide opponents in different rooms than the original game to verify hiding place "state has loaded" later on
            joe = gameController.Opponents.ToList()[0];
            (House.GetLocationByName("Attic") as LocationWithHidingPlace).Hide(joe);
            bob = gameController.Opponents.ToList()[1];
            (House.GetLocationByName("Garage") as LocationWithHidingPlace).Hide(bob);
            ana = gameController.Opponents.ToList()[2];
            (House.GetLocationByName("Downstairs Bathroom") as LocationWithHidingPlace).Hide(ana);
            owen = gameController.Opponents.ToList()[3];
            (House.GetLocationByName("Kitchen") as LocationWithHidingPlace).Hide(owen);
            jimmy = gameController.Opponents.ToList()[4];
            (House.GetLocationByName("Downstairs Bathroom") as LocationWithHidingPlace).Hide(jimmy);
            Assert.AreEqual(1, gameController.MoveNumber);

            // Check all of the game state variables that are relevant to resuming the game
            Assert.AreEqual("Loaded game from \"my_saved_game.json\"", gameController.ParseInput("load my_saved_game"));
            Assert.AreEqual($"You are in the Kitchen. You see the following exits:" +
                $"{Environment.NewLine} - the Hallway is to the Southeast" +
                $"{Environment.NewLine}Someone could hide next to the stove" +
                $"{Environment.NewLine}You have found 3 of 5 opponents: Joe, Bob, Jimmy", gameController.Status);
            Assert.AreEqual("11: Which direction do you want to go (or type 'check'): ", gameController.Prompt);
            Assert.AreEqual(11, gameController.MoveNumber);

            // Verify the hiding locations of the remaining opponents
            atticHiders = (House.GetLocationByName("Attic") as LocationWithHidingPlace).OpponentsHiddenHere;
            Console.WriteLine(string.Join(", ", atticHiders));
            Assert.AreEqual(ana.Name, atticHiders[0].Name);
            Assert.IsInstanceOfType(atticHiders[0], typeof(Opponent));
            Assert.AreEqual(owen.Name, atticHiders[1].Name);
            Assert.IsInstanceOfType(atticHiders[1], typeof(Opponent));

            // Test that file has been deleted after it has been loaded so as not to load the game again in the future
            Assert.AreEqual("Could not load game: Saved file of name \"my_saved_game.json\" not found", gameController.ParseInput("load my_saved_game"));
        }

        [TestMethod]
        public void TestHandleInvalidSymbols()
        {
            Assert.AreEqual("Could not save game to \"my saved game.json\"", gameController.ParseInput("save my saved game"));
            Assert.AreEqual("Could not save game to \"file a.json\"", gameController.ParseInput("save file a"));
            Assert.AreEqual("Could not save game to \"my/saved/game.json\"", gameController.ParseInput("save my/saved/game"));
            Assert.AreEqual("Could not save game to \"my saved//game.json\"", gameController.ParseInput("save my saved//game"));
            Assert.AreEqual("Saved current game to \"my_saved_game.json\"", gameController.ParseInput("save my_saved_game"));
            Assert.AreEqual("Saved current game to \"mysavedgame.json\"", gameController.ParseInput("save mySavedGame"));

            Assert.AreEqual("Could not load game from \"my saved game.json\"", gameController.ParseInput("load my saved game"));
            Assert.AreEqual("Could not load game from \"file a.json\"", gameController.ParseInput("load file a"));
            Assert.AreEqual("Could not load game from \"my/saved/game.json\"", gameController.ParseInput("load my/saved/game"));
            Assert.AreEqual("Could not load game from \"my saved//game.json\"", gameController.ParseInput("load my saved//game"));
            Assert.AreEqual("Loaded current game from \"my_saved_game.json\"", gameController.ParseInput("load my_saved_game"));
            Assert.AreEqual("Loaded current game from \"mysavedgame.json\"", gameController.ParseInput("load mySavedGame"));
        }
    }
}
