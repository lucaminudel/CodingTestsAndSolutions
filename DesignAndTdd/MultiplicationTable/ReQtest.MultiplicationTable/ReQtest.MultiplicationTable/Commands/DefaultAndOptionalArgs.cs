
using System;

namespace ReQtest.MultiplicationTable.Commands
{
    public class DefaultAndOptionalArgs
    {
        public string[] ExpandDefaultsAndOptionalArgs(string[] args)
        {

            if (args.Length < 1 || 3 < args.Length)
            {
                throw new ArgumentException("Wrong command line arguments, specify the command line arguments");
            }

            if (args.Length == 3)
            {
                return (string[])args.Clone();
            }


            string[] normalizedArg = new string[3];
            normalizedArg[0] = args[0];
            normalizedArg[1] = args[0];
            normalizedArg[2] = OutputFormat.Console.ToString().ToLower();

            if (args.Length == 2)
            {
                if (IsANumber(args[1]))
                {
                    normalizedArg[1] = args[1];
                }
                else
                {
                    normalizedArg[2] = args[1];
                }
            }


            return normalizedArg;
        }

        private static bool IsANumber(string arg)
        {
            int x;
            bool isANumber = int.TryParse(arg, out x);
            return isANumber;
        }

    }
}
