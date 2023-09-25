using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Base.Utility
{
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

		public static float Pow(float f, float p)
		{
			return (float)Math.Pow(f, p);
		}

		public static float Exp(float power)
		{
			return (float)Math.Exp(power);
		}

		public static float Log(float f, float p)
		{
			return (float)Math.Log(f, p);
		}

		public static float Log(float f)
		{
			return (float)Math.Log(f);
		}

		public static float Log10(float f)
		{
			return (float)Math.Log10(f);
		}

		public static float Ceil(float f)
		{
			return (float)Math.Ceiling(f);
		}

		public static float Floor(float f)
		{
			return (float)Math.Floor(f);
		}

		public static float Round(float f)
		{
			return (float)Math.Round(f);
		}

		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling(f);
		}

		public static int FloorToInt(float f)
		{
			return (int)Math.Floor(f);
		}

		public static int RoundToInt(float f)
		{
			return (int)Math.Round(f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f) => (double) f >= 0.0 ? 1f : -1f;
	}
}
