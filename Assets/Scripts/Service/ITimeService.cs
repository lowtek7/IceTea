namespace Service
{
	public interface ITimeService : IGameService
	{
		float DeltaTime { get; }
	}

	/// <summary>
	/// 타임 서비스를 편하게 이용하기 위한 헬퍼
	/// </summary>
	public static class TimeServiceHelper
	{
		public static float DeltaTime
		{
			get
			{
				if (ServiceManager.TryGetService(out ITimeService timeService))
				{
					return timeService.DeltaTime;
				}

				return 0f;
			}
		}
	}
}
