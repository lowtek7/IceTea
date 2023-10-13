using System;
using System.Collections.Generic;
using Core;
using Service.Character;

namespace Game.Character
{
	public class DefaultCharacterSpec : ICharacterSpec, IAttachment
	{
		private int typeId = 0;
		private int hp;
		private int maxHp;
		private int energy;
		private int maxEnergy;
		private int hpRegenPer = 0;
		private float atk;
		private float def;
		private float speed;
		private readonly float criticalRate = 0.25f;
		private readonly float criticalDamage = 1.5f;

		public DefaultCharacterSpec(int maxHp, int maxEnergy, float atk, float def, float speed)
		{
			this.hp = maxHp;
			this.maxHp = maxHp;
			this.energy = maxEnergy;
			this.maxEnergy = maxEnergy;
			this.atk = atk;
			this.def = def;
			this.speed = speed;
		}

		public int TypeId => typeId;

		public int Hp => hp;

		public int MaxHp => maxHp;

		public int Energy => energy;

		public int MaxEnergy => maxEnergy;

		public int HpRegenPer => hpRegenPer;

		public float Atk => atk;

		public float Def => def;

		public float Speed => speed;

		public float CriticalRate => criticalRate;

		public float CriticalDamage => criticalDamage;

		public float GetValue(CharacterStatType statType, int customIndex)
		{
			return CharacterSpecExtensions.GetValue(this, statType, customIndex);
		}

		public void AttachTo(IEntity entity)
		{
			id = entity.Id;
		}

		public void DetachFrom(IEntity entity)
		{
			id = -1;
		}

		private int id = -1;

		public int Id => id;

		public IEnumerable<IEntity> Children => Array.Empty<IEntity>();

		public void AddChild(IEntity entity)
		{
		}

		public void RemoveChild(IEntity entity)
		{
		}
	}
}
