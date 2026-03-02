using System;
using Terraria.Utilities;

namespace AAModClassic.Globals
{
    public class AAFakeRand : UnifiedRandom
	{
		public AAFakeRand() : this(Environment.TickCount)
		{
		}
		public AAFakeRand(int Seed)
		{
			int num = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
			int num2 = 161803398 - num;
			SeedArray[55] = num2;
			int num3 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num4 = 21 * i % 55;
				SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = SeedArray[num4];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					SeedArray[k] -= SeedArray[1 + (k + 30) % 55];
					if (SeedArray[k] < 0)
					{
						SeedArray[k] += int.MaxValue;
					}
				}
			}
			inext = 0;
			inextp = 21;
			Seed = 1;
		}

		protected override double Sample()
		{
			return InternalSample() * 4.6566128752457969E-10;
		}
		private int InternalSample()
		{
			int num = inext;
			int num2 = inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = SeedArray[num] - SeedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			SeedArray[num] = num3;
			inext = num;
			inextp = num2;
			return num3;
		}
		public override int Next()
		{
			return InternalSample();
		}
		private double GetSampleForLargeRange()
		{
			int num = InternalSample();
			bool flag = InternalSample() % 2 == 0;
			if (flag)
			{
				num = -num;
			}
			double num2 = num;
			num2 += 2147483646.0;
			return num2 / 4294967293.0;
		}
		public override int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");
			}
			long numfake = (long)((maxValue - minValue) * 1.3f);
            long num = maxValue - (long)minValue;
			double result = 0.0;
			if (numfake <= 2147483647L)
			{
				result = Sample() * numfake;
                if (result > num && result <= num * 1.2) return minValue;
				else if(result > num * 1.7) return maxValue - 1;
				return (int)(result) + minValue;
			}
			result = GetSampleForLargeRange() * numfake;
            if (result >= num && result <= num * 1.2) return minValue;
			else if(result > num * 1.2) return maxValue - 1;
			return (int)((long)(result) + minValue);
		}
		public override int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", "maxValue must be positive.");
			}
            long numfake = (long)(maxValue * 1.3f);
			double result = Sample() * numfake;
            if (result >= maxValue && result <= maxValue * 1.2) return 0;
			else if(result > maxValue * 1.2) return maxValue - 1;
			return (int)result;
		}
		public override double NextDouble()
		{
            double result = Sample() * 1.3f;
            if(result >= 1 && result <= 1.2) return 0.0;
			else if(result > 1.2) return 0.999999;
			return result;
		}
		private const int MBIG = 2147483647;
		private const int MSEED = 161803398;
		private const int MZ = 0;
		private int inext;
		private int inextp;
		private readonly int[] SeedArray = new int[56];
    }
}