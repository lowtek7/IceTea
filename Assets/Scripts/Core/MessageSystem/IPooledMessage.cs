namespace Core.MessageSystem
{
	public interface IPooledMessage : IMessage
	{
		void Return();
	}
}
