using System;
using Core;

namespace Service.Character
{
	public interface ICharacter : IEntity, IDisposable
	{
		ICharacterSpec Spec { get; }
	}
}
