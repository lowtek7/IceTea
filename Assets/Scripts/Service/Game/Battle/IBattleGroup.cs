namespace Service.Game.Battle
{
	public interface IBattleGroup : IBattleObject
	{
		/// <summary>
		/// 그룹 내부의 오브젝트들
		/// </summary>
		public IBattleObject[] GroupObjects { get; }
	}
}
