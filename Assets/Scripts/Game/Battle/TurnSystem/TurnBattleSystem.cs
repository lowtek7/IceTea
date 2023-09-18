using System;
using System.Collections.Generic;
using Core;
using Game.Battle.TurnSystem.Component;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem
{
	public class TurnBattleSystem : IBattleSystem
	{
		private IGameWorld GameWorld { get; set; }

		private EntityHandle SelfHandle { get; set; }

		private SortedList<int, IBattleSession> Sessions { get; set; }

		/// <summary>
		/// 여기서 턴 배틀 시스템의 셋팅 정보를 에셋기반으로 읽어와서 셋팅 해야함.
		/// </summary>
		/// <param name="world"></param>
		/// <param name="systemEntityHandle"></param>
		public void Init(IGameWorld world, EntityHandle systemEntityHandle)
		{
			GameWorld = world;
			SelfHandle = systemEntityHandle;
			Sessions = new SortedList<int, IBattleSession>();

			systemEntityHandle.Add<SystemComponent>();
			systemEntityHandle.Add<SessionsComponent>();

			ref var sessions = ref SelfHandle.Get<SessionsComponent>();
			sessions.SessionIdList = new List<int>();
		}

		public void Dispose()
		{
			GameWorld.DestroyEntity(SelfHandle);

			SelfHandle = EntityHandle.Empty;
			GameWorld = null;
		}

		public IBattleSession CreateSession()
		{
			var sessionHandle = GameWorld.CreateEntity();
			ref var sessions = ref SelfHandle.Get<SessionsComponent>();
			var session = new TurnBattleSession(GameWorld, sessionHandle);

			sessions.SessionIdList.Add(sessionHandle.Id);
			Sessions.Add(sessionHandle.Id, session);

			return session;
		}

		public void ReleaseSession(IBattleSession battleSession)
		{
			ref var sessions = ref SelfHandle.Get<SessionsComponent>();

			Sessions.Remove(battleSession.Id);
			sessions.SessionIdList.Remove(battleSession.Id);

			battleSession.Dispose();
		}

		public void UpdateProcess(float deltaTime)
		{
			Span<int> sessionList = stackalloc int[Sessions.Count];

			for (int i = 0; i < Sessions.Count; i++)
			{
				sessionList[i] = Sessions.Keys[i];
			}

			foreach (var id in sessionList)
			{
				if (Sessions.TryGetValue(id, out var battleSession) &&
					battleSession.IsAlive)
				{
					battleSession.UpdateProcess(deltaTime);
				}
			}
		}

		public void LateUpdateProcess(float deltaTime)
		{
			Span<int> sessionList = stackalloc int[Sessions.Count];

			for (int i = 0; i < Sessions.Count; i++)
			{
				sessionList[i] = Sessions.Keys[i];
			}

			foreach (var id in sessionList)
			{
				if (Sessions.TryGetValue(id, out var battleSession) &&
					battleSession.IsAlive)
				{
					battleSession.LateUpdateProcess(deltaTime);
				}
			}
		}
	}
}
