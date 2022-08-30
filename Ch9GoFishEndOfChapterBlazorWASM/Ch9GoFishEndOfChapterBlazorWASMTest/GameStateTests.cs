using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Ch9GoFishEndOfChapterBlazorWASM;
using System.Collections;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Ch9GoFishEndOfChapterBlazorWASMTest;

[TestClass]
public class GameStateTests
{
    [TestMethod]
    public void TestConstructor()
    {
        // Test constructor assignment of HumanPlayer, Players, Opponents, and Stock
        GameState gameState = new GameState("Human", 3, new Deck());
        List<string> expectedGameState = new List<string>()
        {
            "Human",
            "Computer 1",
            "Computer 2",
            "Computer 3",
        };
        List<string> actualGameState = gameState.Players.Select(player => player.Name).ToList();
        CollectionAssert.AreEqual(expectedGameState, actualGameState);

        // Test that HumanPlayer's hand was dealt 5 cards
        Assert.AreEqual(5, gameState.HumanPlayer.Hand.Count());
    }

    [TestMethod]
    public void TestRandomPlayer()
    {
        // Test that a random player is returned and the current player is also not chosen as the returned player
        GameState gameState = new GameState("Human", 3, new Deck());
        Player.Random = new MockRandom() { ValueToReturn = 1 };
        Assert.AreEqual("Computer 2", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);

        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Human", gameState.RandomPlayer(gameState.Players.ToList()[1]).Name);
        Assert.AreEqual("Computer 1", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);
    }
}





