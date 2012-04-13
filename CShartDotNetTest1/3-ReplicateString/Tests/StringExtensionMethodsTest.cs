using System;
using NUnit.Framework;

namespace ReplicateString.Tests
{
	[TestFixture]
	public class StringExtensionMethodsTest
	{
		private string target;

		[Test]
		public void Replicate_should_raise_an_exception_for_a_null_string()
		{
			target = null;

			Assert.Catch<ArgumentNullException>(() => target.Replicate(5));
		}

		[Test]
		public void Replicate_should_raise_an_exception_for_a_negative_count()
		{
			target = "Hi";

			Assert.Catch<ArgumentOutOfRangeException>(() => target.Replicate(-1));
		}

		[Test]
		public void Replicate_should_return_an_emty_string_when_the_source_is_empty()
		{
			target = string.Empty;

			Assert.IsEmpty(target.Replicate(3));
		}

		[Test]
		public void Replicate_should_return_an_empty_strung_when_count_is_zero()
		{
			target = "Hi";

			Assert.IsEmpty(target.Replicate(0));
		}

		[Test]
		public void Replicate_should_return_the_same_string_when_count_is_one()
		{
			target = "Hi";

			Assert.AreEqual(target, target.Replicate(1));			
		}
		
		[Test]
		public void Replicate_should_return_the_string_replicater_count_times()
		{
			target = "Hi";

			Assert.AreEqual("HiHiHi", target.Replicate(3));
		}
	}
}
