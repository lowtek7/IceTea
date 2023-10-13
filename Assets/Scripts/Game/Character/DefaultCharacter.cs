using System.Collections.Generic;
using Core;
using Service.Character;

namespace Game.Character
{
	public class DefaultCharacter : ICharacter
	{
		private readonly List<IEntity> children = new List<IEntity>();

		public int Id { get; }

		public IEnumerable<IEntity> Children => children;

		public void AddChild(IEntity entity)
		{
			children.Add(entity);

			if (entity is IAttachment attachment)
			{
				attachment.AttachTo(this);
			}
		}

		public void RemoveChild(IEntity entity)
		{
			if (entity is IAttachment attachment)
			{
				attachment.DetachFrom(this);
			}

			children.Remove(entity);
		}

		public ICharacterSpec Spec { get; }

		public DefaultCharacter(int id)
		{
			Id = id;
			Spec = new DefaultCharacterSpec(30, 10, 3, 0, 1);

			// 초기화를 나중에 만들게되면 거기서 add child하게 변경.
			AddChild(Spec);
		}

		public void Dispose()
		{
			RemoveChild(Spec);
		}
	}
}
