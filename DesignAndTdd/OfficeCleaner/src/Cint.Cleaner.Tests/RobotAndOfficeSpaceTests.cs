using NMock2;
using NUnit.Framework;

namespace Cint.Cleaner.Core.Tests
{
    [TestFixture]
    public class RobotAndOfficeSpaceTests
    {
        private Mockery _mocks;
        private IOfficeSpace _mockSpacelySpaceSprocketsOffice;

        [SetUp]
        public void SetUp()
        {
            _mocks = new Mockery();
            _mockSpacelySpaceSprocketsOffice = _mocks.NewMock<IOfficeSpace>();
        }

        [TearDown]
        public void TearDown()
        {
            _mocks.VerifyAllExpectationsHaveBeenMet();
            _mocks = null;
        }

        [Test]
        public void RobotCleanTheStartingPlace()
        {
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(10, 100));
            Robot RosieTheRobotMaid = new Robot(10, 100, _mockSpacelySpaceSprocketsOffice);
        }

        [Test]
        public void RobotCleansEverywhereAsItMovesForwardTest()
        {

            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(0, 0));

            // N 3
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(0, 1));
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(0, 2));
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(0, 3));

            // E 2
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(1, 3));
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(2, 3));

            // W 1
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(1, 3));

            Robot RosieTheRobotMaid = new Robot(0, 0, _mockSpacelySpaceSprocketsOffice);
            RosieTheRobotMaid.MoveForward(new MoveForwardCommand(PointOfCompass.North, 3));
            RosieTheRobotMaid.MoveForward(new MoveForwardCommand(PointOfCompass.East, 2));
            RosieTheRobotMaid.MoveForward(new MoveForwardCommand(PointOfCompass.West, 1));
        }

        [Test]
        public void RobotCountUniquePlacesCleanTest()
        {
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).Method("SetPlaceCleaned").With(new Point(0, 0));
            Expect.Once.On(_mockSpacelySpaceSprocketsOffice).GetProperty("CleanedPlacesCount").Will(Return.Value(123L));

            Robot RosieTheRobotMaid = new Robot(0, 0, _mockSpacelySpaceSprocketsOffice);

            Assert.AreEqual(123, RosieTheRobotMaid.CleanedPlacesCount);
        }

    }
}
