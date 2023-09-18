using System;
using Core;

namespace Service.Game.Battle
{
	public interface IBattleSystem : IUpdate, ILateUpdate, IDisposable
	{
		/// <summary>
		/// 배틀 시스템 초기화
		/// </summary>
		/// <param name="world"></param>
		/// <param name="systemEntityHandle">해당 배틀 시스템의 엔티티 핸들</param>
		void Init(IGameWorld world, EntityHandle systemEntityHandle);

		/// <summary>
		/// 배틀은 기본적으로 세션 별로 나뉘어져 있다.
		/// 따라서 새로운 배틀을 발생 시킬 때는 새로운 세션을 생성해야한다.
		/// </summary>
		IBattleSession CreateSession();

		void ReleaseSession(IBattleSession battleSession);
	}
}
