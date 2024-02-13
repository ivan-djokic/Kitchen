// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Objects.Common;
using UnityEngine;

namespace Kitchen.Objects.KitchenObjects
{
	[CreateAssetMenu]
	public class Plate : PeriodKitchenObject
	{
		private const int INGREDIENTS_PARENT = 1;
		private const int INGREDIENTS_ICONS_PARENT = 0;

		private readonly Dictionary<BaseKitchenObject, IconTemplate> m_ingredients = new();
		private Transform m_ingredientsParent;
		private Transform m_ingredientsIconParent;
		[SerializeField]
		private IconTemplate m_kitchenObjectIconTemplate;

		public ICollection<BaseKitchenObject> Ingredients
		{
			get => m_ingredients.Keys;
		}

		public int IngredientsCount
		{
			get => m_ingredients.Count;
		}

		public bool CanPlace(BaseKitchenObject kitchenObject)
		{
			return Ingredients.CanPlace(kitchenObject);
		}

		public void Clear()
		{
			while (m_ingredients.Any())
			{
				Remove(m_ingredients.First().Key, true);
			}
		}

		public override void CopyTo(BaseKitchenObject other)
		{
			base.CopyTo(other);

			if (other is Plate plate)
			{
				plate.m_kitchenObjectIconTemplate = m_kitchenObjectIconTemplate;
			}
		}

		public KitchenObject Place(BaseKitchenObject kitchenObject, IconTemplate icon = null)
		{
			if (kitchenObject is Plate plate)
			{
				CombineWith(plate);
				return plate;
			}

			kitchenObject.Parent = m_ingredientsParent;
			m_ingredients[kitchenObject] = icon ?? m_kitchenObjectIconTemplate.Create(kitchenObject.Icon, m_ingredientsIconParent);
			kitchenObject.SetVerticalOffset();
			m_price += kitchenObject.Price;

			return null;
		}

		public void Remove(BaseKitchenObject kitchenObject, bool destoryKitchenObject = false)
		{
			m_ingredients[kitchenObject].Destroy();
			m_ingredients.Remove(kitchenObject);
			m_price -= kitchenObject.Price;

			if (!destoryKitchenObject)
			{
				return;
			}

			kitchenObject.Destroy();
		}

		protected override void Instantiate()
		{
			base.Instantiate();

			if (m_visualObject.transform.childCount <= INGREDIENTS_PARENT)
			{
				return;
			}

			m_ingredientsParent = m_visualObject.transform.GetChild(INGREDIENTS_PARENT);
			m_ingredientsIconParent = m_visualObject.transform.GetChild(INGREDIENTS_ICONS_PARENT);
		}

		private void CombineWith(Plate other)
		{
			foreach (var item in new Dictionary<BaseKitchenObject, IconTemplate>(other.m_ingredients))
			{
				if (!CanPlace(item.Key))
				{
					continue;
				}

				Place(item.Key, item.Value);
				item.Value.SetParent(m_ingredientsIconParent);

				other.m_ingredients.Remove(item.Key);
			}
		}
	}
}
