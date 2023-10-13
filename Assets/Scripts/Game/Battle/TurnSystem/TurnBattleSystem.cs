using System;
using System.Collections.Generic;
using Game.Battle.TurnSystem.Session;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem
{
	public class TurnBattleSystem : IBattleSystem
	{
		public static int MaxSessionId => 100;

		private Queue<int> SessionIdQueue { get; set; }

		private SortedList<int, IBattleSession> Sessions { get; set; }

		public IEnumerable<int> SessionIds => Sessions.Keys;

		/// <summary>
		/// 여기서 턴 배틀 시스템의 셋팅 정보를 에셋기반으로 읽어와서 셋팅 해야함.
		/// </summary>
		public void Init()
		{
			SessionIdQueue = new Queue<int>();
			Sessions = new SortedList<int, IBattleSession>();

			for (int i = 0; i < MaxSessionId; i++)
			{
				SessionIdQueue.Enqueue(i);
			}
		}

		public void Dispose()
		{
			foreach (var session in Sessions.Values)
			{
				SessionIdQueue.Enqueue(session.Id);
				session.Dispose();
			}

			Sessions.Clear();
		}

		public IBattleSession CreateSession()
		{
			var id = SessionIdQueue.Dequeue();
			var session = new TurnBattleSession(id);

			Sessions.Add(id, session);

			return session;
		}

		public bool TryGetSession(int id, out IBattleSession session)
		{
			return Sessions.TryGetValue(id, out session);
		}

		public void ReleaseSession(IBattleSession battleSession)
		{
			SessionIdQueue.Enqueue(battleSession.Id);
			Sessions.Remove(battleSession.Id);

			battleSession.Dispose();
		}

		public bool TryGetCharacter(int sessionId, int characterId, out IBattleCharacter battleCharacter)
		{
			battleCharacter = default;

			if (Sessions.TryGetValue(sessionId, out var session) &&
				session.TryGetCharacter(characterId, out battleCharacter))
			{
				return true;
			}

			return false;
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
