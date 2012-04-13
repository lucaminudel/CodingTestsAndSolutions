using Cint.Cleaner.Console.InputOutput;

namespace Cint.Cleaner.Console
{
    static class Program
    {
        static void Main()
        {
            IStandardInputLineReader inputLineReader = new StandardInputLineReader();
            IStandardOutputLineWriter outputLineWriter = new StandardOutputLineWriter();

            Controller controller = new Controller(inputLineReader, outputLineWriter);

            controller.CleanUpTheSpacelySpaceSprocketsOffice();
        }
    }
}
