using System.Collections.Generic;
using Core;
using Game.Battle.TurnSystem.Action;
using Game.Battle.TurnSystem.Message;
using Game.Battle.TurnSystem.Session.Component;
using Service;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Session
{
	public class TurnBattleSession : IBattleSession
	{
		private int currentTurn = 0;

		private IGameWorld GameWorld { get; set; }

		private EntityHandle SessionHandle { get; set; }

		private List<IBattleLog> BattleLogs { get; set; }

		public TurnBattleSession(IGameWorld world, EntityHandle entityHandle)
		{
			GameWorld = world;
			SessionHandle = entityHandle;
			BattleLogs = new List<IBattleLog>(128);

			SessionHandle.Add<SessionComponent>();
		}

		public int Id => SessionHandle.Id;

		public bool IsAlive => SessionHandle.IsAlive;

		public bool IsRunning
		{
			get
			{
				if (SessionHandle.IsAlive &&
					SessionHandle.TryGet(out SessionComponent sessionComponent))
				{
					return sessionComponent.IsRunning;
				}

				return false;
			}
		}

		public void UpdateProcess(float deltaTime)
		{
			throw new System.NotImplementedException();
		}

		public void LateUpdateProcess(float deltaTime)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// 턴 증감 내부 처리
		/// </summary>
		private void IncreaseTurn()
		{
			currentTurn++;

			if (ServiceManager.TryGetService(out IRandomService randomService))
			{
				randomService.Rand();
			}
		}

		public void Dispose()
		{
			GameWorld.DestroyEntity(SessionHandle);

			SessionHandle = EntityHandle.Empty;
			GameWorld = null;
		}
		public IBattleObject RegisterEntity(EntityHandle entityHandle)
		{
			throw new System.NotImplementedException();
		}

		public bool UnregisterEntity(EntityHandle entityHandle)
		{
			throw new System.NotImplementedException();
		}

		public void Start()
		{
			throw new System.NotImplementedException();
		}

		public void Stop()
		{
			throw new System.NotImplementedException();
		}

		public IBattleLog GetLog(int logId)
		{
			if (logId >= 0 && BattleLogs.Count > logId)
			{
				return BattleLogs[logId];
			}

			return EmptyLog.Instance;
		}

		public void ExecuteBattleAction(IBattleObject from, IBattleObject to, ITurnBattleAction battleAction)
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
				int logId = BattleLogs.Count;
				int seed = -1;
				ulong step = 0;

				if (ServiceManager.TryGetService(out IRandomService randomService))
				{
					seed = randomService.Seed;
					step = randomService.Step;
				}

				// 배틀 로그 생성
				var battleLog = new TurnBattleLog(logId, seed, step, from, to, battleAction);
				BattleLogs.Add(battleLog);
				battleAction.Execute(from, to, this, battleLog);
				// 여기서 로그를 생성한다.
				messageService.Publish(BattleActionExecutedMessage.Create(from, to, battleAction, logId));
			}
		}
	}
}
