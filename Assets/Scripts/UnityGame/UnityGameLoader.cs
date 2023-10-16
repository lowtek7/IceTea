using System;
using Game;
using Game.Battle.TurnSystem;
using Game.Character;
using Service;
using Service.Game.Battle;
using UnityEngine;
using UnityGame.Battle;
using UnityService;

namespace UnityGame
{
	public class UnityGameLoader : MonoBehaviour
	{
		[SerializeField]
		private UnityServiceAutoInjector injector;

		[SerializeField]
		private PlayerController playerController;

		// 임시적으로 처리
		[SerializeField]
		private TurnBattleCharacterBehaviour player;

		// 임시적으로 처리
		[SerializeField]
		private TurnBattleCharacterBehaviour enemy;

		private void Start()
		{
			injector.Inject();

			var gameLoader = new GameLoader();

			gameLoader.Load();

			ServiceManager.Init();

			if (ServiceManager.TryGetService(out IBattleSystemService battleSystemService))
			{
				var turnBattleSystem = battleSystemService.CreateSystem<TurnBattleSystem>();

				turnBattleSystem.Init();

				Debug.Log("TurnBattleSystem:Init()");

				var session = turnBattleSystem.CreateSession();

				playerController.SetSession(session);

				//임시적으로 플레이어와 적의 컨트롤러 생성 후 셋팅...
				var playerCharacter = new DefaultCharacter(1);

				//커플링이 있어서 이 결합을 풀어내야함.
				var result = session.RegisterEntity(playerCharacter, new PlayerBattleController());

				player.Init(result as IBattleCharacter);

				var enemyCharacter = new DefaultCharacter(2);

				result = session.RegisterEntity(enemyCharacter, new FoolEnemyController());

				enemy.Init(result as IBattleCharacter);

				session.Start();
			}
		}

		private void Update()
		{
			if (ServiceManager.TryGetService(out ICoroutineService coroutineService))
			{
				coroutineService.UpdateProcess(Time.deltaTime);
			}
		}
	}
}
