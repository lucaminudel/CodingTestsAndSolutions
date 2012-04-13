using NUnit.Framework;

namespace Cint.Cleaner.Core.Tests
{
    [TestFixture]
    public class RobotTests
    {
        private Robot _target;

        [TearDown]
        public void TearDown()
        {
            _target = null;
        }

        [Test]
        public void StartingPositionTest()
        {

            _target = new Robot(10, 22);
            Assert.AreEqual(10, _target.CurrentPositionX);
            Assert.AreEqual(22, _target.CurrentPositionY);

            _target = new Robot(-100000, -100000);
            Assert.AreEqual(-100000, _target.CurrentPositionX);
            Assert.AreEqual(-100000, _target.CurrentPositionY);

            _target = new Robot(100000, 100000);
            Assert.AreEqual(100000, _target.CurrentPositionX);
            Assert.AreEqual(100000, _target.CurrentPositionY);
        }

        [Test]
        public void CurrentPositionAfterMoveEast2StepsTest()
        {
            int x = -3;
            int y = 100;
            _target = new Robot(x, y);
            
            MoveForwardCommand moveEastBy2Command = new MoveForwardCommand(PointOfCompass.East, 2);

            _target.MoveForward(moveEastBy2Command);

            Assert.AreEqual(-1, _target.CurrentPositionX);
            Assert.AreEqual(100, _target.CurrentPositionY);
        }

        [Test]
        public void CurrentPositionAfterMoveWest3StepsTest()
        {
            int x = -3;
            int y = 100;
            _target = new Robot(x, y);

            MoveForwardCommand moveWestBy3Command = new MoveForwardCommand(PointOfCompass.West, 3);

            _target.MoveForward(moveWestBy3Command);

            Assert.AreEqual(-6, _target.CurrentPositionX);
            Assert.AreEqual(100, _target.CurrentPositionY);        

        }

        [Test]
        public void CurrentPositionAfterMoveNorth10StepsTest()
        {
            int x = -3;
            int y = 100;
            _target = new Robot(x, y);

            MoveForwardCommand moveNorthBy10Command = new MoveForwardCommand(PointOfCompass.North, 10);

            _target.MoveForward(moveNorthBy10Command);

            Assert.AreEqual(-3, _target.CurrentPositionX);
            Assert.AreEqual(110, _target.CurrentPositionY);

        }

        [Test]
        public void CurrentPositionAfterMoveSouth100ThousandOneHundredStepsTest()
        {
            int x = -3;
            int y = 100;
            _target = new Robot(x, y);

            MoveForwardCommand moveSouthBy100ThousandOneHundredCommand = new MoveForwardCommand(PointOfCompass.South, 100100);

            _target.MoveForward(moveSouthBy100ThousandOneHundredCommand);

            Assert.AreEqual(-3, _target.CurrentPositionX);
            Assert.AreEqual(-100000, _target.CurrentPositionY);

        }

        [Test]
        public void CurrentPositionBetweenManyMoveCommandsTest()
        {
            _target = new Robot(-100000, -100000);

            _target.MoveForward(new MoveForwardCommand(PointOfCompass.North, 200000));
            Assert.AreEqual(-100000, _target.CurrentPositionX);
            Assert.AreEqual(100000, _target.CurrentPositionY);


            _target.MoveForward(new MoveForwardCommand(PointOfCompass.East, 200000));
            Assert.AreEqual(100000, _target.CurrentPositionX);
            Assert.AreEqual(100000, _target.CurrentPositionY);


            _target.MoveForward(new MoveForwardCommand(PointOfCompass.South, 200000));
            Assert.AreEqual(100000, _target.CurrentPositionX);
            Assert.AreEqual(-100000, _target.CurrentPositionY);


            _target.MoveForward(new MoveForwardCommand(PointOfCompass.West, 200000));
            Assert.AreEqual(-100000, _target.CurrentPositionX);
            Assert.AreEqual(-100000, _target.CurrentPositionY);
        }
    }
}
