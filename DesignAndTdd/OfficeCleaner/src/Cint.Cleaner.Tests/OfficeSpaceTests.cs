using NUnit.Framework;

namespace Cint.Cleaner.Core.Tests
{
    [TestFixture]
    public class OfficeSpaceTests
    {
        private OfficeSpace _target;

        [SetUp]
        public void SetUp()
        {
            _target = new OfficeSpace();
        }

        [TearDown]
        public void TearDown()
        {
            _target = null;
        }

        [Test]
        public void CleanedPlacesCountStartFromZeroTest()
        {
            Assert.AreEqual(0, _target.CleanedPlacesCount);
        }

        [Test]
        public void CleanFourUniquePlacesTest()
        {
            _target.SetPlaceCleaned(new Point(-100000, -100000));
            Assert.AreEqual(1, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(new Point(100000, -100000));
            Assert.AreEqual(2, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(new Point(100000, 100000));
            Assert.AreEqual(3, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(new Point(-100000, 100000));
            Assert.AreEqual(4, _target.CleanedPlacesCount);

        }

        [Test]
        public void CleanTwoUniquePlacesTwiceTest()
        {
            Point firstPlace = new Point(-100000, -100000);
            Point secondPlace = new Point(100000, -100000);

            _target.SetPlaceCleaned(firstPlace);
            Assert.AreEqual(1, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(secondPlace);
            Assert.AreEqual(2, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(secondPlace);
            Assert.AreEqual(2, _target.CleanedPlacesCount);

            _target.SetPlaceCleaned(firstPlace);
            Assert.AreEqual(2, _target.CleanedPlacesCount);


        }

    }
}
