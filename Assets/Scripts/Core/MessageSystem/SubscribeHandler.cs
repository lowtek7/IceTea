using System;

namespace Core.MessageSystem
{
	public readonly struct SubscribeHandler : IDisposable
	{
		private Guid Id { get; }

		private ISubscriberManager SubscriberManager { get; }

		/// <summary>
		/// 해당 구독 핸들러가 살아있는지 검사
		/// </summary>
		public bool IsAlive => Id != Guid.Empty && SubscriberManager.IsAlive(Id);

		internal static SubscribeHandler Empty
		{
			get
			{
				var result = new SubscribeHandler(Guid.Empty, default);

				return result;
			}
		}

		internal SubscribeHandler(Guid id, ISubscriberManager subscriberManager)
		{
			Id = id;
			SubscriberManager = subscriberManager;
		}

		public void Dispose()
		{
			if (Id != Guid.Empty)
			{
				SubscriberManager.Unsubscribe(Id);
			}
		}
	}
}
