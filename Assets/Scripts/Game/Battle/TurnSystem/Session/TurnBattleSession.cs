using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base.Coroutines;
using Core;
using Game.Battle.TurnSystem.Action;
using Game.Battle.TurnSystem.Entity;
using Game.Battle.TurnSystem.Message;
using Service;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Session
{
	public class TurnBattleSession : IBattleSession
	{
		private int currentTurn = 0;

		private List<IBattleLog> BattleLogs { get; }

		private Dictionary<int, TurnBattleCharacter> Characters { get; }

		private List<int> TurnTable { get; }

		private int currentTurnCursor = 0;

		public TurnBattleSession(int id)
		{
			Id = id;
			IsAlive = true;
			IsRunning = false;
			BattleLogs = new List<IBattleLog>(128);
			Characters = new Dictionary<int, TurnBattleCharacter>();
			TurnTable = new List<int>(200);
			turnProcessHandle = default;
		}

		public int Id { get; }

		public bool IsAlive { get; private set; }

		public bool IsRunning { get; private set; }

		public IEnumerable<int> CharacterIds => Characters.Keys;

		private CoroutineHandle turnProcessHandle;

		public void UpdateProcess(float deltaTime)
		{
		}

		public void LateUpdateProcess(float deltaTime)
		{
		}

		/// <summary>
		/// 턴 증감 내부 처리
		/// </summary>
		private void IncreaseTurn()
		{
			TurnTable.Clear();
			currentTurnCursor = 0;
			currentTurn++;

			if (ServiceManager.TryGetService(out IRandomService randomService))
			{
				randomService.Rand();
			}
		}

		public void Dispose()
		{
			Stop();
			IsAlive = false;
		}

		public void Start()
		{
			if (!IsRunning && ServiceManager.TryGetService(out ICoroutineService coroutineService))
			{
				turnProcessHandle = coroutineService.Run(TurnProcess());
			}

			IsRunning = true;
		}

		public void Stop()
		{
			// 턴이 진행 중이면 강제로 종료
			if (IsRunning)
			{
				turnProcessHandle.Stop();
			}

			IsRunning = false;
			Clear();
		}

		/// <summary>
		/// 내부 정보들을 청소
		/// </summary>
		private void Clear()
		{
			currentTurn = 0;
			currentTurnCursor = 0;
			TurnTable.Clear();
		}

		public IBattleLog GetLog(int logId)
		{
			if (logId >= 0 && BattleLogs.Count > logId)
			{
				return BattleLogs[logId];
			}

			return EmptyLog.Instance;
		}

		public bool TryGetCharacter(int id, out IBattleCharacter character)
		{
			character = default;

			if (Characters.TryGetValue(id, out var result))
			{
				character = result;
				return true;
			}

			return false;
		}

		public IBattleObject RegisterEntity(IEntity entity, IBattleEntityController battleEntityController)
		{
			var result = new TurnBattleCharacter(Id, entity, battleEntityController as ITurnBattleCharacterController);

			Characters.Add(result.Id, result);

			if (battleEntityController is ITurnBattleCharacterController turnBattleCharacterController)
			{
				turnBattleCharacterController.Init(result);
			}

			return result;
		}

		public void UnregisterEntity(IEntity entity)
		{
			if (Characters.TryGetValue(entity.Id, out var character))
			{
				if (character is IDisposable disposable)
				{
					disposable.Dispose();
				}

				Characters.Remove(entity.Id);
			}
		}

		// 배틀로그 기능을 언젠가 재활성화 해야해서 주석으로 기록을 남겨둠
		// public void ExecuteBattleAction(IBattleObject from, IBattleObject to, ITurnBattleAction battleAction)
		// {
		// 	if (ServiceManager.TryGetService(out IMessageService messageService))
		// 	{
		// 		int logId = BattleLogs.Count;
		// 		int seed = -1;
		// 		ulong step = 0;
		//
		// 		if (ServiceManager.TryGetService(out IRandomService randomService))
		// 		{
		// 			seed = randomService.Seed;
		// 			step = randomService.Step;
		// 		}
		//
		// 		// 배틀 로그 생성
		// 		var battleLog = new TurnBattleLog(logId, seed, step, from, to, battleAction);
		// 		BattleLogs.Add(battleLog);
		// 		battleAction.Execute(from, to, this, battleLog);
		// 		// 여기서 로그를 생성한다.
		// 		messageService.Publish(BattleActionExecutedMessage.Create(from, to, battleAction, logId));
		// 	}
		// }

		private IEnumerator TurnProcess()
		{
			while (IsRunning)
			{
				// turn table 생성
				TurnTable.Clear();

				// 일단 캐릭터의 스피드를 중심으로 turn table 생성
				TurnTable.AddRange(Characters
					.OrderByDescending(x =>
					{
						var speed = Convert.ToSingle(x.Value.Speed);
						var weight = x.Value.TurnBattleCharacterController is ITurnBattlePlayableCharacterController
							? 0.5f
							: 0f;

						return speed + weight;
					})
					.Select(x => x.Key));

				currentTurnCursor = 0;

				while (TurnTable.Count > currentTurnCursor && IsRunning)
				{
					var currentCharacterId = TurnTable[currentTurnCursor];

					if (Characters.TryGetValue(currentCharacterId, out var character))
					{
						yield return character.TurnProcess(currentTurnCursor, this);
					}

					++currentTurnCursor;
				}

				if (IsRunning)
				{
					IncreaseTurn();
				}
			}
		}
	}
}
