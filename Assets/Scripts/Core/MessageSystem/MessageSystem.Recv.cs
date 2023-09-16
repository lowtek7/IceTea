using System;

namespace Core.MessageSystem
{
	public interface IMessageReceiver
	{
		void RecvMessage(IMessage message);

		void RecvMessage<T>(T message) where T : IMessage;
	}

	public partial class MessageSystem : IMessageReceiver
	{
		public void RecvMessage(IMessage message)
		{
			var type = message.GetType();

			RecvMessageInternal(type, message);
		}

		public void RecvMessage<T>(T message) where T : IMessage
		{
			RecvMessageInternal(typeof(T), message);
		}

		private void RecvMessageInternal(Type messageType, IMessage message)
		{
			if (messageTypeToSubscribers.TryGetValue(messageType, out var innerSubscribers))
			{
				foreach (var subscriber in innerSubscribers.Values)
				{
					subscriber.Invoke(message);
				}
			}
		}
	}
}
