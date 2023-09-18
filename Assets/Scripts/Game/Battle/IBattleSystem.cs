namespace Game.Battle
{
	public interface IBattleSystem
	{
		/// <summary>
		/// 배틀은 기본적으로 세션 별로 나뉘어져 있다.
		/// 따라서 새로운 배틀을 발생 시킬 때는 새로운 세션을 생성해야한다.
		/// </summary>
		IBattleSession CreateSession();

		void ReleaseSession(IBattleSession battleSession);
	}
}
