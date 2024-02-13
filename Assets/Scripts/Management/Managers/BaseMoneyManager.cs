// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using Kitchen.Management.Administration;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class BaseMoneyManager : UnityObject
	{
		[SerializeField]
		private TextMeshProUGUI m_balance;

		public virtual int Balance
		{
			get => User.Instance.Level.MoneyBalance;

			set
			{
				User.Instance.Level.MoneyBalance = value;
				m_balance.text = $"{value}$";
			}
		}

		protected override void OnEnable()
		{
			Balance = User.Instance.Level.MoneyBalance;
		}
	}
}
