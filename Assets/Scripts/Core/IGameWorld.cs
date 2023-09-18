namespace Core
{
	public interface IGameWorld
	{
		EntityHandle CreateEntity();

		EntityHandle GetEntity(int id);

		void DestroyEntity(EntityHandle entityHandle);
	}
}
