using Core.MessageSystem;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Message
{
	public class TurnBattleActionMessage : PooledMessage<TurnBattleActionMessage>
	{
		public int SessionId { get; private set; }

		/// <summary>
		/// 배틀 액션을 실행한 캐릭터의 Id.
		/// </summary>
		public int From { get; private set; }

		/// <summary>
		/// 대상이 그룹일수도 있음. 대상의 Id.
		/// </summary>
		public int To { get; private set; }

		public TurnBattleActionType ActionType { get; private set; }

		public static TurnBattleActionMessage Create(int sessionId, int from, int to, TurnBattleActionType actionType)
		{
			var result = GetOrCreate();

			result.SessionId = sessionId;
			result.From = from;
			result.To = to;
			result.ActionType = actionType;

			return result;
		}
	}
}
