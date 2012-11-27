using NUnit.Framework;

namespace PowerOfTwo.Tests
{
	[TestFixture]
	public class PowerOfTwoTest
	{
		private PowerOfTwoNumbersSet powerOfTwoNumbersSet;

		[SetUp]
		public void SetUp()
		{
			powerOfTwoNumbersSet = new PowerOfTwoNumbersSet();
		}

		[Test]
		public void Two_is_power_of_two()
		{
			Assert.IsTrue(powerOfTwoNumbersSet.Contains(2));
		}

		[Test]
		public void Tree_is_not_power_of_two()
		{
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(3));
		}

		[Test]
		public void Two_to_the_power_of_16_is_power_of_two()
		{
			const long twoToThePowerOf16 = 65536;
			Assert.IsTrue(powerOfTwoNumbersSet.Contains(twoToThePowerOf16));
		}

		[Test]
		public void Not_all_even_numbers_are_power_of_two()
		{
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(65530));			
		}
	}
}
