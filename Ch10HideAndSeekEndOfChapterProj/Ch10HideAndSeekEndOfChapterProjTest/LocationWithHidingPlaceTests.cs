using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProjTest
{
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class LocationWithHidingPlaceTests
    {
        [TestMethod]
        public void TestHiding()
        {
            // The contructor sets the Name and HidingPace properties
            var hidingLocation = new LocationWithHidingPlace("Room", "under the bed");
            Assert.AreEqual("Room", hidingLocation.Name);
            Assert.AreEqual("Room", hidingLocation.ToString());
            Assert.AreEqual("under the bed", hidingLocation.HidingPlace);

            // Hide two opponents in the room then check the room
            Opponent opponent1 = new Opponent("Opponent1");
            Opponent opponent2 = new Opponent("Opponent2");
            hidingLocation.Hide(opponent1);
            hidingLocation.Hide(opponent2);
            CollectionAssert.AreEqual(new List<Opponent>() { opponent1, opponent2 }, hidingLocation.CheckHidingPlace().ToList());

            // HidingPlace should now be empty
            CollectionAssert.AreEqual(new List<Opponent>(), hidingLocation.CheckHidingPlace().ToList());
        }
    }
}
