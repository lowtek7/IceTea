using Core.MessageSystem;
using Game.Battle.TurnSystem.Action;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Message
{
	public class BattleActionExecutedMessage : PooledMessage<BattleActionExecutedMessage>
	{
		/// <summary>
		/// 배틀 액션을 실행한 사람
		/// </summary>
		public IBattleObject From { get; private set; }

		/// <summary>
		/// 대상이 그룹일수도 있음
		/// </summary>
		public IBattleObject To { get; private set; }

		public ITurnBattleAction BattleAction { get; private set; }

		public int LogId { get; private set; }

		public static BattleActionExecutedMessage Create(IBattleObject from, IBattleObject to, ITurnBattleAction battleAction, int logId)
		{
			var result = GetOrCreate();

			result.From = from;
			result.To = to;
			result.BattleAction = battleAction;
			result.LogId = logId;

			return result;
		}
	}
}
