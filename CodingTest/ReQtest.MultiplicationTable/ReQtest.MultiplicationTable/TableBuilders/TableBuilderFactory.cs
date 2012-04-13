using System;
using System.IO;

namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class TableBuilderFactory
    {
        public ITableBuilder CreateMultiplicationTableBuilderFor(OutputFormat outputFormat, string fileName)
        {
            ITableBuilder builder = null;
            switch (outputFormat)
            {
                case OutputFormat.Console:
                    builder = new ConsoleTableBuilder(Console.Out);
                    break;
                case OutputFormat.Csv:
                    builder = new CsvTableBuilder(new StreamWriter(fileName + ".csv"));
                    break;
                case OutputFormat.Html:
                    builder = new HtmlTableBuilder(new StreamWriter(fileName + ".html"));
                    break;
            }

            return builder;
        }
    }
}
