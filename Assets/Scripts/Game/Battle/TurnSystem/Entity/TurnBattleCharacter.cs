using System;
using System.Collections;
using Core;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Entity
{
	public class TurnBattleCharacter : IBattleCharacter, IDisposable
	{
		private IEntity Entity { get; }

		public int Id { get; }

		public int Speed { get; private set; }

		IBattleEntityController IBattleCharacter.BattleEntityController => TurnBattleCharacterController;

		public ITurnBattleCharacterController TurnBattleCharacterController { get; set; }

		public TurnBattleCharacter(IEntity entity, ITurnBattleCharacterController entityController)
		{
			Entity = entity;
			Id = entity.Id;
			TurnBattleCharacterController = entityController;
		}

		public void Dispose()
		{
		}

		public IEnumerator TurnProcess(int turnCursor, TurnBattleSession session)
		{
			yield return TurnBattleCharacterController.TurnProcess(turnCursor, session, this);
		}
	}
}