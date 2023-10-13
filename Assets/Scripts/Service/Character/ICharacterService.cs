using Core;

namespace Service.Character
{
	public interface ICharacterService : IGameService
	{
		T CreateCharacter<T>(int baseId) where T : ICharacter;
	}
}
