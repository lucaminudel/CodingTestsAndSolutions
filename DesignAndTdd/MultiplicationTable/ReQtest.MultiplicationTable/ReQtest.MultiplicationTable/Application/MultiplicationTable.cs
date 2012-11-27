
namespace ReQtest.MultiplicationTable.Application
{
    public class MultiplicationTable : ITable
    {
        public int this[int row, int col]
        {
            get
            {
                return row * col;
            }
        }

    }
}
