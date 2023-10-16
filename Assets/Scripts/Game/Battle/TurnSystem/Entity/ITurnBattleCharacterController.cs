using System.Collections;
using Game.Battle.TurnSystem.Action;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Entity
{
	public interface ITurnBattleCharacterController : IBattleEntityController
	{
		ITurnBattleSelector Selector { get; }

		void Init(IBattleCharacter owner);

		IEnumerator TurnProcess(int turnCursor, TurnBattleSession session, TurnBattleCharacter character);
	}

	public interface ITurnBattlePlayableCharacterController : ITurnBattleCharacterController
	{

	}
}
