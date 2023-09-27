namespace Game.Battle.TurnSystem.Action
{
	public enum MotionType
	{
		Unknown = -1,
		Attack = 0,
		GroupAttack,
		Cast = 10,
		Guard = 20,
		Custom = 1000,
	}
}
