namespace Service.Game.Battle
{
	public interface IBattleSystemService : IGameService
	{
		/// <summary>
		/// 항상 시스템은 타입별로 하나의 고유한 인스턴스만 존재 할 수 있다.
		/// 만약 시스템이 이미 생성 되었다면 생성되어 있는 시스템을 리턴하게 된다.
		/// TODO: 생성되어 있을 경우 이미 생성된 인스턴스를 반환하는게 올바른 행위인지 생각 해볼것
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T CreateSystem<T>() where T : IBattleSystem, new();

		bool DestroySystem<T>() where T : IBattleSystem;
	}
}
