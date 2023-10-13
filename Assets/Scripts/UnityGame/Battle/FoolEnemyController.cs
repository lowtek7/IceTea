using System.Collections;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Session;
using UnityEngine;

namespace UnityGame.Battle
{
	public class FoolEnemyController : ITurnBattleCharacterController
	{
		private TurnBattleCharacterBehaviour self;

		private float forwardMoveDuration = 1.2f;

		private float backwardMoveDuration = 0.75f;

		private float moveDistance = 1.5f;

		public IEnumerator TurnProcess(int turnCursor, TurnBattleSession session, TurnBattleCharacter character)
		{
			var startPos = self.transform.position;
			var endPos = startPos + new Vector3(-moveDistance, 0, 0);
			var timePassed = 0f;

			while (timePassed < forwardMoveDuration)
			{
				timePassed += Time.deltaTime;
				var progress = timePassed / forwardMoveDuration;

				self.transform.position = startPos + new Vector3(-moveDistance * progress, 0, 0);
				yield return null;
			}

			timePassed = 0f;

			while (timePassed < backwardMoveDuration)
			{
				timePassed += Time.deltaTime;

				var progress = timePassed / backwardMoveDuration;

				self.transform.position = endPos + new Vector3(moveDistance * progress, 0, 0);
				yield return null;
			}

			self.transform.position = startPos;
		}

		public FoolEnemyController(TurnBattleCharacterBehaviour behaviour)
		{
			self = behaviour;
		}
	}
}
