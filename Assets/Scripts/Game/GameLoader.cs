using Core;
using Game.Battle;
using Service;
using Service.Game;
using Service.Game.Battle;

namespace Game
{
	public class GameLoader
	{
		private GameWorldService gameWorldService;

		public void Load()
		{
			gameWorldService = new GameWorldService();

			var randomService = new RandomService();

			// 게임 월드 서비스의 초기화 순서가 제일 중요하므로 특별한 취급으로 강제로 초기화 시킨다.
			gameWorldService.Init();
			randomService.Init();

			ServiceManager.SetService(typeof(IGameWorldService), gameWorldService);
			ServiceManager.SetService(typeof(IRandomService), randomService);
			ServiceManager.SetService(typeof(IBattleSystemService), new BattleSystemService());
		}
	}
}
