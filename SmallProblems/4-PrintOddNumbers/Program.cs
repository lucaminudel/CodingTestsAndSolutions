using System;

namespace PrintOddNumbers
{
	static class Program
	{
		private static void Main()
		{
			var oddNumbersSet = new OddNumbersBetween0And100();

			oddNumbersSet.PrintNumbersOn(Console.WriteLine);
		}
	}
}
