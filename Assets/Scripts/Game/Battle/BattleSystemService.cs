using System;
using System.Collections.Generic;
using Core;
using Service.Game.Battle;

namespace Game.Battle
{
	public class BattleSystemService : IBattleSystemService, IOrderUpdate, IOrderLateUpdate, IDisposable
	{
		public int UpdateOrder => -1;

		public int LateUpdateOrder => -1;

		private readonly Dictionary<Type, IBattleSystem> battleSystems = new Dictionary<Type, IBattleSystem>();

		public void Init()
		{
		}

		public void Dispose()
		{
			battleSystems.Clear();
		}

		public T CreateSystem<T>() where T : IBattleSystem, new()
		{
			var systemType = typeof(T);

			if (battleSystems.TryGetValue(systemType, out var system))
			{
				return (T)system;
			}

			var resultSystem = new T();

			resultSystem.Init();

			battleSystems.Add(systemType, resultSystem);

			return resultSystem;
		}

		public bool TryGetSystem<T>(out T system) where T : IBattleSystem
		{
			system = default;

			if (battleSystems.TryGetValue(typeof(T), out var result) &&
				result is T output)
			{
				system = output;
				return true;
			}

			return false;
		}

		public bool DestroySystem<T>() where T : IBattleSystem
		{
			var systemType = typeof(T);

			if (battleSystems.TryGetValue(systemType, out var system))
			{
				system.Dispose();
				battleSystems.Remove(systemType);

				return true;
			}

			return false;
		}

		public void UpdateProcess(float deltaTime)
		{
			foreach (var battleSystem in battleSystems.Values)
			{
				battleSystem.UpdateProcess(deltaTime);
			}
		}

		public void LateUpdateProcess(float deltaTime)
		{
			foreach (var battleSystem in battleSystems.Values)
			{
				battleSystem.LateUpdateProcess(deltaTime);
			}
		}
	}
}
