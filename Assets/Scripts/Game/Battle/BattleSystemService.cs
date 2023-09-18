using Core;
using Service.Game.Battle;

namespace Game.Battle
{
	public class BattleSystemService : IBattleSystemService
	{
		public void Init(IGameWorld gameWorld)
		{
		}

		public T CreateSystem<T>() where T : IBattleSystem, new()
		{
			throw new System.NotImplementedException();
		}
	}
}
