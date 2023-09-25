using Core;

namespace Service.Character
{
	public interface ICharacter : IEntity
	{
		ICharacterSpec Spec { get; }
	}
}
