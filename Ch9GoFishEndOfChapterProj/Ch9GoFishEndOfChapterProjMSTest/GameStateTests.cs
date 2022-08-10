using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Ch9GoFishEndOfChapterProj;

namespace Ch9GoFishEndOfChapterProjMSTest;

[TestClass]
public class GameStateTests
{
    [TestMethod]
    public void TestConstructor()
    {
        // Test constructor assignment of HumanPlayer, Players, Opponents, and Stock
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        var gameState = new GameState("Human", computerPlayerNames, new Deck());
        var expectedGameState = new List<string>()
        {
            "Human",
            "Computer1",
            "Computer2",
            "Computer3",
        };
        var actualGameState = gameState.Players.Select(player => player.Name).ToList();
        CollectionAssert.AreEqual(expectedGameState, actualGameState);

        // Test that HumanPlayer's hand was dealt 5 cards
        Assert.AreEqual(5, gameState.HumanPlayer.Hand.Count());
    }

    [TestMethod]
    public void TestRandomPlayer()
    {
        // Test [?] - What fucntion is MockRandom performing here?
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        var gameState = new GameState("Human", computerPlayerNames, new Deck());
        Player.Random = new MockRandom() { ValueToReturn = 1 };
        Assert.AreEqual("Computer 2", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);

        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Human", gameState.RandomPlayer(gameState.Players.ToList()[1]).Name);
        Assert.AreEqual("Computer 1", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);
    }

    [TestMethod]
    public void TestPlayRound()
    {
        var deck = new Deck();
        deck.Clear();
        var cardsToAdd = new List<Card>()
        {
            // Cards that game will deal to Owen
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Six, Suit.Spades),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Six, Suit.Hearts),

            // Cards that game will deal to Brittney
            new Card(Value.Six, Suit.Diamonds),
            new Card(Value.Six, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Nine, Suit.Spades),

            // Two more cards in the deck for Owen to draw when he runs out
            new Card(Value.Queen, Suit.Hearts),
            new Card(Value.King, Suit.Spades),
        };
        foreach(var card in cardsToAdd) deck.Add(card);

        var gameState = new GameState("Owen", new List<string>() { "Brittney" }, deck);

        var owen = gameState.HumanPlayer;
        var brittney = gameState.Opponents.First();

        Assert.AreEqual("Owen", owen.Name);
        Assert.AreEqual(5, owen.Hand.Count());
        Assert.AreEqual("Brittney", brittney.Name);
        Assert.AreEqual(5, brittney.Hand.Count());

        var message = gameState.PlayRound(owen, brittney, Value.Jack, deck);
        Assert.AreEqual("Owen asked Brittney for Jacks" + Environment.NewLine + "Brittney has 1 Jack card", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(2, owen.Hand.Count());
        Assert.AreEqual(0, brittney.Books.Count());
        Assert.AreEqual(4, brittney.Hand.Count());

        message = gameState.PlayRound(brittney, owen, Value.Six, deck);
        Assert.AreEqual("Brittney asked Owen for Sixes" + Environment.NewLine + "Brittney has 1 Jack card", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(2, owen.Hand.Count());
        Assert.AreEqual(1, brittney.Books.Count());
        Assert.AreEqual(2, brittney.Hand.Count());

        message = gameState.PlayRound(owen, brittney, Value.Queen, deck);
        Assert.AreEqual("Owen asked Brittney for Jacks" + Environment.NewLine + "The stock is out of cards", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(2, owen.Hand.Count());
    }

    [TestMethod]
    public void TestCheckForWinner()
    {
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        var emptyDeck = new Deck();
        emptyDeck.Clear();
        var gameState = new GameState("Human", computerPlayerNames, new Deck());
        Assert.AreEqual("The winners are Human and Computer1 and Computer2 and Computer3", gameState.CheckForWinner());
    }
}





