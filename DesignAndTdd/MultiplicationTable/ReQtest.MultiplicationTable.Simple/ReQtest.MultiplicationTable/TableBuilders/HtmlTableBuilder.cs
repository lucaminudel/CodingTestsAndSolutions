using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class HtmlTableBuilder
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly TextWriter _output;

        public HtmlTableBuilder(int rows, int columns)
            : this(rows, columns, new StreamWriter(string.Format("multiply_{0}_{1}", rows, columns) + ".html"))
        {
        }

        public HtmlTableBuilder(int rows, int columns, TextWriter output)
        {
            _rows = rows;
            _columns = columns;
            _output = output;
            _output.WriteLine("<html><body>");
        }

        public void ProduceOutput(Application.MultiplicationTable multiplicationTable)
        {
            _output.WriteLine("<table>");

            _output.WriteLine("<tr>");
            _output.WriteLine("<td></td>");
            for (int i = 1; i <= _columns; ++i)
            {
                _output.WriteLine("<td>{0}</td>", i);
            }
            _output.WriteLine("</tr>");

            for (int row = 1; row <= _rows; ++row)
            {
                _output.WriteLine("<tr>");
                _output.WriteLine("<td>{0}</td>", row);
                for (int col = 1; col <= _columns; ++col)
                {
                    _output.WriteLine("<td>{0}</td>", multiplicationTable[row, col]);
                }
                _output.WriteLine("</tr>");
            }

            _output.WriteLine("</table>");
            _output.WriteLine("</body></html>");
            _output.Flush();
            _output.Close();        
        }

    }
}
