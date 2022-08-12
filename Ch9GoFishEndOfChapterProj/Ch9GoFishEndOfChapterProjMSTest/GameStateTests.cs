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
        List<string> computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        GameState gameState = new GameState("Human", computerPlayerNames, new Deck());
        List<string> expectedGameState = new List<string>()
        {
            "Human",
            "Computer1",
            "Computer2",
            "Computer3",
        };
        List<string> actualGameState = gameState.Players.Select(player => player.Name).ToList();
        CollectionAssert.AreEqual(expectedGameState, actualGameState);

        // Test that HumanPlayer's hand was dealt 5 cards
        Console.WriteLine(gameState.HumanPlayer.Books.Count());
        Assert.AreEqual(5, gameState.HumanPlayer.Hand.Count());
    }

    [TestMethod]
    public void TestRandomPlayer()
    {
        // Test that a rnadom player is returned and the current player is also not chosen as the returned player
        List<string> computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        GameState gameState = new GameState("Human", computerPlayerNames, new Deck());
        Player.Random = new MockRandom() { ValueToReturn = 1 };
        Assert.AreEqual("Computer2", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);

        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Human", gameState.RandomPlayer(gameState.Players.ToList()[1]).Name);
        Assert.AreEqual("Computer1", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);
    }

    [TestMethod]
    public void TestPlayRound()
    {
        Deck deck = new Deck();
        deck.Clear();
        List<Card> cardsToAdd = new List<Card>()
        {
            // Cards that game will deal to Owen
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Nine, Suit.Spades),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Six, Suit.Hearts),

            // Cards that game will deal to Brittney
            new Card(Value.Six, Suit.Diamonds),
            new Card(Value.Six, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Six, Suit.Spades),

            // Two more cards in the deck for Owen to draw when he runs out
            new Card(Value.Queen, Suit.Hearts),
            new Card(Value.King, Suit.Spades),
        };
        foreach(Card card in cardsToAdd) deck.Add(card);  // Could also do: deck.AddRange(cardsToAdd);

        GameState gameState = new GameState("Owen", new List<string>() { "Brittney" }, deck);

        Player owen = gameState.HumanPlayer;
        Player brittney = gameState.Opponents.First();

        // Asks for value, opponent has it, card is transfered and books are pulled
        string message = gameState.PlayRound(owen, brittney, Value.Jack, deck);
        Assert.AreEqual($"Owen asked Brittney for Jacks{Environment.NewLine}Brittney has 1 Jack card", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(2, owen.Hand.Count());
        Assert.AreEqual(0, brittney.Books.Count());
        Assert.AreEqual(4, brittney.Hand.Count());

        // Asks for value, opponent doesn't have it, and deck has no cards but Human player does have cards left
        message = gameState.PlayRound(brittney, owen, Value.Six, deck);
        Assert.AreEqual($"Brittney asked Owen for Sixes{Environment.NewLine}Owen has 1 Six card", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(1, owen.Hand.Count());
        Assert.AreEqual(1, brittney.Books.Count());
        Assert.AreEqual(1, brittney.Hand.Count());

        // Asks for value, opponent doesn't have it, and player is able to draw card from deck
        message = gameState.PlayRound(owen, brittney, Value.Nine, deck);
        Console.WriteLine(deck.Count());
        Assert.AreEqual($"Owen asked Brittney for Nines{Environment.NewLine}Owen drew a card", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(2, owen.Hand.Count());
        Assert.AreEqual(1, brittney.Books.Count());
        Assert.AreEqual(1, brittney.Hand.Count());

        // Note: Since this is a method with many possible status outcomes, it good to reset the data for the remaining status tests

        deck.Clear();
        cardsToAdd = new List<Card>()
        {
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Three, Suit.Spades),

            new Card(Value.Six, Suit.Diamonds),
            new Card(Value.Six, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.King, Suit.Clubs),
            new Card(Value.Six, Suit.Spades),
        };
        deck.AddRange(cardsToAdd);

        gameState = new GameState("Owen", new List<string>() { "Brittney" }, deck);

        owen = gameState.HumanPlayer;
        brittney = gameState.Opponents.First();

        // Player has no cards to start their turn
        gameState.PlayRound(brittney, owen, Value.Jack, deck);  // These are to drain Owen's hand so that he has to GetNextHand() on the next line
        message = gameState.PlayRound(brittney, owen, Value.Three, deck);
        Assert.AreEqual($"{player} asked {playerToAsk} for {valuesToAskFor}{pluralAndIfSix}{Environment.NewLine}", message);
        Assert.AreEqual(0, owen.Books.Count());
        Assert.AreEqual(0, owen.Hand.Count());
        Assert.AreEqual(1, brittney.Books.Count());
        Assert.AreEqual(6, brittney.Hand.Count());


        cardsToAdd = new List<Card>()
        {
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Three, Suit.Spades),

            new Card(Value.Six, Suit.Diamonds),
            new Card(Value.Six, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.King, Suit.Clubs),
            new Card(Value.Six, Suit.Spades),
        };
        deck.Clear();
        deck.AddRange(cardsToAdd);
        gameState.Stock.Clear();
        gameState.Stock.AddRange(deck);

        gameState.PlayRound(owen, brittney, Value.Queen, deck);
        message = gameState.PlayRound(owen, brittney, Value.King, deck);
        Assert.AreEqual($"Owen asked Brittney for Kings{Environment.NewLine}The stock is out of cards", message);
        Assert.AreEqual(1, owen.Books.Count());
        Assert.AreEqual(3, owen.Hand.Count());

        // Asks for value, opponent has it, player gets a book and is left with no cards causing them to have to draw a full hand or as many as is left in the deck

        // Asks for value, opponnent doesn't have it, deck has one card and HumanPlayer gets a book and doesn't have any cards
        deck.Clear();
        gameState.PlayRound(brittney, owen, Value.Queen, deck);
        gameState.PlayRound(brittney, owen, Value.Nine, deck);
        gameState.PlayRound(brittney, owen, Value.King, deck);
        gameState.PlayRound(owen, brittney, Value.Two, deck);
    }

    [TestMethod]
    public void TestCheckForWinner()
    {
        List<string> computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
        Deck emptyDeck = new Deck();
        emptyDeck.Clear();
        GameState gameState = new GameState("Human", computerPlayerNames, new Deck());
        Assert.AreEqual("The winners are Human and Computer1 and Computer2 and Computer3", gameState.CheckForWinner());

        Deck computerTwoWinsDeck = new Deck();
        computerTwoWinsDeck.Clear();
        List<Card> cardsToAdd = new List<Card>()
        {
            // Cards that game will deal to Owen
            new Card(Value.Nine, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Nine, Suit.Spades),
            new Card(Value.Eight, Suit.Diamonds),
            new Card(Value.Six, Suit.Hearts),

            // Cards that game will deal to Brittney
            new Card(Value.Eight, Suit.Diamonds),
            new Card(Value.Nine, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Six, Suit.Spades),

            // Cards that game will deal to Owen
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Nine, Suit.Spades),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Jack, Suit.Clubs),

            // Cards that game will deal to Brittney
            new Card(Value.Nine, Suit.Diamonds),
            new Card(Value.Six, Suit.Clubs),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Jack, Suit.Clubs),
            new Card(Value.Eight, Suit.Spades),
        };
        computerTwoWinsDeck.AddRange(cardsToAdd);

        gameState = new GameState("Owen", computerPlayerNames, computerTwoWinsDeck);
        foreach(Player player in gameState.Players) player.AddCardsAndPullOutBooks(new List<Card>());
        Assert.AreEqual("The winner is Computer2", gameState.CheckForWinner());
    }
}





