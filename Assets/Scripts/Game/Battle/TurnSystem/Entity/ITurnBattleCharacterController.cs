using System.Collections;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Entity
{
	public interface ITurnBattleCharacterController : IBattleEntityController
	{
		IEnumerator TurnProcess(int turnCursor, TurnBattleSession session, TurnBattleCharacter character);
	}
}