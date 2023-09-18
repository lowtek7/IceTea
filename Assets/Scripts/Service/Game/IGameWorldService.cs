using Core;

namespace Service.Game
{
	public interface IGameWorldService : IGameService
	{
		bool TryGetWorld(out IGameWorld gameWorld);
	}
}
