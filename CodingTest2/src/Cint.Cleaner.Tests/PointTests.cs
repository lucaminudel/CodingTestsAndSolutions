using System;
using NUnit.Framework;

namespace Cint.Cleaner.Core.Tests
{
    [TestFixture]
    public class PointTests
    {

        [Test]
        public void ConvertFromPointOfCompassTest()
        {
            Point target;

            target = (Point)PointOfCompass.North;
            Assert.AreEqual(0, target.X);
            Assert.AreEqual(1, target.Y);


            target = (Point)PointOfCompass.South;
            Assert.AreEqual(0, target.X);
            Assert.AreEqual(-1, target.Y);


            target = (Point)PointOfCompass.East;
            Assert.AreEqual(1, target.X);
            Assert.AreEqual(0, target.Y);


            target = (Point)PointOfCompass.West;
            Assert.AreEqual(-1, target.X);
            Assert.AreEqual(0, target.Y);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void ConvertFromPointOfCompassErrorTest()
        {
            PointOfCompass outOfRangeEnumValue = (PointOfCompass) 15;
            
            Point target  = (Point)outOfRangeEnumValue;
        }

        [Test]
        public void MultiplyPointByScalarTest()
        {
            Point point;
            Point target;

            point = new Point(-7, 19);
            target = point * 3;
            Assert.AreEqual(-21, target.X);
            Assert.AreEqual(57, target.Y);

        }

        [Test]
        public void PointAdditionTest()
        {
            Point a = new Point(2, 19);
            Point b = new Point(3, -4);

            Point target = a + b;

            Assert.AreEqual(5, target.X);
            Assert.AreEqual(15, target.Y);
        }

    }

}
