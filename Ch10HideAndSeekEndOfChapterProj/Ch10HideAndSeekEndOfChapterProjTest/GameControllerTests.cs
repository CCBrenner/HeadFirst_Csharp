using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    using Ch10HideAndSeekEndOfChapterProj;
    [TestClass]
    public class GameControllerTests
    {
        GameController gameController;

        [TestInitialize]
        public void Initialize() => gameController = new GameController();
        [TestMethod]
        public void TestMovement()
        {
            Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
            Assert.IsFalse(gameController.Move(Direction.Up));

            Assert.IsTrue(gameController.Move(Direction.East));
            Assert.AreEqual("Hallway", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Up));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Northwest));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.North));
            Assert.AreEqual("Master Bedroom", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Southeast));
            Assert.AreEqual("Landing", gameController.CurrentLocation.Name);
        }
        /*
        public void TestParseInput()
        {
            throw new NotImplementedException();
        }
        */
    }
}
