using Core;
using Game.Battle;
using Service;
using Service.Game;
using Service.Game.Battle;

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

			// 게임 월드 서비스의 초기화 순서가 제일 중요하므로 특별한 취급으로 강제로 초기화 시킨다.
			gameWorldService.Init(gameWorld);

			ServiceManager.SetService(typeof(IGameWorldService), gameWorldService);
			ServiceManager.SetService(typeof(IBattleSystemService), new BattleSystemService());
		}
	}
}
