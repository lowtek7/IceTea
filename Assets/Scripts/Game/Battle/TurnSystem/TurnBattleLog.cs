using Game.Battle.TurnSystem.Action;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem
{
	public class TurnBattleLog : IBattleLog
	{
		public int LogId { get; }

		public int Seed { get; }

		public ulong Step { get; }

		public IBattleObject From { get; }

		public IBattleObject To { get; }

		public ITurnBattleAction BattleAction { get; }

		public TurnBattleLog(int logId, int seed, ulong step, IBattleObject from, IBattleObject to, ITurnBattleAction battleAction)
		{
			LogId = logId;
			Seed = seed;
			Step = step;
			From = from;
			To = to;
			BattleAction = battleAction;
		}
	}
}
