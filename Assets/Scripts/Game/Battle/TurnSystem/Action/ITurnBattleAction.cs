using System.Collections;
using Game.Battle.TurnSystem.Entity;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Action
{
	public enum TargetingMode
	{
		Self = 0,
		AllTeam,
		AllEnemy,
		SingleTeam,
		SingleEnemy,
		MultiTeam,
		MultiEnemy,
	}

	/// <summary>
	/// 턴 배틀 액션은 조합형으로 디자인 가능하게
	/// </summary>
	public interface ITurnBattleAction : IBattleAction
	{
		TargetingMode TargetingMode { get; }

		/// <summary>
		/// 멀티 타겟팅의 경우에만 사용.
		/// </summary>
		int TargetingCount { get; }

		MotionType MotionType { get; }

		/// <summary>
		/// 배틀 액션 실행.
		/// 실행 후 로그에 배틀 액션의 결과를 저장한다.
		/// </summary>
		IEnumerator Execute(IBattleCharacter self, ITurnBattleSelector battleSelector, IBattleSession battleSession);
	}
}
