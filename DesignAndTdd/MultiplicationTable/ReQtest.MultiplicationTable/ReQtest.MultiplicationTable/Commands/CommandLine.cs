
using System;

namespace ReQtest.MultiplicationTable.Commands
{
    public class CommandLine
    {
        private int _rows;
        private int _columns;
        private OutputFormat _outputFormat;

        public void Process(string[] normalizedArgs)
        {
            ReadSize(normalizedArgs[0], "rows", out _rows);
            ReadSize(normalizedArgs[1], "columns", out _columns);
            ReadOutputFormat(normalizedArgs[2], out _outputFormat);
        }

        public int Rows
        {
            get { return _rows; }
        }

        public int Columns
        {
            get { return _columns; }
        }

        public OutputFormat OutputFormat
        {
            get { return _outputFormat; }
        }

        private static void ReadSize(string argument, string name, out int index)
        {
            bool isInteger = int.TryParse(argument, out index);
            if (isInteger == false)
            {
                throw new ArgumentOutOfRangeException(name, argument, "Expected a valid integer number");
            }

            if (index < 1 || 20 < index)
            {
                throw new ArgumentOutOfRangeException(name, index, "Expected a valid integer number between 1 and 20");
            }
        }

        private static void ReadOutputFormat(string argument, out OutputFormat format)
        {
            try
            {
                format = (OutputFormat)Enum.Parse(typeof(OutputFormat), argument, true);
            }
            catch (ArgumentException)
            {
                format = (OutputFormat)(-100);
            }

            if (Enum.IsDefined(typeof(OutputFormat), format) == false)
            {
                throw new ArgumentOutOfRangeException("Format", argument, "Expected a valid output-format: console, csv or html");
            }
        }

    }
}
