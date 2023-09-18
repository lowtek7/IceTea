namespace Service.Game.Battle
{
	public interface IBattleSystemService : IGameService
	{
		T CreateSystem<T>() where T : IBattleSystem, new();
	}
}
