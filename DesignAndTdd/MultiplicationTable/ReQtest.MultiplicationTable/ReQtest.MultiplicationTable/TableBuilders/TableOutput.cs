
namespace ReQtest.MultiplicationTable.TableBuilders
{
    public class TableOutput
    {
        private readonly ITableBuilder _builder;
        private readonly ITable _table;

        public TableOutput(ITableBuilder builder, ITable table)
        {
            _builder = builder;
            _table = table;
        }

        public void ProduceOutput(int rows, int columns)
        {
            int[] rowValues = new int[columns];

            _builder.BeginTable(rows, columns);
            for (int row = 1; row <= rows; ++row)
            {
                for (int col = 1; col <= columns; ++col)
                {
                    rowValues[col - 1] = _table[row, col];
                }
                _builder.AddRow(rowValues);
            }
            _builder.EndTable();
        }
    }
}
