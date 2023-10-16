using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game.Battle.TurnSystem.Message;
using Game.Battle.TurnSystem.Session;
using Service;
using Service.Game.Battle;
using Vim.Math3d;

namespace Game.Battle.TurnSystem.Entity
{
	public enum TurnBattleTeam
	{
		Yellow = 0,
		Purple
	}

	public class TurnBattleCharacter : IBattleCharacter, IDisposable
	{
		private IEntity Entity { get; }

		public int Id { get; }

		public Vector3 Position { get; set; }

		public int SessionId { get; }

		public int Speed { get; private set; }

		IBattleEntityController IBattleCharacter.BattleEntityController => TurnBattleCharacterController;

		public ITurnBattleCharacterController TurnBattleCharacterController { get; set; }

		public TurnBattleCharacter(int sessionId, IEntity entity, ITurnBattleCharacterController entityController)
		{
			SessionId = sessionId;
			Entity = entity;
			Id = entity.Id;
			TurnBattleCharacterController = entityController;
			Position = Vector3.Zero;
		}

		public void Dispose()
		{
		}

		public IEnumerator TurnProcess(int turnCursor, TurnBattleSession session)
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				messageService.Publish(CharacterTurnStartMessage.Create(this));
			}

			yield return TurnBattleCharacterController.TurnProcess(turnCursor, session, this);
		}
	}
}
