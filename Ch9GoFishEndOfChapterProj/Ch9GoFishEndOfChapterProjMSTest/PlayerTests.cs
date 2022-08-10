using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Ch9GoFishEndOfChapterProj;

namespace Ch9GoFishEndOfChapterProjMSTest;

[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void TestGetNextHand()
    {
        // Test for correct number of cards and correct cards added to hand when starting with an empty hand
        var player = new Player("Owen", new List<Card>());
        player.GetNextHand(new Deck());
        var expected = new Deck().Take(5).Select(card => card.ToString()).ToList();
        var actual = player.Hand.Select(card => card.ToString()).ToList();
        CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void TestDoYouHaveAny()
    {
        // Test for correct return of items matched
        IEnumerable<Card> cards = new List<Card>()
        {
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Three, Suit.Clubs),
            new Card(Value.Three, Suit.Hearts),
            new Card(Value.Four, Suit.Diamonds),
            new Card(Value.Three, Suit.Diamonds),
            new Card(Value.Jack, Suit.Clubs),
        };
        Player player = new Player("Owen", cards);
        var threes = player.DoYouHaveAny(Value.Three, new Deck())
            .Select(card => card.ToString())
            .ToList();
        CollectionAssert.AreEqual(new List<string>()
        {
            "Three of Clubs",
            "Three of Hearts",
            "Three of Diamonds",
        }, threes);

        // Test for the items above actually having been taken out of the player's hand
        Assert.AreEqual(3, player.Hand.Count());

        // Test for correct return of items matched after having already done so once before
        var jacks = player.DoYouHaveAny(Value.Jack, new Deck())
            .Select(card => card.ToString())
            .ToList();

        CollectionAssert.AreEqual(new List<string>() {
            "Jack of Spades",
            "Jack of Clubs",
        }, jacks);

        // Test player's Status property plus use of plural when quantity is 1 (for cards and books)
        Assert.AreEqual("Owen has 1 card and 0 books.", player.Status);

    }
    [TestMethod]
    public void TestAddCardsAndPullOutBooks()
    {
        // Configure and test for starting with no books
        IEnumerable<Card> cards = new List<Card>()
        {
            new Card(Value.Jack, Suit.Spades),
            new Card(Value.Three, Suit.Clubs),
            new Card(Value.Jack, Suit.Hearts),
            new Card(Value.Three, Suit.Hearts),
            new Card(Value.Four, Suit.Diamonds),
            new Card(Value.Jack, Suit.Diamonds),
            new Card(Value.Jack, Suit.Clubs),
        };
        Player player = new Player("Owen", cards);
        Assert.AreEqual(0, player.Books.Count());

        // Test adding cards and pulling out the two books that exist in player's hand
        var cardsToAdd = new List<Card>()
        {
            new Card(Value.Three, Suit.Spades),
            new Card(Value.Three, Suit.Diamonds),
        };
        player.AddCardsAndPullOutBooks(cardsToAdd);
        var books = player.Books.ToList();
        CollectionAssert.AreEqual(new List<Value>() { Value.Three, Value.Jack }, books);

        // Test state of hand after adding cards and pulling books from hand
        var hand = player.Hand.Select(card => card.ToString()).ToList();
        CollectionAssert.AreEqual(new List<string>() { "Four of Diamonds" }, hand);

        // Test player.Status for player's state after all of these actions have taken place
        Assert.AreEqual("Owen has 1 card and 2 books.", player.Status);
    }
    [TestMethod]
    public void TestDrawCard()
    {
        // Test drawing only one card
        Player player = new Player("Owen", new List<Card>());
        player.DrawCard(new Deck());
        Assert.AreEqual(player.Hand.Count(), 1);

        // Test to check if first card pulled is correct based on deck order
        Assert.AreEqual("Ace of Spades", player.Hand.First().ToString());
    }
    [TestMethod]
    public void TestRandomValueFromHand()
    {
        // Test "Ace"
        var player = new Player("Owen", new Deck());
        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Ace", player.RandomValueFromHand().ToString());

        // Test "Two"
        Player.Random = new MockRandom() { ValueToReturn = 4 };
        Assert.AreEqual("Two", player.RandomValueFromHand().ToString());

        // Test "Three"
        Player.Random = new MockRandom() { ValueToReturn = 8 };
        Assert.AreEqual("Three", player.RandomValueFromHand().ToString());
    }
}

public class MockRandom : System.Random
{
    public int ValueToReturn { get; set; } = 0;
    public override int Next() => ValueToReturn;
    public override int Next(int maxValue) => ValueToReturn;
    public override int Next(int minValue, int maxValue) => ValueToReturn;


}
