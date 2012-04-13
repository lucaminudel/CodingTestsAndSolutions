using System;
using System.Text;

namespace ReverseString
{
	public static class StringExtensionMethods
	{
		public static string Reverse(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException();
			}

			var reverseString = new StringBuilder();

			foreach (var grapheme in new UnicodeStringGraphemes(source))
			{
				reverseString.Insert(0, grapheme);
			}

			reverseString.Replace("\n\r", "\r\n");

			return reverseString.ToString();
		}

	}
}
