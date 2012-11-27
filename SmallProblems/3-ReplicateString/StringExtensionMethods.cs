
using System;
using System.Text;

namespace ReplicateString
{
	public static class StringExtensionMethods
	{
		public static string Replicate(this string source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count must be zero or more then zero.");
			}

			var replica = new StringBuilder();

			for (; count > 0; --count)
			{
				replica.Append(source);
			}
				
			return replica.ToString();
		}
	}
}
