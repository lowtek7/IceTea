namespace Service
{
	public interface IRandomGenerator
	{
	}

	public interface IRandomService : IGameService
	{
		int Seed { get; }

		ulong Step { get; }

		int Rand();

		int Rand(int max);

		int Rand(int min, int max);
	}
}
