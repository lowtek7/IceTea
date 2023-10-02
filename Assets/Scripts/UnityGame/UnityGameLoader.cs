using System;
using Game;
using Game.Battle.TurnSystem;
using Service;
using Service.Game.Battle;
using UnityEngine;
using UnityService;

namespace UnityGame
{
	public class UnityGameLoader : MonoBehaviour
	{
		[SerializeField]
		private UnityServiceAutoInjector injector;

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
			}
		}
	}
}