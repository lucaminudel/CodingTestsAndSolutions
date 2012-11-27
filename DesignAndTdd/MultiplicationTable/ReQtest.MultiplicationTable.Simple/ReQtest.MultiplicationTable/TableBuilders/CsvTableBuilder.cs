using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class CsvTableBuilder 
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly TextWriter _output;

        public CsvTableBuilder(int rows, int columns)
            : this(rows, columns, new StreamWriter(string.Format("multiply_{0}_{1}", rows, columns) + ".csv"))
        {
        }

        public CsvTableBuilder(int rows, int columns, TextWriter output)
        {
            _rows = rows;
            _columns = columns;
            _output = output;
        }

        public void ProduceOutput(Application.MultiplicationTable multiplicationTable)
        {
            for (int i = 1; i <= _columns; ++i)
            {
                _output.Write(";{0}", i);
            }
            _output.WriteLine();

            for (int row = 1; row <= _rows; ++row)
            {
                _output.Write(row);
                for (int col = 1; col <= _columns; ++col)
                {
                    _output.Write(";{0}", multiplicationTable[row, col]);
                }
                _output.WriteLine();
            }

            _output.Flush();
            _output.Close();
        }

    }
}
