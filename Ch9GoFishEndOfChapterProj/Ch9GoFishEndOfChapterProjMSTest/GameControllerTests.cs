using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Ch9GoFishEndOfChapterProj;
using System.Linq;

namespace Ch9GoFishEndOfChapterProjMSTest;

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
        GameController gameController = new GameController("Human", new List<string>() { "Player1", "Player2", "Player3" });
        Assert.AreEqual("Starting a new game with players Human, Player1, Player2, Player3", gameController.Status);
    }

    [TestMethod]
    public void TestNextRound()
    {
        // *** This test covers both NextRound() and ComputerPlayersPlayNextRound() ***

        GameController gameController = new GameController("Owen", new List<string>() { "Brittney" });
        // gameController.gameState.Stock.Sort();
        /*
        gameController.gameState.HumanPlayer.Hand = new List<Card>()
        {
            new Card(Value.Ace, Suit.Spades),
            new Card(Value.Two, Suit.Spades),
            new Card(Value.Three, Suit.Spades),
            new Card(Value.Four, Suit.Spades),
            new Card(Value.Five, Suit.Spades),
        };
        gameController.gameState.Opponents.First().Hand = new List<Card>()
        {
            new Card(Value.Six, Suit.Spades),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Eight, Suit.Spades),
            new Card(Value.Nine, Suit.Spades),
            new Card(Value.Ten, Suit.Spades),
        };*/


        foreach (Player player in gameController.gameState.Players)
        {
            Console.WriteLine($"\n{player}");
            foreach (Card card in player.Hand) Console.WriteLine($"{card}");
        }

        Player.Random = new MockRandom() { ValueToReturn = 0 };

        // This status return will let us konw that everything in the method is working correctly
        gameController.NextRound(gameController.Opponents.First(), Value.Six);
        Assert.AreEqual($"Owen asked Brittney for Sixes" +
            $"{Environment.NewLine}Brittney has 1 Six card" +
            $"{Environment.NewLine}Brittney asked Owen for Sevens" +
            $"{Environment.NewLine}Brittney drew a card" +
            $"{Environment.NewLine}Owen has 6 cards an 0 books" +
            $"{Environment.NewLine}Brittney has 5 cards and 0 books" +
            $"{Environment.NewLine}The stock has 41 cards" +
            $"{Environment.NewLine}", gameController.Status);
    }

    [TestMethod]
    public void TestNewGame()
    {
        Player.Random = new MockRandom() { ValueToReturn = 0 };
        GameController gameController = new GameController("Owen", new List<string>(){ "Brittney" });
        gameController.NextRound(gameController.Opponents.First(), Value.Six);
        gameController.NewGame();
        Assert.AreEqual("Owen", gameController.HumanPlayer.Name);
        Assert.AreEqual("Brittney", gameController.Opponents.First().Name);
        Assert.AreEqual("Starting anew game", gameController.Status);
    }
}


