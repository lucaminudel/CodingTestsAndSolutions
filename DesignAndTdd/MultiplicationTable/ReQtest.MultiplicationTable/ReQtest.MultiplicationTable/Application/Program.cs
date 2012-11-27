using System;
using ReQtest.MultiplicationTable.Commands;
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
                ProcessCommandLine(args);

                ITable table = new MultiplicationTable();

                WriteOutput(table);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine();
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName.ToUpper() + " rows [cols] [output-format]");
                return;
            }
        }

        private void ProcessCommandLine(string[] args)
        {
            DefaultAndOptionalArgs defaultAndOptionalArgs = new DefaultAndOptionalArgs();
            string[] expandedArgs = defaultAndOptionalArgs.ExpandDefaultsAndOptionalArgs(args);
            _commandLine = new CommandLine();
            _commandLine.Process(expandedArgs);
        }

        private void WriteOutput(ITable table)
        {
            string fileName = string.Format("multiply_{0}_{1}", _commandLine.Rows, _commandLine.Columns);
            TableBuilderFactory builderFactory = new TableBuilderFactory();
            ITableBuilder builder = builderFactory.CreateMultiplicationTableBuilderFor(
                _commandLine.OutputFormat,
                fileName);
            TableOutput tableOutput = new TableOutput(builder, table);
            tableOutput.ProduceOutput(_commandLine.Rows, _commandLine.Columns);
        }



        public static void Main(string[] args)
        {
            new Program().Execute(args);
        }

    }
}
