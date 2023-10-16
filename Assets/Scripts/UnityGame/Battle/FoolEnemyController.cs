using System.Collections;
using Game.Battle.TurnSystem.Action;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;
using UnityEngine;

namespace UnityGame.Battle
{
	public class FoolEnemyController : ITurnBattleCharacterController
	{
		private float forwardMoveDuration = 1.2f;

		private float backwardMoveDuration = 0.75f;

		private float moveDistance = 1.5f;

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
			foreach (var targetId in session.CharacterIds)
			{
				if (targetId != Owner.Id)
				{
					selector.SetTarget(targetId);
					break;
				}
			}

			yield return DefaultAttackAction.Execute(Owner, Selector, session);
		}

		public FoolEnemyController()
		{
			DefaultAttackAction = new DefaultAttackAction();
		}
	}
}
