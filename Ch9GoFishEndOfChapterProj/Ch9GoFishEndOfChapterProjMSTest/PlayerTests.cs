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
        var player = new Player("Owen", new List<Card>());
        player.GetNextHand(new Deck());
        var expected = new Deck().Take(5).Select(card => card.ToString()).ToList();
        var actual = player.Hand.Select(card => card.ToString()).ToList();
        CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void TestDoYouHaveAny()
    {
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

        Assert.AreEqual(3, player.Hand.Count());
    }
    [TestMethod]
    public void TestAddCardsAndPullOutBooks()
    {

    }
    [TestMethod]
    public void TestDrawCard()
    {

    }
    [TestMethod]
    public void TestRandomValueFromHand()
    {

    }
}

public class MockRandom : System.Random
{
    public int ValueToReturn { get; set; } = 0;
    public override int Next() => ValueToReturn;
    public override int Next(int maxValue) => ValueToReturn;
    public override int Next(int minValue, int maxValue) => ValueToReturn;


}
