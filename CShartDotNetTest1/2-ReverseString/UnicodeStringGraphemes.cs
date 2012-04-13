using System.Collections;
using System.Globalization;

namespace ReverseString
{
	class UnicodeStringGraphemes : IEnumerable
	{
		private readonly string source;

		public UnicodeStringGraphemes(string source)
		{
			this.source = source;
		}

		public IEnumerator GetEnumerator()
		{
			return StringInfo.GetTextElementEnumerator(source);
		}
	}
}