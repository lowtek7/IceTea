using System;
using System.Collections;
using System.Collections.Generic;
using Core.MessageSystem;
using Game.Battle.TurnSystem.Action;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Message;
using Game.Battle.TurnSystem.Session;
using Service;
using Service.Game.Battle;
using UnityEngine;

namespace UnityGame.Battle
{
	public class PlayerBattleController : ITurnBattlePlayableCharacterController, IDisposable
	{
		private bool canInput = false;

		private bool isInput = false;

		private float forwardMoveDuration = 1.2f;

		private float backwardMoveDuration = 0.75f;

		private float moveDistance = 1.5f;

		private readonly List<SubscribeHandler> subscribeHandlers = new List<SubscribeHandler>();

		private TurnBattleSelector selector;

		private IBattleCharacter Owner { get; set; }

		public ITurnBattleSelector Selector => selector;

		private DefaultAttackAction DefaultAttackAction { get; }

		public void Init(IBattleCharacter owner)
		{
			Owner = owner;
			selector = new TurnBattleSelector(owner);
		}

		public IEnumerator TurnProcess(int turnCursor, TurnBattleSession session, TurnBattleCharacter character)
		{
			canInput = true;

			while (!isInput)
			{
				yield return null;
			}

			isInput = false;
			canInput = false;

			yield return DefaultAttackAction.Execute(Owner, Selector, session);
		}

		public PlayerBattleController()
		{
			canInput = false;
			DefaultAttackAction = new DefaultAttackAction();

			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				subscribeHandlers.Add(messageService.Subscribe<TurnBattleActionMessage>(OnEvent));
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
				selector.SetTarget(turnBattleActionMessage.To);
			}

			return false;
		}

		public void Dispose()
		{
			foreach (var subscribeHandler in subscribeHandlers)
			{
				subscribeHandler.Dispose();
			}

			subscribeHandlers.Clear();
		}
	}
}
