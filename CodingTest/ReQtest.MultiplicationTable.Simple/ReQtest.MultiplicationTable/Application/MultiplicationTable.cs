
namespace ReQtest.MultiplicationTable.Application
{
    public class MultiplicationTable 
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
