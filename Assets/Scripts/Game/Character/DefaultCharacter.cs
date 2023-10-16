using Service.Character;

namespace Game.Character
{
	public class DefaultCharacter : ICharacter
	{
		public int Id { get; }

		public ICharacterSpec Spec { get; }

		public DefaultCharacter(int id)
		{
			Id = id;
			Spec = new DefaultCharacterSpec(30, 10, 3, 0, 1);
		}

		public void Dispose()
		{
		}
	}
}
