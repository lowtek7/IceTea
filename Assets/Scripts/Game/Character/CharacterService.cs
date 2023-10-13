using Service.Character;

namespace Game.Character
{
	public class CharacterService : ICharacterService
	{
		public void Init()
		{
		}

		public T CreateCharacter<T>(int baseId) where T : ICharacter
		{
			throw new System.NotImplementedException();
		}
	}
}
