using System;
using BlitzEcs;

namespace Core
{
	public readonly struct EntityHandle
	{
		private World World { get; }

		public int Id { get; }

		public bool IsEmpty => Id == -1;

		public bool IsAlive => World != null && World.IsEntityAlive(Id);

		public EntityHandle(World world, int id)
		{
			World = world;
			Id = id;
		}

		public EntityHandle Add<TComponent>() where TComponent : struct
		{
			World.GetComponentPool<TComponent>().Add(Id);
			return this;
		}

		public EntityHandle Add<TComponent>(TComponent component) where TComponent : struct
		{
			World.GetComponentPool<TComponent>().Add(Id, component);
			return this;
		}

		public ref TComponent Get<TComponent>() where TComponent : struct
		{
			return ref World.GetComponentPool<TComponent>().Get(Id);
		}

		public ref TComponent GetUnsafe<TComponent>() where TComponent : struct
		{
			return ref World.GetComponentPool<TComponent>().GetUnsafe(Id);
		}

		/// <summary>
		/// 가져온 컴포넌트는 무조건 읽기 전용이므로 값을 설정해도 변하지 않는다.
		/// 따라서 가져온 후 Assign을 실행해야한다
		/// </summary>
		/// <param name="component"></param>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns></returns>
		public bool TryGet<TComponent>(out TComponent component) where TComponent : struct
		{
			component = default;

			if (Has<TComponent>())
			{
				component = Get<TComponent>();
				return true;
			}

			return false;
		}

		public void Assign<TComponent>(in TComponent component) where TComponent : struct
		{
			World.GetComponentPool<TComponent>().Add(Id, component);
		}

		public EntityHandle Remove<TComponent>() where TComponent : struct
		{
			World.GetComponentPool<TComponent>().Remove(Id);
			return this;
		}

		public bool Has<TComponent>() where TComponent : struct
		{
			return World.GetComponentPool<TComponent>().Contains(Id);
		}

		public bool Has(Type type)
		{
			if (World.TryGetIComponentPool(type, out var pool))
			{
				return pool.Contains(Id);
			}

			return false;
		}

		/// <summary>
		/// Entity 직접 비교 시에도 id만 비교한 결과물과 동일하도록 함
		/// </summary>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
