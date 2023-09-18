using Core;
using Game.Battle.TurnSystem.Session.Component;
using Service.Game.Battle;

namespace Game.Battle.TurnSystem.Session
{
	public class TurnBattleSession : IBattleSession
	{
		private IGameWorld GameWorld { get; set; }

		private EntityHandle SessionHandle { get; set; }

		public TurnBattleSession(IGameWorld world, EntityHandle entityHandle)
		{
			GameWorld = world;
			SessionHandle = entityHandle;

			SessionHandle.Add<SessionComponent>();
		}

		public int Id => SessionHandle.Id;

		public bool IsAlive => SessionHandle.IsAlive;

		public bool IsRunning
		{
			get
			{
				if (SessionHandle.IsAlive &&
					SessionHandle.TryGet(out SessionComponent sessionComponent))
				{
					return sessionComponent.IsRunning;
				}

				return false;
			}
		}

		public void UpdateProcess(float deltaTime)
		{
			throw new System.NotImplementedException();
		}

		public void LateUpdateProcess(float deltaTime)
		{
			throw new System.NotImplementedException();
		}

		public void Dispose()
		{
			GameWorld.DestroyEntity(SessionHandle);

			SessionHandle = EntityHandle.Empty;
			GameWorld = null;
		}
		public IBattleObject RegisterEntity(EntityHandle entityHandle)
		{
			throw new System.NotImplementedException();
		}

		public bool UnregisterEntity(EntityHandle entityHandle)
		{
			throw new System.NotImplementedException();
		}

		public void Start()
		{
			throw new System.NotImplementedException();
		}

		public void Stop()
		{
			throw new System.NotImplementedException();
		}
	}
}
