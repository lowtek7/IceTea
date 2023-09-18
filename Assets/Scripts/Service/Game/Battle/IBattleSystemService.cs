namespace Service.Game.Battle
{
	public interface IBattleSystemService
	{
		T CreateSystem<T>() where T : IBattleSystem;
	}
}
