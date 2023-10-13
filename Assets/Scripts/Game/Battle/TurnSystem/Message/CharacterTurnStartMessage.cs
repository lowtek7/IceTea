using Core.MessageSystem;
using Game.Battle.TurnSystem.Entity;

namespace Game.Battle.TurnSystem.Message
{
	public class CharacterTurnStartMessage : PooledMessage<CharacterTurnStartMessage>
	{
		public int CharacterId { get; private set; }

		public int SessionId { get; private set; }

		public static CharacterTurnStartMessage Create(TurnBattleCharacter turnBattleCharacter)
		{
			var result = GetOrCreate();

			result.CharacterId = turnBattleCharacter.Id;
			result.SessionId = turnBattleCharacter.SessionId;

			return result;
		}
	}
}
