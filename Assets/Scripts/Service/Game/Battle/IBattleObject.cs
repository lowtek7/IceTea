using Core;
using Vim.Math3d;

namespace Service.Game.Battle
{
	public interface IBattleObject : IEntity
	{
		Vector3 Position { get; set; }

		int SessionId { get; }
	}
}
