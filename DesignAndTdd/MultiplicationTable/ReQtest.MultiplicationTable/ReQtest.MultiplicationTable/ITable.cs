namespace ReQtest.MultiplicationTable
{
    public interface ITable
    {
        int this[int row, int col] { get; }
    }
}