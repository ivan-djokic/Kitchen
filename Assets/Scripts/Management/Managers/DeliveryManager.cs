// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Objects.Orders;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class DeliveryManager : UnityObject, ISpawnItem
	{
		private const int DOUBLE = 2;
		private const int MAX_ORDERS_COUNT = 5;

		[SerializeField]
		private OrdersGenerator m_generator;
		private readonly List<OrderTemplate> m_waitingOrders = new();

		public float SpawnPeriod
		{
			get => User.Instance.Level.OrderSpawnTime;
		}

		private bool CanSpawn
		{
			get => m_waitingOrders.Count < MAX_ORDERS_COUNT;
		}

		public bool Contains(KitchenObject kitchenObject)
		{
			return FindOrder(kitchenObject) != null;
		}

		public bool ContainsPartially(ICollection<BaseKitchenObject> ingredients)
		{
			return m_waitingOrders.Any(order => order.Ingredients.Contains(ingredients));
		}

		public void ProcessOrder(KitchenObject kitchenObject)
		{
			var doubled = kitchenObject is GoldenPlate;
			var order = FindOrder(kitchenObject);
			order.Destroy(doubled);

			if (doubled)
			{
				Game.Manager.Money.Balance += order.Price * DOUBLE;
				return;
			}

			Game.Manager.Money.Balance += order.Price;
		}

		public void Spawn()
		{
			if (!CanSpawn)
			{
				return;
			}

			Waiter.Instance.TakeOrder();
		}

		protected override void Start()
		{
			Waiter.Instance.ReadyToTakeOrder += SpawnOrder;
		}

		private OrderTemplate FindOrder(KitchenObject kitchenObject)
		{
			if (kitchenObject is Plate plate)
			{
				// Backward check because list is sorted by price and then by remaining life time
				return m_waitingOrders.LastOrDefault(order => order.Ingredients.Count == plate.Ingredients.Count && order.Ingredients.Contains(plate.Ingredients));
			}

			return null;
		}

		private void OrderDestroyed(OrderTemplate order, bool timeOut)
		{
			m_waitingOrders.Remove(order);

			if (!timeOut)
			{
				return;
			}

			Game.Manager.Lives.Decrease();
		}

		private void SpawnOrder()
		{
			var order = m_generator.NextOrder(transform);
			var index = m_waitingOrders.GetIndex(item => item.Price <= order.Price);

			m_waitingOrders.Insert(index, order);
			order.Destroyed += OrderDestroyed;
			order.transform.SetSiblingIndex(index);
		}
	}
}
