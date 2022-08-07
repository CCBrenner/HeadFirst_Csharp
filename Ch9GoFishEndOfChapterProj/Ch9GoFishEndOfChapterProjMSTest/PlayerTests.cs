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
        CollectionAssert.AreEqual(
            new Deck().Take(5).Select(card => card.ToString()).ToList(),
            player.Hand.Select(card => card.ToString()).ToList()
            );
    }
    [TestMethod]
    public void TestDoYouHaveAny()
    {

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
