using System;
using Core;
using Service;

namespace Game
{
	public class RandomService : IRandomService
	{
		private Random baseRandom = new Random();

		private int innerSeed = -1;

		private ulong baseStep = 0;

		public int Seed => innerSeed;

		public ulong Step => baseStep;

		public void Init()
		{
			innerSeed = new Random().Next();
			baseRandom = new Random(innerSeed);
		}

		public void SetSeed(int seed, ulong step)
		{
			innerSeed = seed;
			baseRandom = new Random(innerSeed);

			for (ulong i = 0; i < step; i++)
			{
				baseRandom.Next();
			}
		}

		public int Rand()
		{
			var result = baseRandom.Next();
			++baseStep;

			return result;
		}

		public int Rand(int max)
		{
			var result = baseRandom.Next(max);
			++baseStep;

			return result;
		}

		public int Rand(int min, int max)
		{
			var result = baseRandom.Next(min, max);
			++baseStep;

			return result;
		}
	}
}
