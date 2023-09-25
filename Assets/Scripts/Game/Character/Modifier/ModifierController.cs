using System;
using Base.Utility;

namespace Game.Character.Modifier
{
	public class ModifierController
	{
		private bool isDirty;
		private bool isDirtySort;
		private float baseValue;

		private float value;

		public float Value
		{
			get
			{
				if (isDirty)
				{
					CalculateProcess();
					isDirty = false;
				}

				return value;
			}
		}

		public int IntValue => MathUtil.RoundToInt(Value);

		public ModifierController()
		{
			isDirty = true;

			isDirtySort = true;

			baseValue = 0;
			value = 0;
		}

		private void CalculateProcess()
		{
			// if (isDirtySort)
			// {
			// 	modifiers.Sort(CompareModifierOrder);
			// 	isDirtySort = false;
			// }
			//
			float finalValue = baseValue;
			//
			// float sumPercentAdd = 0;
			// float sumSetValue = 0;
			// int setCount = 0;
			//
			// for (int i = 0; i < modifiers.Count; i++)
			// {
			// 	IAttributeModifier mod = modifiers[i];
			// 	if (mod.Mod == StatModType.Flat)
			// 	{
			// 		finalValue += mod.Value;
			// 	}
			// 	else if (mod.Mod == StatModType.PercentAdd)
			// 	{
			// 		sumPercentAdd += mod.Value;
			//
			// 		if (i + 1 >= modifiers.Count || modifiers[i + 1].Mod != StatModType.PercentAdd)
			// 		{
			// 			finalValue *= 1 + sumPercentAdd;
			// 			sumPercentAdd = 0;
			// 		}
			// 	}
			// 	else if (mod.Mod == StatModType.PercentSet)
			// 	{
			// 		finalValue *= mod.Value;
			// 	}
			// 	else if (mod.Mod == StatModType.Set)
			// 	{
			// 		++setCount;
			// 		sumSetValue += mod.Value;
			// 		if (i + 1 >= modifiers.Count || modifiers[i + 1].Mod != StatModType.Set)
			// 		{
			// 			finalValue = sumSetValue / setCount;
			// 			setCount = 0;
			// 		}
			// 	}
			// }

			value = finalValue;
		}
	}
}
