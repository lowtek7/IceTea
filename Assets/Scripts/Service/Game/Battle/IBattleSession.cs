using System;
using Core;

namespace Service.Game.Battle
{
	/// <summary>
	/// 배틀의 단위는 세션으로 관리 된다.
	/// </summary>
	public interface IBattleSession : IUpdate, ILateUpdate, IDisposable
	{
		int Id { get; }

		bool IsRunning { get; }

		/// <summary>
		/// 세션에 엔티티 등록
		/// </summary>
		/// <param name="entityHandle"></param>
		/// <returns></returns>
		IBattleObject RegisterEntity(EntityHandle entityHandle);

		bool UnregisterEntity(EntityHandle entityHandle);

		void Start();

		void Stop();
	}
}
