using System;
using Core;
using Service.Game;

namespace Game
{
	/// <summary>
	/// 게임 월드 서비스 구현체
	/// </summary>
	public class GameWorldService : IGameWorldService, IDisposable
	{
		private IGameWorld GameWorld { get; set; }

		private bool isInit = false;

		public void Init(IGameWorld gameWorld)
		{
			if (isInit)
			{
				return;
			}

			isInit = true;
			GameWorld = gameWorld;
		}

		public bool TryGetWorld(out IGameWorld gameWorld)
		{
			gameWorld = GameWorld;

			return GameWorld != null;
		}

		public void Dispose()
		{
			GameWorld = null;
			isInit = false;
		}
	}
}
