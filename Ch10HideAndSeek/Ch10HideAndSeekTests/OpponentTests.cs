using System;

namespace Ch10HideAndSeek
{
    public class MockRandomWithValueList : System.Random
    {
        public MockRandomWithValueList(IEnumerable<int> values) =>
            valuesToReturn = new Queue<int>(values);
        private Queue<int> valuesToReturn;
        public int NextValue()
        {
            var nextValue = valuesToReturn.Dequeue();
            valuesToReturn.Enqueue(nextValue);
            return nextValue;
        }
        public override int Next() => NextValue();
        public override int Next(int maxValue) => Next(0, maxValue);
        public override int Next(int minValue, int maxValue)
        {
            int next = NextValue();
            return next >= minValue && next < maxValue ? next : minValue;
        }
    }

    [TestClass]
    public class OpponentTests
    {
        [TestMethod]
        public void TestOpponentHiding()
        {
            Opponent opponent1 = new Opponent("Opponent1");
            Assert.AreEqual("Opponent1", opponent1.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1 });
            opponent1.Hide();
            LocationWithHidingPlace? downstrairsBathroom = House.GetLocationByName("Downstairs Bathroom") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent1 }, downstrairsBathroom.CheckHidingPlace().ToList());

            Opponent opponent2 = new Opponent("Opponent2");
            Assert.AreEqual("Opponent2", opponent2.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1, 2, 3 });
            opponent2.Hide();
            LocationWithHidingPlace kitchen = House.GetLocationByName("Kitchen") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent2 }, kitchen.CheckHidingPlace().ToList());
        }
    }
}
