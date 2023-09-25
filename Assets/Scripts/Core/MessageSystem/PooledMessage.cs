using System;
using Base;

namespace Core.MessageSystem
{
	public abstract class PooledMessage<T> : IPooledMessage, IPoolItem, IDisposable
		where T : class, new()
	{
		private static readonly ObjectPool<T> Pool = new ObjectPool<T>();

		protected static T GetOrCreate()
		{
			return Pool.Create();
		}

		public void Return()
		{
			Pool.Return(this as T);
		}

		/// <summary>
		/// Pool에서 GetOrCreate될때 이벤트
		/// </summary>
		public virtual void OnCreate()
		{

		}

		/// <summary>
		/// Pool로 돌아갈때 이벤트
		/// </summary>
		public virtual void OnReturn()
		{

		}

		public void Dispose()
		{
			Return();
		}
	}
}
