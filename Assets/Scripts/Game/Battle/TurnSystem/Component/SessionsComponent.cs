using System.Collections.Generic;

namespace Game.Battle.TurnSystem.Component
{
	/// <summary>
	/// 턴 배틀 시스템의 세션들의 정보가 기록된 컴포넌트
	/// </summary>
	public struct SessionsComponent
	{
		/// <summary>
		/// 현재 세션들
		/// </summary>
		public List<int> SessionIdList { get; set; }
	}
}
