using System;
using Core;
using Core.MessageSystem;
using Service;
using UnityEngine;
using IMessageReceiver = Service.IMessageReceiver;

namespace UnityService.MessageService
{
	[UnityService(typeof(IMessageService))]
	public class MessageService : MonoBehaviour, IMessageService
	{
		private readonly MessageSystem messageSystem = new MessageSystem();

		public void Init(IGameWorld gameWorld)
		{
		}

		private void OnDestroy()
		{
			if (messageSystem is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		public SubscribeHandler Subscribe<T>(Predicate<IMessage> callback) where T : IMessage
		{
			return messageSystem.Subscribe<T>(callback);
		}

		public void Publish(IMessage message)
		{
			messageSystem.RecvMessage(message);
		}

		public void Send(IMessageReceiver receiver, IMessage message)
		{
			receiver.OnRecvMessage(message);
		}
	}
}
