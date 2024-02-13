// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Management.Administration;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;
using TMPro;
using UnityEngine;

namespace Kitchen.Objects.Counters
{
	public class ContainerCounter : BaseCounter
	{
		[SerializeField]
		private KitchenObject m_kitchenObjectTemplate;
		[SerializeField]
		private TextMeshProUGUI m_count;

		public override bool CanBeSelected
		{
			get
			{
				if (!Player.Instance.HasKitchenObject)
				{
					return KitchenObjectCount > 0;
				}

				return KitchenObject.Equals(Player.Instance.KitchenObject) && !IsFull;
			}
		}

		public Action Changed { get; set; }

		public bool IsFull
		{
			get => KitchenObjectCount > User.Instance.Level.MaxKitchenObjectCount - 1;
		}

		public int KitchenObjectCount
		{
			get => User.Instance.Level.Groceries.GetKitchenObjectCount(KitchenObject.Type);

			set
			{
				User.Instance.Level.Groceries.SetKitchenObjectCount(KitchenObject.Type, value);
				ShowKitchenObjectCount(value);
				Changed?.Invoke();
			}
		}

		protected override void Interact()
		{
			if (!Player.Instance.HasKitchenObject)
			{
				KitchenObjectCount--;
				Player.Instance.KitchenObject = KitchenObject.Create(Player.Instance.KitchenObjectLocation);
				return;
			}

			KitchenObjectCount++;
			Player.Instance.DestroyKitchenObject();
			PlaySound();
		}

		protected override void Awake()
		{
			// Initialize KitchenObject befor OnStart() to avoid KitchenObjectParent.PlaySound()
			KitchenObject = m_kitchenObjectTemplate;

			var surplus = KitchenObjectCount - (int) User.Instance.Level.MaxKitchenObjectCount;

			if (surplus <= 0)
			{
				ShowKitchenObjectCount(KitchenObjectCount);
				return;
			}

			// Sell surplus
			User.Instance.Level.MoneyBalance += surplus * KitchenObject.Price;
			KitchenObjectCount = (int) User.Instance.Level.MaxKitchenObjectCount;
		}

		private void ShowKitchenObjectCount(int count)
		{
			m_count.text = $"x{count}";
		}
	}
}
