namespace Base.Utility
{
	/// <summary>
	/// MIT LICENSE
	/// source url : https://github.com/Inspiaaa/BlitzEcs
	/// </summary>
	public static class MathUtil
	{
		public static int NextPowerOf2(int num)
		{
			if (num == 0)
				return 0;

			int pow = 1;
			while (pow < num)
			{
				pow = pow << 1;
			}

			return pow;
		}

		public static int CeilDivision(int numerator, int denominator)
		{
			return (numerator + denominator - 1) / denominator;
		}
	}
}
