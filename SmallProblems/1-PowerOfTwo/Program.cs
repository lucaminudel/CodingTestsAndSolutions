
using System;

namespace PowerOfTwo
{
	class Program
	{
		private static void Main()
		{
			var powerOfTwoNumbersSet = new PowerOfTwoNumbersSet();

			const long minusTwoToThePowerOf63 = -9223372036854775808;
			Console.WriteLine(powerOfTwoNumbersSet.Contains(minusTwoToThePowerOf63));

			const long twoToThePowerOf32 = 4294967296;
			Console.WriteLine(powerOfTwoNumbersSet.Contains(twoToThePowerOf32));
		}
	}
}
