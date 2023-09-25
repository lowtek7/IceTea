namespace Service.Character
{
	public interface ICharacterSpec
	{
		/// <summary>
		/// 타입 Id (flag 형식으로 사용)
		/// </summary>
		int TypeId { get; }

		/// <summary>
		/// 현재 체력
		/// </summary>
		int Hp { get; }

		/// <summary>
		/// 최대 체력
		/// </summary>
		int MaxHp { get; }

		/// <summary>
		/// 현재 에너지
		/// </summary>
		int Energy { get; }

		/// <summary>
		/// 최대 에너지
		/// </summary>
		int MaxEnergy { get; }

		/// <summary>
		/// 체력 회복율 (시간 기반. 턴이 될 수도 있음)
		/// </summary>
		int HpRegenPer { get; }

		/// <summary>
		/// 공격력
		/// </summary>
		float Atk { get; }

		/// <summary>
		/// 방어력
		/// </summary>
		float Def { get; }

		/// <summary>
		/// 속도
		/// </summary>
		float Speed { get; }

		/// <summary>
		/// 치명타 확률
		/// </summary>
		float CriticalRate { get; }

		/// <summary>
		/// 치명타 피해
		/// </summary>
		float CriticalDamage { get; }

		float GetValue(CharacterStatType statType, int customIndex);
	}
}
