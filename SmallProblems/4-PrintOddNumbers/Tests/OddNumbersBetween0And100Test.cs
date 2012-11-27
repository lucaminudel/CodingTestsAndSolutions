using System;
using NUnit.Framework;

namespace PrintOddNumbers.Tests
{
	[TestFixture]
	public class OddNumbersBetween0And100Test
	{
		private OddNumbersBetween0And100 target;

		[SetUp]
		public void SetUp()
		{
			target = new OddNumbersBetween0And100();
		}

		[Test]
		public void PrintNumbersOn_should_raise_an_exception_when_OutputStream_is_null()
		{
			Assert.Throws<ArgumentNullException>(() => target.PrintNumbersOn(null));
		}

		[Test]
		public void PrintNumbersOn_should_return_50_numbers()
		{
			int count = 0;
			target.PrintNumbersOn((oddNumber) => ++count);

			const int expectedCount = 50;
			Assert.AreEqual(expectedCount, count);
		}

		[Test]
		public void PrintNumbersOn_should_return_numbers_between_0_and_100()
		{
			target.PrintNumbersOn((oddNumber) => Assert.IsTrue(0 <= oddNumber && oddNumber <= 100, string.Format("{0} between 0 and 100", oddNumber)));
		}

		[Test]
		public void PrintNumbersOn_should_return_only_odd_numbers()
		{
			target.PrintNumbersOn((oddNumber) => Assert.IsFalse(oddNumber % 2 == 0, string.Format("{0} Mod 2 equals zero", oddNumber)));
		}

	}
}
