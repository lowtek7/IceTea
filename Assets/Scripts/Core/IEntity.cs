using System.Collections.Generic;

namespace Core
{
	public interface IEntity
	{
		int Id { get; }

		IEnumerable<IEntity> Children { get; }

		void AddChild(IEntity entity);

		void RemoveChild(IEntity entity);
	}

	public interface IAttachment
	{
		void AttachTo(IEntity entity);

		void DetachFrom(IEntity entity);
	}
}
