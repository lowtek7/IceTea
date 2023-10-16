using System.Collections.Generic;
using Game.Battle.TurnSystem.Entity;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Action
{
	public class TurnBattleSelector : ITurnBattleSelector
	{
		private readonly IBattleCharacter self;

		private readonly List<int> targetList = new List<int>();

		public int SessionId => self.SessionId;

		public int InstigatorId => self.Id;

		public IEnumerable<int> TargetIds => targetList;

		public void SetTarget(int id)
		{
			targetList.Clear();
			targetList.Add(id);
		}

		public void SetTarget(params int[] idArray)
		{
			targetList.Clear();
			targetList.AddRange(idArray);
		}

		public TurnBattleSelector(IBattleCharacter self)
		{
			this.self = self;
		}
	}
}
