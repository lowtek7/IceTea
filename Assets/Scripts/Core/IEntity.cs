using System.Collections.Generic;

namespace Core
{
	public interface IEntity
	{
		int Id { get; }
	}

	public interface IAttachment
	{
		void AttachTo(IEntity entity);

		void DetachFrom(IEntity entity);
	}
}
