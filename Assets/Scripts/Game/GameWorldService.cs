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
		private bool isInit = false;

		public void Init()
		{
			if (isInit)
			{
				return;
			}

			isInit = true;
		}

		public void Dispose()
		{
			isInit = false;
		}
	}
}
