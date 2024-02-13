// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using Kitchen.Management.Managers;
using Kitchen.Objects.Counters;
using Kitchen.Objects.ProgressBar;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.MenuItems
{
	public class MarketItem : UnityObject
	{
		[SerializeField]
		private TextMeshProUGUI m_total;
		[SerializeField]
		private ContainerCounter m_counter;
		[SerializeField]
		private BaseMoneyManager m_money;
		[SerializeField]
		private TextMeshProUGUI m_price;
		[SerializeField]
		private CursorProgress m_progress;

		public void BuyKitchenObject()
		{
			if (m_money.Balance < m_counter.KitchenObject.Price || m_counter.IsFull)
			{
				SoundManager.Instance.Play(Sounds.NoMoney, inPause: true);
				return;
			}

			m_counter.KitchenObjectCount++;
			m_money.Balance -= m_counter.KitchenObject.Price;
			Game.Manager.Money.Balance = m_money.Balance;
			SoundManager.Instance.Play(Sounds.Money, inPause: true);
		}

		protected override void OnEnable()
		{
			m_progress.SetProgress(m_counter.KitchenObjectCount / User.Instance.Level.MaxKitchenObjectCount);
		}

		protected override void Start()
		{
			m_price.text = $"{m_counter.KitchenObject.Price}$";
			m_counter.Changed += OnEnable;
		}
	}
}
