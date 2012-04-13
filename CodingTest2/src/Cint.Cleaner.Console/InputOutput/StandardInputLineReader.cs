namespace Cint.Cleaner.Console.InputOutput
{
    public class StandardInputLineReader : IStandardInputLineReader
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}