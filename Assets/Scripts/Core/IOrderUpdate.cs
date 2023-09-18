namespace Core
{
	/// <summary>
	/// 순서가 있는 업데이트
	/// </summary>
	public interface IOrderUpdate : IUpdate
	{
		int UpdateOrder { get; }
	}
}
