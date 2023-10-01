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

		private List<IBattleLog> BattleLogs { get; set; }

		public TurnBattleSession(int id)
		{
			Id = id;
			BattleLogs = new List<IBattleLog>(128);
		}

		public int Id { get; }

		public bool IsAlive { get; }

		public bool IsRunning
		{
			get
			{
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
