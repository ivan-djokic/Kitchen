// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using Kitchen.Objects.ProgressBar;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class MoneyManager : BaseMoneyManager
	{
		[SerializeField]
		private TextMeshProUGUI m_earned;
		[SerializeField]
		private TextMeshProUGUI m_limit;
		[SerializeField]
		private IncreasingProgress m_progress;

		public override int Balance
		{
			get => base.Balance;

			set
			{
				if (value > base.Balance)
				{
					m_earned.text = $"{value - base.Balance}$";
					Changed?.Invoke();
				}

				m_progress.Increase(value / (float) User.Instance.Level.MoneyLimit);
				base.Balance = value;
			}
		}

		public Action Changed { get; set; }

		protected override void Start()
		{
			m_limit.text = $"{User.Instance.Level.MoneyLimit}$";
			m_progress.Finished += Game.Instance.LevelUp;
		}
	}
}
