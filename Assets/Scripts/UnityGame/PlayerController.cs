using System;
using Service;
using UnityEngine;
using UnityEngine.UI;

namespace UnityGame
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private Button attackButton;

		[SerializeField]
		private Button skillButton;

		[SerializeField]
		private Button ultButton;

		private void Start()
		{
			if (ServiceManager.TryGetService(out IMessageService messageService))
			{
			}
		}

		public void AttackButton_OnClicked()
		{

		}

		public void SkillButton_OnClicked()
		{

		}

		public void UltButton_OnClicked()
		{

		}
	}
}