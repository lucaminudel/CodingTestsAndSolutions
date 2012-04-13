namespace PowerOfTwo
{
	public class PowerOfTwoNumbersSet
	{
		public bool Contains(long number)
		{
			// Fast algorithm check based on the binary representation of integers:
			// positive x is a power of two <=> (x & (x ? 1)) equals zero.

			if (number <= 0)
			{
				return false;
			}

			return (number & (number - 1)) == 0;
		}
	}
}