using System;
using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class ConsoleTableBuilder 
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly TextWriter _output;

        public ConsoleTableBuilder(int rows, int columns)
            : this(rows, columns, Console.Out)
        {
        }

        public ConsoleTableBuilder(int rows, int columns, TextWriter output)
        {
            _rows = rows;
            _columns = columns;
            _output = output;
        }

        public void ProduceOutput(Application.MultiplicationTable multiplicationTable)
        {
            _output.Write("    ");
            for (int i = 1; i <= _columns; ++i)
            {
                _output.Write("{0,4}", i);
            }
            _output.WriteLine();

            for (int row = 1; row <= _rows; ++row)
            {
                _output.Write("{0,4}", row);
                for (int col = 1; col <= _columns; ++col)
                {
                    _output.Write("{0,4}", multiplicationTable[row, col]);
                }
                _output.WriteLine();
            }

            _output.Flush();
        }

    }
}
