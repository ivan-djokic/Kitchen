// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Kitchen.Objects.Orders
{
	[CreateAssetMenu]
	public class Order : ScriptableObject
	{
		[SerializeField]
		private BaseKitchenObject[] m_baseIngredients;
		[SerializeField]
		private float m_frequecy;
		[SerializeField]
		private float m_lifeTime;
		[SerializeField]
		private int m_minSideDishesCount = 0;
		[SerializeField]
		private int m_profit;
		[SerializeField]
		private BaseKitchenObject[] m_sideDishes;
		[SerializeField]
		private OrderType m_type;

		public float Frequecy
		{
			get => m_frequecy;
		}

		public (ICollection<BaseKitchenObject> Base, ICollection<BaseKitchenObject> SideDishes) Ingredients { get; private set; }

		public float LifeTime
		{
			get => m_lifeTime;
		}

		public int Price
		{
			get => Ingredients.Base.Sum(item => item.Price) + Ingredients.SideDishes.Sum(item => item.Price) + m_profit;
		}

		public string Title
		{
			get => m_type.Name();
		}

		public void Reinitialize()
		{
			Ingredients = (m_baseIngredients.AsReadOnlyCollection(), GetSideDishes().AsReadOnlyCollection());
		}

		private ICollection<BaseKitchenObject> GetSideDishes()
		{
			var sideDishes = new List<BaseKitchenObject>();

			if (m_sideDishes.Length == 0)
			{
				return sideDishes;
			}

			var sideDishesCount = Randomize.NextRange(m_minSideDishesCount, m_sideDishes.Length);

			if (sideDishesCount == m_sideDishes.Length)
			{
				return m_sideDishes;
			}

			for (var i = 0; i < sideDishesCount; i++)
			{
				sideDishes.Add(GetUniqueItem(sideDishes));
			}

			return sideDishes;
		}

		private BaseKitchenObject GetUniqueItem(List<BaseKitchenObject> source)
		{
			var item = Randomize.NextItem(m_sideDishes);

			while (source.Contains(item))
			{
				item = Randomize.NextItem(m_sideDishes);
			}

			return item;
		}
	}
}
