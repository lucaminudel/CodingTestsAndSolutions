using System;

namespace ReQtest.MultiplicationTable.Application
{
    public class CommandLine
    {
        private int _rows;
        private int _columns;
        private OutputFormat _outputFormat;

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

        public void Process(string[] args)
        {
            string[] expandedArgs = ExpandDefaultsAndOptionalArgs(args);
            ReadSize(expandedArgs[0], "rows", out _rows);
            ReadSize(expandedArgs[1], "columns", out _columns);
            ReadOutputFormat(expandedArgs[2], out _outputFormat);
        }

        private static string[] ExpandDefaultsAndOptionalArgs(string[] args)
        {

            if (args.Length < 1 || 3 < args.Length)
            {
                throw new ArgumentException("Wrong command line arguments, specify the command line arguments");
            }

            if (args.Length == 3)
            {
                return (string[])args.Clone();
            }


            string[] normalizedArg = new string[3];
            normalizedArg[0] = args[0];
            normalizedArg[1] = args[0];
            normalizedArg[2] = OutputFormat.Console.ToString().ToLower();

            if (args.Length == 2)
            {
                if (IsANumber(args[1]))
                {
                    normalizedArg[1] = args[1];
                }
                else
                {
                    normalizedArg[2] = args[1];
                }
            }


            return normalizedArg;
        }

        private static bool IsANumber(string arg)
        {
            int x;
            bool isANumber = int.TryParse(arg, out x);
            return isANumber;
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
