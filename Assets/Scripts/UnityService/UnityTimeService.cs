using Service;
using UnityEngine;

namespace UnityService
{
	[UnityService(typeof(ITimeService))]
	public class UnityTimeService : MonoBehaviour, ITimeService
	{
		public float DeltaTime => Time.deltaTime;

		public void Init()
		{
		}
	}
}
