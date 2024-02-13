// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Audio;
using Kitchen.Management.Dispatchers;
using Kitchen.Management.Commands;
using Kitchen.Objects.Characters;
using Kitchen.Objects.Common;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Counters
{
	public class BaseCounter : KitchenObjectParent
	{
		private SelectHelper m_selected;
		[SerializeField]
		private GameObject m_selectedState;

		public virtual bool CanBeSelected
		{
			get => HasKitchenObject || Player.Instance.HasKitchenObject;
		}

		public virtual CounterPriority Priority
		{
			get => HasKitchenObject ? CounterPriority.Medium : CounterPriority.Low;
		}

		public bool Selected
		{
			get => m_selected.Value;
			set => m_selected.Value = value;
		}

		protected override Sounds Sound
		{
			get => Sounds.ObjectDrop;
		}

		protected bool CanCombineWith(KitchenObject kitchenObject)
		{
			return kitchenObject is Plate plate && plate.CanPlace(KitchenObject);
		}

		protected virtual void Interact()
		{
			if (TryCombineWith(Player.Instance.KitchenObject))
			{
				return;
			}

			SwapKitchenObjectWith(Player.Instance);
		}

		protected virtual void InteractEnded()
		{
		}

		protected override void Start()
		{
			base.Start();

			m_selected = new SelectHelper(m_selectedState);
			m_selected.Changed += SelectedChanged;
		}

		private bool CanCombineWith(ICollection<BaseKitchenObject> ingredients)
		{
			if (KitchenObject is Plate plate)
			{
				return Game.DeliveryManager.ContainsPartially(new List<BaseKitchenObject>(ingredients.Concat(plate.Ingredients)));
			}

			return true;
		}

		private void InteractUpdated(bool ended)
		{
			if (!ended)
			{
				Interact();
				return;
			}

			InteractEnded();
		}

		private void SelectedChanged(bool selected)
		{
			if (selected)
			{
				GameInput.Instance.InteractUpdated += InteractUpdated;
				return;
			}

			GameInput.Instance.InteractUpdated -= InteractUpdated;
			InteractEnded();
		}

		private bool TryCombineWith(KitchenObject kitchenObject)
		{
			if (!CanCombineWith(kitchenObject))
			{
				return false;
			}

			var plate = kitchenObject as Plate;

			if (!CanCombineWith(plate.Ingredients))
			{
				return false;
			}

			KitchenObject = plate.Place(KitchenObject);
			Player.Instance.KitchenObjectChanged.Invoke();
			return true;
		}
	}
}
