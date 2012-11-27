using System.Diagnostics;

namespace ReverseString
{
	static class Program
	{
		private static void Main()
		{
			Debug.WriteLine("Hello world!");
			Debug.WriteLine("Hello world!".Reverse());
			Debug.WriteLine("H𝔼𝕃𝕃𝕆 world");
			Debug.WriteLine("H𝔼𝕃𝕃𝕆 world".Reverse());
			Debug.WriteLine("var ga\u030Ar du?");
			Debug.WriteLine("var ga\u030Ar du?".Reverse());
			Debug.WriteLine("First line\r\n2nd line.");
			Debug.WriteLine("First line\r\n2nd line.".Reverse());		
		}
	}
}
