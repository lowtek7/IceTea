using System;
using System.Collections.Generic;
using Core.MessageSystem;
using Game.Battle.TurnSystem;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Message;
using Service;
using Service.Game.Battle;
using UnityEngine;
using UnityEngine.UI;
using UnityGame.Battle;

namespace UnityGame
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private Button attackButton;

		[SerializeField]
		private Button skillButton;

		[SerializeField]
		private Button ultButton;

		[SerializeField]
		private TurnBattleCharacterBehaviour player;

		[SerializeField]
		private TurnBattleCharacterBehaviour enemy;

		private IBattleSession currentSession;

		private IBattleCharacter currentCharacter;

		private readonly List<SubscribeHandler> subscribeHandlers = new List<SubscribeHandler>();

		private void Start()
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				subscribeHandlers.Add(messageService.Subscribe<CharacterTurnStartMessage>(OnCharacterTurnStart));
			}

			SetActiveByButtons(false);
		}

		private void OnDestroy()
		{
			foreach (var subscribeHandler in subscribeHandlers)
			{
				subscribeHandler.Dispose();
			}

			subscribeHandlers.Clear();
		}

		private bool OnCharacterTurnStart(IMessage message)
		{
			if (message is CharacterTurnStartMessage characterTurnStartMessage)
			{
				var characterId = characterTurnStartMessage.CharacterId;
				var sessionId = characterTurnStartMessage.SessionId;

				// 플레이어블 캐릭터 체크...
				// 플레이어블 캐릭터 측에서 인풋 가능할때 메세지 날리는것도 생각해볼것
				if (TryGetCharacter(sessionId, characterId, out var character) &&
					character.BattleEntityController is ITurnBattlePlayableCharacterController)
				{
					SetActiveByButtons(true);
					return true;
				}
			}

			return false;
		}

		public void SetSession(IBattleSession battleSession)
		{
			currentSession = battleSession;
		}

		private bool TryGetCharacter(int sessionId, int characterId, out IBattleCharacter battleCharacter)
		{
			battleCharacter = default;

			if (ServiceManager.TryGetService(out IBattleSystemService battleSystemService) &&
				battleSystemService.TryGetSystem(out TurnBattleSystem turnBattleSystem) &&
				turnBattleSystem.TryGetSession(sessionId, out var session) &&
				session.TryGetCharacter(characterId, out var character))
			{
				battleCharacter = character;

				SetActiveByButtons(true);

				return true;
			}

			return false;
		}

		private void SetActiveByButtons(bool value)
		{
			attackButton.gameObject.SetActive(value);
			skillButton.gameObject.SetActive(value);
			ultButton.gameObject.SetActive(value);
		}

		public void AttackButton_OnClicked()
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				messageService.Publish(TurnBattleActionMessage.Create(currentSession.Id,
					player.CharacterId,
					enemy.CharacterId,
					TurnBattleActionType.Basic));
			}
		}

		public void SkillButton_OnClicked()
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				messageService.Publish(TurnBattleActionMessage.Create(currentSession.Id,
					player.CharacterId,
					enemy.CharacterId,
					TurnBattleActionType.Skill));
			}
		}

		public void UltButton_OnClicked()
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				messageService.Publish(TurnBattleActionMessage.Create(currentSession.Id,
					player.CharacterId,
					enemy.CharacterId,
					TurnBattleActionType.Ultimate));
			}
		}
	}
}
