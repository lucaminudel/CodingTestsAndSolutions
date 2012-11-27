namespace ReQtest.MultiplicationTable.TableBuilders
{
    public interface ITableBuilder 
    {
        void BeginTable(int rows, int columns);
        void AddRow(int[] items);
        void EndTable();
    }
}