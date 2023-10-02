namespace Service.Game.Battle
{
	public interface IBattleCharacter : IBattleObject
	{
		IBattleEntityController BattleEntityController { get; }
	}
}
