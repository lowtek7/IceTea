using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Action
{
	/// <summary>
	/// 턴 배틀 액션은 조합형으로 디자인 가능하게
	/// </summary>
	public interface ITurnBattleAction : IBattleAction
	{
		MotionType MotionType { get; }

		/// <summary>
		/// 배틀 액션 실행.
		/// 실행 후 로그에 배틀 액션의 결과를 저장한다.
		/// </summary>
		/// <param name="from">실행자</param>
		/// <param name="to">대상</param>
		/// <param name="battleSession">배틀 세션</param>
		/// <param name="battleLog">기록할 로그</param>
		void Execute(IBattleObject from, IBattleObject to, IBattleSession battleSession, TurnBattleLog battleLog);
	}
}
