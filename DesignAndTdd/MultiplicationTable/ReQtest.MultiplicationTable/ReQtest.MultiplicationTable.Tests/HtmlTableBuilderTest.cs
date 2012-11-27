using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.TableBuilders;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class HtmlTableBuilderTest
    {
        private StringWriter _outputStream;
        private HtmlTableBuilder _target;
 
        [SetUp]
        public void SetUp()
        {
            _outputStream = new StringWriter();
            _target = new HtmlTableBuilder(_outputStream);
        }

        [TearDown]
        public void TearDown()
        {
            _outputStream.Dispose();
        }

        [Test]
        public void One_row_one_column_table_rendered_with_the_right_html()
        {
            _target.BeginTable(1,1);
            _target.AddRow(new int[] { 3 });
            _target.EndTable();

            string result = _outputStream.ToString().Replace(" ", "").Replace(System.Environment.NewLine, "");

            string expectedResult = 
                  "<html><body><table>" 
                + "<tr><td></td><td>1</td></tr>"
                + "<tr><td>1</td><td>3</td></tr>"
                + "</table></body></html>";

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

        [Test]
        public void Two_rows_tree_columns_table_rendered_with_the_right_html()
        {
            _target.BeginTable(2, 3);
            _target.AddRow(new int[] { 1, 2, 3 });
            _target.AddRow(new int[] { 4, 5, 6 });
            _target.EndTable();

            string result = _outputStream.ToString().Replace(" ", "").Replace(System.Environment.NewLine, "");

            string expectedResult =
                  "<html><body><table>"
                + "<tr><td></td><td>1</td><td>2</td><td>3</td></tr>"
                + "<tr><td>1</td><td>1</td><td>2</td><td>3</td></tr>"
                + "<tr><td>2</td><td>4</td><td>5</td><td>6</td></tr>"
                + "</table></body></html>";

            StringAssert.AreEqualIgnoringCase(expectedResult, result);
        }

    }
}
