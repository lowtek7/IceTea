using System;
using System.Collections.Generic;

namespace Core.MessageSystem
{
	public interface ISubscriberManager
	{
		bool IsAlive(Guid guid);

		void Unsubscribe(Guid guid);
	}

	readonly struct Subscriber
	{
		internal Guid Id { get; }

		internal Type Type { get; }

		private Predicate<IMessage> Callback { get; }

		internal Subscriber(Guid id, Type type, Predicate<IMessage> callback)
		{
			Id = id;
			Type = type;
			Callback = callback;
		}

		internal void Invoke(IMessage message)
		{
			Callback.Invoke(message);
		}
	}

	public partial class MessageSystem : ISubscriberManager, IDisposable
	{
		private readonly Dictionary<Type, Dictionary<Guid, Subscriber>> messageTypeToSubscribers = new Dictionary<Type, Dictionary<Guid, Subscriber>>();

		private readonly Dictionary<Guid, Subscriber> subscribers = new Dictionary<Guid, Subscriber>();

		public void Init()
		{
		}

		/// <summary>
		/// 구독 함수. 무조건 구독하면 핸들러를 반환하는데 가지고 있어야 한다.
		/// 핸들러를 통해서 구독 해제를 하는 구조.
		/// 구독 핸들러를 분실했다면 게임이 끝날때 에러 로그 출력.
		/// </summary>
		/// <param name="callback"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public SubscribeHandler Subscribe<T>(Predicate<IMessage> callback) where T : IMessage
		{
			var type = typeof(T);
			var id = Guid.NewGuid();
			var handler = new SubscribeHandler(id, this);
			var subscriber = new Subscriber(id, type, callback);

			if (!messageTypeToSubscribers.TryGetValue(type, out var innerSubscribers))
			{
				innerSubscribers = new Dictionary<Guid, Subscriber>();
				messageTypeToSubscribers[type] = innerSubscribers;
			}

			innerSubscribers.Add(id, subscriber);
			subscribers.Add(id, subscriber);

			return handler;
		}

		private void UnsubscribeInternal(Guid guid)
		{
			if (subscribers.TryGetValue(guid, out var subscriber))
			{
				var type = subscriber.Type;

				if (messageTypeToSubscribers.TryGetValue(type, out var innerSubscribers))
				{
					innerSubscribers.Remove(guid);
				}

				subscribers.Remove(guid);
			}
		}

		/// <summary>
		/// 해당 구독자가 생존하고 있는지 검사
		/// </summary>
		/// <param name="guid">구독자 id</param>
		/// <returns></returns>
		bool ISubscriberManager.IsAlive(Guid guid)
		{
			return subscribers.ContainsKey(guid);
		}

		void ISubscriberManager.Unsubscribe(Guid guid)
		{
			UnsubscribeInternal(guid);
		}

		void IDisposable.Dispose()
		{
			// 게임이 끝나는데도 남아있는 메세지들에 대한 에러 로그 출력
			foreach (var keyValuePair in subscribers)
			{

			}

			messageTypeToSubscribers.Clear();
			subscribers.Clear();
		}
	}
}
