using NUnit.Framework;
using ReQtest.MultiplicationTable.Application;
using ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles;

namespace ReQtest.MultiplicationTable.Tests.Acceptance
{
    [TestFixture]
    public class CsvMultiplicationTableTest
    {
        private Helper _helper;
        private Program _controller;

        [SetUp]
        public void SetUp()
        {
            _helper = new Helper();
            _controller = new Program();
        }

        [Test]
        public void Create_a_5_per_15_csv_multiplication_table()
        {
            const string expectedFilename = "expected_multiply_5_15.csv";
            const string actualFilename = "multiply_5_15.csv";
            _helper.DeleteFileIfExixts(actualFilename);

            _controller.Execute(new string[] { "5", "15", "csv" });

            using (_helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(_helper.FilesAreEqual(expectedFilename, actualFilename));                
            }
        }

        [Test]
        public void Create_a_1_per_1_csv_multiplication_table()
        {
            const string expectedFilename = "expected_multiply_1_1.csv";
            const string actualFilename = "multiply_1_1.csv";
            _helper.DeleteFileIfExixts(actualFilename);

            _controller.Execute(new string[] { "1", "1", "csv" });

            using (_helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(_helper.FilesAreEqual(expectedFilename, actualFilename));
            }
        }

        [Test]
        public void Create_a_20_per_20_csv_multiplication_table()
        {
            const string expectedFilename = "expected_multiply_20_20.csv";
            const string actualFilename = "multiply_20_20.csv";
            _helper.DeleteFileIfExixts(actualFilename);

            _controller.Execute(new string[] { "20", "20", "csv" });

            using (_helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(_helper.FilesAreEqual(expectedFilename, actualFilename));
            }
        }

    }
}
