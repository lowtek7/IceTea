using System.Collections.Generic;

namespace Game.Battle.TurnSystem.Action
{
	/// <summary>
	/// 대상에 대한 정보를 담은 셀렉터
	/// </summary>
	public interface ITurnBattleSelector
	{
		public int SessionId { get; }

		public int InstigatorId { get; }

		public IEnumerable<int> TargetIds { get; }
	}

	public static class TurnBattleSelectorExtensions
	{
		public static bool TryGetSingleTargetId(this ITurnBattleSelector selector, out int resultId)
		{
			resultId = -1;

			foreach (var id in selector.TargetIds)
			{
				resultId = id;
				return true;
			}

			return false;
		}
	}
}
