using System;
using System.Collections.Generic;
using Core;

namespace Service.Game.Battle
{
	/// <summary>
	/// 배틀의 단위는 세션으로 관리 된다.
	/// </summary>
	public interface IBattleSession : IUpdate, ILateUpdate, IDisposable
	{
		/// <summary>
		/// 이 세션의 ID
		/// </summary>
		int Id { get; }

		bool IsAlive { get; }

		bool IsRunning { get; }

		IEnumerable<int> CharacterIds { get; }

		void Start();

		void Stop();

		IBattleLog GetLog(int logId);

		bool TryGetCharacter(int id, out IBattleCharacter character);

		IBattleObject RegisterEntity(IEntity entity, IBattleEntityController entityController);

		void UnregisterEntity(IEntity entity);
	}
}
