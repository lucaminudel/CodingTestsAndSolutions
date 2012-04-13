using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class HtmlTableBuilder : ITableBuilder
    {
        private readonly TextWriter _output;
        private int _rowCount;

        public HtmlTableBuilder(TextWriter output)
        {
            _output = output;
            _output.WriteLine("<html><body>");
            _rowCount = 0;
        }

        public void BeginTable(int rows, int columns)
        {
            _output.WriteLine("<table>");

            _output.WriteLine("<tr>");
            _output.WriteLine("<td></td>");
            for(int i = 1; i <= columns; ++i)
            {
                _output.WriteLine("<td>{0}</td>", i);
            }
            _output.WriteLine("</tr>");

        }

        public void AddRow(int[] items)
        {
            _rowCount += 1;

            _output.WriteLine("<tr>");
            _output.WriteLine("<td>{0}</td>", _rowCount);
            foreach (int i in items)
            {
                _output.WriteLine("<td>{0}</td>", i);                
            }
            _output.WriteLine("</tr>");

        }

        public void EndTable()
        {
            _output.WriteLine("</table>");
            _output.WriteLine("</body></html>");
            _output.Flush();
            _output.Close();
        }

    }
}
