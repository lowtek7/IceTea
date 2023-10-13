using Service.Game.Battle;
using UnityEngine;

namespace UnityGame.Battle
{
	public class TurnBattleCharacterBehaviour : MonoBehaviour
	{
		private IBattleCharacter battleCharacter;

		public int CharacterId => battleCharacter.Id;

		public void SetCharacter(IBattleCharacter character)
		{
			battleCharacter = character;
		}
	}
}
