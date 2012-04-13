namespace PrintOddNumbers
{
	class OddNumbersBetween0And100
	{
		public delegate void OutputStream(int oddNumber);

		public void PrintNumbersOn(OutputStream printOddNumber)
		{
			for (int z = 0; z <= 49; ++z)
			{
				printOddNumber(z * 2 + 1);
			}
		}
	}
}