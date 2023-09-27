using Service;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Action
{
	public class DefaultAttackAction : ITurnBattleAction
	{
		public MotionType MotionType => MotionType.Attack;

		public void Execute(IBattleObject from, IBattleObject to, IBattleSession battleSession, TurnBattleLog battleLog)
		{
			var fromId = from.Id;
			var toId = to.Id;

			if (ServiceManager.TryGetService(out IRandomService randomService))
			{
				if (to is IBattleGroup battleGroup)
				{
					var targets = battleGroup.GroupObjects;
				}
			}
		}
	}
}
