using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class ConsoleTableBuilder : ITableBuilder
    {
        private readonly TextWriter _output;
        private int _rowCount;

        public ConsoleTableBuilder(TextWriter output)
        {
            _output = output;
            _rowCount = 0;
        }

        public void BeginTable(int rows, int columns)
        {
            _output.Write("    ");
            for (int i = 1; i <= columns; ++i)
            {
                _output.Write("{0,4}", i);
            }
            _output.WriteLine();
        }

        public void AddRow(int[] items)
        {
            _rowCount += 1;
            _output.Write("{0,4}", _rowCount);
            foreach (int i in items)
            {
                _output.Write("{0,4}", i);
            }
            _output.WriteLine();
        }

        public void EndTable()
        {
            _output.Flush();
        }
    }
}
