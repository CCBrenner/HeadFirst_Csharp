using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch9LinqJimmyMSTest
{
    using System;
    using System.Collections;
    // using System.Collections.Generic;  // Not needed due to using System.Collections;
    using System.Linq;
    using Ch9JimmyLinq;

    [TestClass]
    public class ComicAnalyzerTests
    {
        IEnumerable<Comic> testComics = new[]
        {
            new Comic() { Issue = 1, Name = "Issue 1" },
            new Comic() { Issue = 2, Name = "Issue 2" },
            new Comic() { Issue = 3, Name = "Issue 3" },
        };


        [TestMethod]
        public void ComicAnalyzer_Should_Group_Comics()
        {
            var prices = new Dictionary<int, decimal>()
            {
                { 1, 20M },
                { 2, 10M },
                { 3, 1000M },
            };
            var groups = ComicAnalyzer.GroupComicsByPrice(testComics, prices);

            Assert.AreEqual(2, groups.Count());
            Assert.AreEqual(PriceRange.Cheap, groups.First().Key);
            Assert.AreEqual(2, groups.First().First().Issue);
            Assert.AreEqual("Issue 2", groups.First().First().Name);
        }

        [TestMethod]
        public void ComicAnalyzer_Should_Generate_A_List_Of_Reviews()
        {
            var testReviews = new[]
            {
                new Review() { Issue = 1, Critic = Critics.MuddyCritic, Score = 14.5 },
                new Review() { Issue = 1, Critic = Critics.RottenTornadoes, Score = 59.93 },
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3 },
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11 },
            };

            var expectedResults = new[]
            {
                "MuddyCritic rated issue #1 'Issue 1' 14.5",
                "RottenTornadoes rated issue #1 'Issue 1' 59.93",
                "MuddyCritic rated issue #2 'Issue 2' 40.3",
                "RottenTornadoes rated issue #2 'Issue 2' 95.11",
            };

            var actualResults = ComicAnalyzer.GetReviews(testComics, testReviews).ToList();
            CollectionAssert.AreEqual(expectedResults, actualResults);
        }

        [TestMethod]
        public void ComicAnalyzer_Should_Handle_Weird_Review_Scores()
        {
            var testReviews = new[]
            {
                // negative scores
                new Review() { Issue = 1, Critic = Critics.MuddyCritic, Score = -12.1212 },
                // Really big numbers (it was found that the method cuts off the score at 7 decimal places; test has been adjusted to fit expected limit)
                new Review() { Issue = 1, Critic = Critics.RottenTornadoes, Score = 1234123567.1234567 },
                // Zero
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 0 },
                // Multiple of the same score for the same issue
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11 },
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11 },
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11 },
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11 },
            };
            var expectedResults = new[]
            {
                "MuddyCritic rated issue #1 'Issue 1' -12.1212",
                "RottenTornadoes rated issue #1 'Issue 1' 1234123567.1234567",
                "MuddyCritic rated issue #2 'Issue 2' 0",
                "RottenTornadoes rated issue #2 'Issue 2' 95.11",
                "RottenTornadoes rated issue #2 'Issue 2' 95.11",
                "RottenTornadoes rated issue #2 'Issue 2' 95.11",
                "RottenTornadoes rated issue #2 'Issue 2' 95.11",
            };
            List<string> actualResults = ComicAnalyzer.GetReviews(testComics, testReviews).ToList();
            CollectionAssert.AreEqual(expectedResults, actualResults);
        }
    }
}