using Game.Battle.TurnSystem.Action;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Ai
{
	public interface ITurnBattleAi
	{
		ITurnBattleAction Process(IBattleObject entity);
	}
}
