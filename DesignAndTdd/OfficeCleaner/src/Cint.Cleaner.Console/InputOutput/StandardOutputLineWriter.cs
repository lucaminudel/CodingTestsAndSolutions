namespace Cint.Cleaner.Console.InputOutput
{
    public class StandardOutputLineWriter : IStandardOutputLineWriter
    {
        public void WriteLine(string output)
        {
            System.Console.WriteLine(output);
        }
    }
}
