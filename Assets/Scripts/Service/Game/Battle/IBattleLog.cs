namespace Service.Game.Battle
{
	/// <summary>
	/// 배틀에서 발생한 로그
	/// 로그는 세션마다 기록되고 있다.
	/// </summary>
	public interface IBattleLog
	{
		int LogId { get; }
	}

	public class EmptyLog : IBattleLog
	{
		public int LogId => -1;

		public static EmptyLog Instance { get; } = new EmptyLog();

		private EmptyLog()
		{
		}
	}
}
