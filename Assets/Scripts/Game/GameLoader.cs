using Core;
using Service;
using Service.Game;

namespace Game
{
	public class GameLoader
	{
		private IGameWorld gameWorld;

		private GameWorldService gameWorldService;

		public void Load()
		{
			gameWorld = new GameWorld();
			gameWorldService = new GameWorldService();

			gameWorldService.Init(gameWorld);

			ServiceManager.SetService(typeof(IGameWorldService), gameWorldService);
		}
	}
}
