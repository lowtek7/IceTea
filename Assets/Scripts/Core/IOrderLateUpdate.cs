namespace Core
{
	public interface IOrderLateUpdate : ILateUpdate
	{
		int LateUpdateOrder { get; }
	}
}
