using System.Collections;
using Core.MessageSystem;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Message;
using Game.Battle.TurnSystem.Session;
using Service;
using UnityEngine;

namespace UnityGame.Battle
{
	public class PlayerBattleController : ITurnBattlePlayableCharacterController
	{
		private bool canInput = false;

		private bool isInput = false;

		private TurnBattleCharacterBehaviour self;

		private float forwardMoveDuration = 1.2f;

		private float backwardMoveDuration = 0.75f;

		private float moveDistance = 1.5f;

		public IEnumerator TurnProcess(int turnCursor, TurnBattleSession session, TurnBattleCharacter character)
		{
			canInput = true;

			while (!isInput)
			{
				yield return null;
			}

			isInput = false;
			canInput = false;

			var startPos = self.transform.position;
			var endPos = startPos + new Vector3(moveDistance, 0, 0);
			var timePassed = 0f;

			while (timePassed < forwardMoveDuration)
			{
				timePassed += Time.deltaTime;
				var progress = timePassed / forwardMoveDuration;

				self.transform.position = startPos + new Vector3(moveDistance * progress, 0, 0);
				yield return null;
			}

			timePassed = 0f;

			while (timePassed < backwardMoveDuration)
			{
				timePassed += Time.deltaTime;

				var progress = timePassed / backwardMoveDuration;

				self.transform.position = endPos + new Vector3(-moveDistance * progress, 0, 0);
				yield return null;
			}

			self.transform.position = startPos;
		}

		public PlayerBattleController(TurnBattleCharacterBehaviour behaviour)
		{
			self = behaviour;
			canInput = false;

			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				messageService.Subscribe<TurnBattleActionMessage>(OnEvent);
			}
		}

		private bool OnEvent(IMessage message)
		{
			if (!canInput)
			{
				return false;
			}

			if (message is TurnBattleActionMessage turnBattleActionMessage)
			{
				isInput = true;
				canInput = false;
			}

			return false;
		}
	}
}
