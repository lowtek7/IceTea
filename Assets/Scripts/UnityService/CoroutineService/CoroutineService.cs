using System;
using System.Collections;
using Base.Coroutines;
using Core;
using Service;
using UnityEngine;

namespace UnityService.CoroutineService
{
	[UnityService(typeof(ICoroutineService))]
	public class CoroutineService : MonoBehaviour, ICoroutineService
	{
		private CoroutineRunner runner;

		private void OnDestroy()
		{
			runner.StopAll();
			runner = null;
		}

		public void Init()
		{
			runner = new CoroutineRunner();
		}

		public void UpdateProcess(float deltaTime)
		{
			runner.Update(deltaTime);
		}

		public CoroutineHandle Run(float delay, IEnumerator routine)
		{
			return runner.Run(delay, routine);
		}

		public CoroutineHandle Run(IEnumerator routine)
		{
			return runner.Run(routine);
		}

		public bool TryRun(float delay, IEnumerator routine, out CoroutineHandle coroutineHandle)
		{
			coroutineHandle = default;

			if (runner != null)
			{
				coroutineHandle = runner.Run(delay, routine);
				return true;
			}

			return false;
		}

		public bool TryRun(IEnumerator routine, out CoroutineHandle coroutineHandle)
		{
			coroutineHandle = default;

			if (runner != null)
			{
				coroutineHandle = runner.Run(routine);
				return true;
			}

			return false;
		}
	}
}
