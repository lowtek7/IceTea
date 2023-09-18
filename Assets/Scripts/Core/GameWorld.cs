using System;
using BlitzEcs;

namespace Core
{
	/// <summary>
	/// 내부 데이터는 ECS를 사용해서 보관한다.
	/// </summary>
	public class GameWorld : IGameWorld, IUpdate, ILateUpdate, IDisposable
	{
		private World gameWorld;

		public void Init()
		{
			gameWorld = new World();
		}

		public void Dispose()
		{
			gameWorld = null;
			GC.Collect();
		}

		public void UpdateProcess(float deltaTime)
		{
		}

		public void LateUpdateProcess(float deltaTime)
		{
		}

		public EntityHandle CreateEntity()
		{
			if (gameWorld == null)
			{
				return new EntityHandle(null, -1);
			}

			var entity = gameWorld.Spawn();

			return new EntityHandle(gameWorld, entity.Id);
		}

		public EntityHandle GetEntity(int id)
		{
			return new EntityHandle(gameWorld, id);
		}

		public void DestroyEntity(EntityHandle entityHandle)
		{
			if (entityHandle.IsAlive)
			{
				gameWorld.Despawn(entityHandle.Id);
			}
		}
	}
}
