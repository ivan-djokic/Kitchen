// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Orders
{
	public class OrdersGenerator : ScriptableObject
	{
		[SerializeField]
		private Order[] m_availableOrders;
		[SerializeField]
		private OrderTemplate m_orderTemplate;

		public OrderTemplate NextOrder(Transform parent)
		{
			var selector = Randomize.Next;
			var frequency = 0f;

			foreach (var order in m_availableOrders)
			{
				// If selector is between 0.41 & 0.74 => 0.74 - 0.41 = 0.33 = 33%, that means every third order should be type of mentioned order
				if (!selector.IsBetween(frequency, frequency + order.Frequecy))
				{
					frequency += order.Frequecy;
					continue;
				}

				order.Reinitialize();
				return m_orderTemplate.Create(order, parent);
			}

			return null;
		}
	}
}
