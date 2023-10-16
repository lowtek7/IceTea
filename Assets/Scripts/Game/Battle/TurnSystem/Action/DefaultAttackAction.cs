using System.Collections;
using Game.Battle.TurnSystem.Entity;
using Service;
using Service.Game.Battle;
using Vim.Math3d;

namespace Game.Battle.TurnSystem.Action
{
	public class DefaultAttackAction : ITurnBattleAction
	{
		public TargetingMode TargetingMode => TargetingMode.SingleEnemy;

		public int TargetingCount => 1;

		public MotionType MotionType => MotionType.Attack;

		private readonly float forwardMoveDuration = 1.2f;

		private readonly float backwardMoveDuration = 0.75f;

		public IEnumerator Execute(IBattleCharacter self, ITurnBattleSelector battleSelector, IBattleSession battleSession)
		{
			if (battleSelector.TryGetSingleTargetId(out var targetId) &&
				battleSession.TryGetCharacter(targetId, out var target))
			{
				var startPos = self.Position;
				var targetPos = target.Position;
				var dir = (targetPos - startPos).Normalize();
				var distance = (float) (targetPos - startPos).Magnitude() * 0.85f;
				var endPos = startPos + dir * distance;
				var timePassed = 0f;

				while (timePassed < forwardMoveDuration)
				{
					timePassed += TimeServiceHelper.DeltaTime;
					var progress = timePassed / forwardMoveDuration;

					self.Position = startPos + dir * (distance * progress);
					yield return null;
				}

				timePassed = 0f;

				while (timePassed < backwardMoveDuration)
				{
					timePassed += TimeServiceHelper.DeltaTime;
					var progress = timePassed / backwardMoveDuration;

					self.Position = endPos + dir * (-distance * progress);
					yield return null;
				}

				self.Position = startPos;
			}
		}
	}
}
