using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.TableBuilders;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class CsvTableBuilderTest
    {
        private StringWriter _outputStream;
        private CsvTableBuilder _target;

        [SetUp]
        public void SetUp()
        {
            _outputStream = new StringWriter();
            _target = new CsvTableBuilder(_outputStream);
        }

        [TearDown]
        public void TearDown()
        {
            _outputStream.Dispose();
        }

        [Test]
        public void One_row_one_column_table_rendered_with_the_right_csv()
        {
            _target.BeginTable(1, 1);
            _target.AddRow(new int[] { 3 });
            _target.EndTable();

            string result = _outputStream.ToString().Replace(" ", "");

            string expectedResult =
                  ";1" + System.Environment.NewLine
                + "1;3" + System.Environment.NewLine;

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

        [Test]
        public void Two_rows_tree_columns_table_rendered_with_the_right_csv()
        {
            _target.BeginTable(2, 3);
            _target.AddRow(new int[] { 1, 2, 3 });
            _target.AddRow(new int[] { 4, 5, 6 });
            _target.EndTable();

            string result = _outputStream.ToString().Replace(" ", "");

            string expectedResult =
                  ";1;2;3" + System.Environment.NewLine
                + "1;1;2;3" + System.Environment.NewLine
                + "2;4;5;6" + System.Environment.NewLine;

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }
    }
}
