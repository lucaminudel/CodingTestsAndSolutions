using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class CsvTableBuilder : ITableBuilder
    {
        private readonly TextWriter _output;
         private int _rowCount;

        public CsvTableBuilder(TextWriter output)
        {
            _output = output;
            _rowCount = 0;
        }

        public void BeginTable(int rows, int columns)
        {
            for (int i = 1; i <= columns; ++i)
            {
                _output.Write(";{0}", i);
            }
            _output.WriteLine();
        }

        public void AddRow(int[] items)
        {
            _rowCount += 1;
            _output.Write(_rowCount);
            foreach (int i in items)
            {
                _output.Write(";{0}", i);
            }
            _output.WriteLine();
        }

        public void EndTable()
        {
            _output.Flush();
            _output.Close();
        }
    }
}
