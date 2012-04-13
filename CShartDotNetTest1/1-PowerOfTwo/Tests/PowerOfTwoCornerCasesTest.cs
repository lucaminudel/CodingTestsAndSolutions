using NUnit.Framework;

namespace PowerOfTwo.Tests
{
	[TestFixture]
	public class PowerOfTwoCornerCasesTest
	{
		private PowerOfTwoNumbersSet powerOfTwoNumbersSet;

		[SetUp]
		public void SetUp()
		{
			powerOfTwoNumbersSet = new PowerOfTwoNumbersSet();
		}

		[Test]
		public void A_negative_number_is_not_power_of_two()
		{
			const long aNegativeNumber = -1;
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(aNegativeNumber));
		}

		[Test]
		public void Zero_is_not_power_of_two()
		{
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(0));
		}

		[Test]
		public void One_is_power_of_two()
		{
			Assert.IsTrue(powerOfTwoNumbersSet.Contains(1));
		}

		[Test]
		public void Two_to_the_power_of_62_is_power_of_two()
		{
			const long twoToThePowerOf63 = 4611686018427387904;
			Assert.IsTrue(powerOfTwoNumbersSet.Contains(twoToThePowerOf63));
		}

		[Test]
		public void Long_MaxValue_is_not_power_of_two()
		{
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(long.MaxValue));
		}

		[Test]
		public void Long_MinValue_is_not_power_of_two()
		{
			Assert.IsFalse(powerOfTwoNumbersSet.Contains(long.MinValue));
		}
	}
}
