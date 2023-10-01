using System;
using Core;

namespace Service.Game.Battle
{
	/// <summary>
	/// 배틀의 단위는 세션으로 관리 된다.
	/// </summary>
	public interface IBattleSession : IUpdate, ILateUpdate, IDisposable, IEntity
	{
		bool IsAlive { get; }

		bool IsRunning { get; }

		void Start();

		void Stop();

		IBattleLog GetLog(int logId);
	}
}
