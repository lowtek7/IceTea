using System;
using Service.Game.Battle;
using UnityEngine;

namespace UnityGame.Battle
{
	public class TurnBattleCharacterBehaviour : MonoBehaviour
	{
		public IBattleCharacter BattleCharacter { get; private set; }

		public int CharacterId => BattleCharacter.Id;

		public void Init(IBattleCharacter battleCharacter)
		{
			var currentPos = transform.position;

			BattleCharacter = battleCharacter;
			BattleCharacter.Position = new Vim.Math3d.Vector3(currentPos.x, currentPos.y, currentPos.z);
		}

		private void Update()
		{
			if (BattleCharacter != null)
			{
				transform.position = new Vector3(BattleCharacter.Position.X, BattleCharacter.Position.Y, BattleCharacter.Position.Z);
			}
		}
	}
}
