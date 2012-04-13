using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.TableBuilders;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class ConsoleTableBuilderTest
    {
        private StringWriter _outputStream;
        private ConsoleTableBuilder _target;

        [SetUp]
        public void SetUp()
        {
            _outputStream = new StringWriter();
            _target = new ConsoleTableBuilder(_outputStream);
        }

        [TearDown]
        public void TearDown()
        {
            _outputStream.Dispose();
        }

        [Test]
        public void One_row_one_column_table_rendered_with_the_right_text()
        {
            _target.BeginTable(1, 1);
            _target.AddRow(new int[] { 3 });
            _target.EndTable();

            string result = _outputStream.ToString();

            string expectedResult =
                  "       1" + System.Environment.NewLine
                + "   1   3" + System.Environment.NewLine;

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

        [Test]
        public void Two_rows_tree_columns_table_rendered_with_the_right_text()
        {
            _target.BeginTable(2, 3);
            _target.AddRow(new int[] { 1, 2, 3 });
            _target.AddRow(new int[] { 40, 50, 60 });
            _target.EndTable();

            string result = _outputStream.ToString();

            string expectedResult =
                  "       1   2   3" + System.Environment.NewLine
                + "   1   1   2   3" + System.Environment.NewLine
                + "   2  40  50  60" + System.Environment.NewLine;

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }
    }
}
