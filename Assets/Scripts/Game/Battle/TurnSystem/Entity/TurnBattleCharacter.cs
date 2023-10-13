using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game.Battle.TurnSystem.Message;
using Game.Battle.TurnSystem.Session;
using Service;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Entity
{
	public class TurnBattleCharacter : IBattleCharacter, IDisposable
	{
		private IEntity Entity { get; }

		private readonly List<IEntity> children = new List<IEntity>();

		public int Id { get; }

		public IEnumerable<IEntity> Children => children;

		public void AddChild(IEntity entity)
		{
			children.Add(entity);

			if (entity is IAttachment attachment)
			{
				attachment.AttachTo(this);
			}
		}

		public void RemoveChild(IEntity entity)
		{
			if (entity is IAttachment attachment)
			{
				attachment.DetachFrom(this);
			}

			children.Remove(entity);
		}

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
