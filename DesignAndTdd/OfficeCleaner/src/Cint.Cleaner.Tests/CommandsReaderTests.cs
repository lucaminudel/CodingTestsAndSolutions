using Cint.Cleaner.Core;
using NUnit.Framework;
using NMock2;

namespace Cint.Cleaner.Console.InputOutput.Tests
{
    [TestFixture]
    public class CommandsReaderTests
    {
        Mockery _mocks;

        [SetUp]
        public void SetUp()
        {
            _mocks = new Mockery();
        }

        [TearDown]
        public void TearDown()
        {
            _mocks.VerifyAllExpectationsHaveBeenMet();
            _mocks = null;
        }

        [Test]
        public void StartingPositionTest()
        {
            IStandardInputLineReader stubLineReader = _mocks.NewMock<IStandardInputLineReader>();

            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("0"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("10 22"));

            CommandsReader target = new CommandsReader(stubLineReader);
            target.ReadCommandFromStandardInput();

            Assert.AreEqual(10, target.StartingPositionX);
            Assert.AreEqual(22, target.StartingPositionY);
            
        }

        [Test]
        public void ZeroMoveForwardCommandTestTest()
        {
            IStandardInputLineReader stubLineReader = _mocks.NewMock<IStandardInputLineReader>();

            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("0"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("10 22"));

            CommandsReader target = new CommandsReader(stubLineReader);
            target.ReadCommandFromStandardInput();

            Assert.AreEqual(0, target.MoveForwardCommands.Count);
        }


        [Test]
        public void OneMoveForwardCommandTest()
        {
            IStandardInputLineReader stubLineReader = _mocks.NewMock<IStandardInputLineReader>();

            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("1"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("10 22"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("E -1"));

            CommandsReader target = new CommandsReader(stubLineReader);
            target.ReadCommandFromStandardInput();

            Assert.AreEqual(1, target.MoveForwardCommands.Count);
            Assert.AreEqual(-1, target.MoveForwardCommands.Peek().Steps);
            Assert.AreEqual(PointOfCompass.East, target.MoveForwardCommands.Peek().Direction);
        }

        [Test]
        public void TreeMoveForwardCommandTest()
        {
            IStandardInputLineReader stubLineReader = _mocks.NewMock<IStandardInputLineReader>();

            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("3"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("10 22"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("W 43"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("S -1004"));
            Expect.Once.On(stubLineReader).Method("ReadLine").Will(Return.Value("N 2"));

            CommandsReader target = new CommandsReader(stubLineReader);
            target.ReadCommandFromStandardInput();

            Assert.AreEqual(3, target.MoveForwardCommands.Count);

            Assert.AreEqual(PointOfCompass.West, target.MoveForwardCommands.Peek().Direction);
            Assert.AreEqual(43, target.MoveForwardCommands.Peek().Steps);
            target.MoveForwardCommands.Dequeue();

            Assert.AreEqual(PointOfCompass.South, target.MoveForwardCommands.Peek().Direction);
            Assert.AreEqual(-1004, target.MoveForwardCommands.Peek().Steps);
            target.MoveForwardCommands.Dequeue();

            Assert.AreEqual(PointOfCompass.North, target.MoveForwardCommands.Peek().Direction);
            Assert.AreEqual(2, target.MoveForwardCommands.Peek().Steps);
        }



    }
}

