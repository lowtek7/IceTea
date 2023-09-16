using System;
using Core.MessageSystem;

namespace Service
{
	public interface IMessageReceiver
	{
		void OnRecvMessage(IMessage message);
	}

	public interface IMessageService : IGameService
	{
		SubscribeHandler Subscribe<T>(Predicate<IMessage> callback) where T : IMessage;

		void Publish(IMessage message);

		void Send(IMessageReceiver receiver, IMessage message);
	}
}
