using Cint.Cleaner.Console;
using Cint.Cleaner.Console.InputOutput;
using NMock2;
using NUnit.Framework;

namespace Cint.Cleaner.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        Mockery _mocks;
        private IStandardInputLineReader _mockLineReader;
        private IStandardOutputLineWriter _mockLineWriter;

        [SetUp]
        public void SetUp()
        {
            _mocks = new Mockery();

            _mockLineReader = _mocks.NewMock<IStandardInputLineReader>();
            _mockLineWriter = _mocks.NewMock<IStandardOutputLineWriter>();
        }

        [TearDown]
        public void TearDown()
        {
            _mocks.VerifyAllExpectationsHaveBeenMet();
            _mocks = null;
            _mockLineReader = null;
            _mockLineWriter = null;
        }

        [Test]
        public void PdfSampleTest()
        {
            // 2
            // 10 22
            // E 2
            // N 1

            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("2"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("10 22"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 2"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("N 1"));

            Expect.Once.On(_mockLineWriter).Method("WriteLine").With("=> Cleaned: 4");

            Controller controller = new Controller(_mockLineReader, _mockLineWriter);
            controller.CleanUpTheSpacelySpaceSprocketsOffice();
        }

        [Test]
        public void OneCommandTest()
        {
            // 1
            // -10000 -10000
            // E 20000

            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("-10000 -10000"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 20000"));

            Expect.Once.On(_mockLineWriter).Method("WriteLine").With("=> Cleaned: 20001");

            Controller controller = new Controller(_mockLineReader, _mockLineWriter);
            controller.CleanUpTheSpacelySpaceSprocketsOffice();
        }

        [Test]
        public void NonUniquePlacesTest()
        {
            // 8
            // 0 0
            // E 1
            // N 1
            // W 1
            // S 1
            // E 1
            // N 1
            // W 1
            // S 1

            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("8"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("0 0"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("N 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("W 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("S 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("N 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("W 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("S 1"));

            Expect.Once.On(_mockLineWriter).Method("WriteLine").With("=> Cleaned: 4");

            Controller controller = new Controller(_mockLineReader, _mockLineWriter);
            controller.CleanUpTheSpacelySpaceSprocketsOffice();
        }

        [Test]
        public void AnotherNonUniquePlacesTest()
        {
            // 6
            // 10 -100
            // E 3
            // N 2
            // W 2
            // S 4
            // E 1
            // N 5

            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("6"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("10 -100"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 3"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("N 2"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("W 2"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("S 4"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("E 1"));
            Expect.Once.On(_mockLineReader).Method("ReadLine").Will(Return.Value("N 5"));

            Expect.Once.On(_mockLineWriter).Method("WriteLine").With("=> Cleaned: 15");

            Controller controller = new Controller(_mockLineReader, _mockLineWriter);
            controller.CleanUpTheSpacelySpaceSprocketsOffice();
        }

    }
}
