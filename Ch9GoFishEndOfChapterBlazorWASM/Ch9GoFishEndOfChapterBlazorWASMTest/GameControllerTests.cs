using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Ch9GoFishEndOfChapterBlazorWASM;
using System.Linq;

namespace Ch9GoFishEndOfChapterBlazorWASMTest;

[TestClass]
public class GameControllerTests
{
    [TestInitialize]
    public void Initialize()
    {
        Player.Random = new MockRandom() { ValueToReturn = 0 };
    }

    [TestMethod]
    public void TestConstructor()
    {
        GameController gameController = new GameController("Human", 3);
        Assert.AreEqual("Starting a new game with players Human, Computer 1, Computer 2, Computer 3", gameController.Status);
    }
    [TestMethod]
    public void TestNewGame()
    {
        // It may seem like there should be more testing for the second part of the code that exists,
        // but this is already covered by another test. There is no sense in testing the same method twice.
        Player.Random = new MockRandom() { ValueToReturn = 0 };
        GameController gameController = new GameController("Owen", 1);
        gameController.NextRound(gameController.Opponents.First(), Value.Six);
        gameController.NewGame();
        Assert.AreEqual("Owen", gameController.HumanPlayer.Name);
        Assert.AreEqual("Computer 1", gameController.Opponents.First().Name);
        Assert.AreEqual("Starting a new game", gameController.Status);
    }
}


