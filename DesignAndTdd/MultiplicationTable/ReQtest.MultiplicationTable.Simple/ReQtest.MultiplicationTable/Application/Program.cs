
using System;
using ReQtest.MultiplicationTable.TableBuilders;

namespace ReQtest.MultiplicationTable.Application
{
    public class Program
    {
        private CommandLine _commandLine;

        public void Execute(string[] args)
        {
            try
            {
                ProcessCommandline(args);

                MultiplicationTable table = new MultiplicationTable();

                WriteOutput(table);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine();
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName.ToUpper() + " rows [cols] [output-format]");
            }
        }

        private void ProcessCommandline(string[] args)
        {
            _commandLine = new CommandLine();
            _commandLine.Process(args);
        }

        private void WriteOutput(MultiplicationTable table)
        {
            switch (_commandLine.OutputFormat)
            {
                case OutputFormat.Console:
                    new ConsoleTableBuilder(_commandLine.Rows, _commandLine.Columns).ProduceOutput(table);
                    break;
                case OutputFormat.Csv:
                    new CsvTableBuilder(_commandLine.Rows, _commandLine.Columns).ProduceOutput(table);
                    break;
                case OutputFormat.Html:
                    new HtmlTableBuilder(_commandLine.Rows, _commandLine.Columns).ProduceOutput(table);
                    break;
            }
        }



        static void Main(string[] args)
        {
            new Program().Execute(args);
        }
    }
}
