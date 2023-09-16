using System.Collections;
using Base.Coroutines;
using Core;

namespace Service
{
	public interface ICoroutineService : IGameService, IUpdate
	{
		CoroutineHandle Run(float delay, IEnumerator routine);

		CoroutineHandle Run(IEnumerator routine);

		bool TryRun(float delay, IEnumerator routine, out CoroutineHandle coroutineHandle);

		bool TryRun(IEnumerator routine, out CoroutineHandle coroutineHandle);
	}
}
