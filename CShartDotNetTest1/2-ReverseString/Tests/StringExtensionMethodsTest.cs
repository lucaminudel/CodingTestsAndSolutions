using System;
using NUnit.Framework;

namespace ReverseString.Tests
{
	[TestFixture]
	public class StringExtensionMethodsTest
	{
		private string target;

		[Test]
		public void Null_string_should_raise_an_exception()
		{
			target = null;

			Assert.Catch<ArgumentNullException>(() => target.Reverse());
		}

		[Test]
		public void Empty_string_should_return_an_empty_string()
		{
			target = string.Empty;

			Assert.AreEqual(string.Empty, target.Reverse());
		}

		[Test]
		public void One_character_string_should_return_the_same_string()
		{
			target = "i";

			Assert.AreEqual(target, target.Reverse());
		}

		[Test]
		public void An_ASCII_string_should_be_reversed()
		{
			target = "Hello!";

			Assert.AreEqual("!olleH", target.Reverse());
		}

		[Test]
		public void A_string_with_one_2_bytes_unicode_character_from_the_supplementary_pane_should_not_be_reversed()
		{
			target = "𝔼";

			Assert.AreEqual("𝔼", target.Reverse());
		}

		[Test]
		public void A_string_with_2_bytes_unicode_characters_from_the_supplementary_pane_should_be_reversed()
		{
			target = "H𝔼𝕃𝕃𝕆";

			Assert.AreEqual("𝕆𝕃𝕃𝔼H", target.Reverse());
		}

		[Test]
		public void A_string_with_a_2_byte_unicode_combinig_charter_sequence_should_be_reversed()
		{
			target = "var ga\u030Ar du?";

			Assert.AreEqual("?ud ra\u030Ag rav", target.Reverse());
		}

		[Test]
		public void A_string_with_a_new_line_should_be_reversed()
		{
			target = "1 line.\r\nNext line";

			Assert.AreEqual("enil txeN\r\n.enil 1", target.Reverse());
		}
	}
}
