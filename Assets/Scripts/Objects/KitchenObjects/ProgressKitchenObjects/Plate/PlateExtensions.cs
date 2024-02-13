// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Kitchen.Objects.KitchenObjects
{
	public static class PlateExtensions
	{
		public static bool AreEqualWith(this ICollection<BaseKitchenObject> ingredients, ICollection<BaseKitchenObject> other)
		{
			return ingredients.Count == other.Count && ingredients.Contains(other);
		}

		public static bool CanPlace(this ICollection<BaseKitchenObject> ingredients, BaseKitchenObject kitchenObject)
		{
			if (kitchenObject == null)
			{
				return false;
			}

			if (kitchenObject is Plate plate)
			{
				return !ingredients.Contains(plate.Ingredients);
			}

			return kitchenObject.State != KitchenObjectState.Default && !Contains(ingredients, kitchenObject);
		}

		public static bool Contains(this ICollection<BaseKitchenObject> ingredients, ICollection<BaseKitchenObject> other)
		{
			return other.All(item => Contains(ingredients, item));
		}

		private static bool Contains(ICollection<BaseKitchenObject> ingredients, BaseKitchenObject kitchenObject)
		{
			return ingredients.Any(item => kitchenObject.Equals(item));
		}
	}
}
