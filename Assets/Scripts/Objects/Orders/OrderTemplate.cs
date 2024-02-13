// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Kitchen.Management.Administration;
using Kitchen.Objects.Common;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Objects.ProgressBar;
using TMPro;
using UnityEngine;

namespace Kitchen.Objects.Orders
{
	public class OrderTemplate : TemplateFactory<Order>
	{
		[SerializeField]
		private Transform m_ingredients;
		[SerializeField]
		private IconTemplate m_orderIconTemplate;
		[SerializeField]
		private TextMeshProUGUI m_price;
		[SerializeField]
		private Progress m_progress;
		[SerializeField]
		private TextMeshProUGUI m_subtitle;
		[SerializeField]
		private TextMeshProUGUI m_title;

		public Action<bool, Action> Destroying { get; set; }

		public Action<OrderTemplate, bool> Destroyed { get; set; }

		public ICollection<BaseKitchenObject> Ingredients { get; private set; }

		public int Price { get; private set; }

		public OrderTemplate Create(Order source, Transform parent)
		{
			return Generate(source, parent) as OrderTemplate;
		}

		public void Destroy(bool doubled, bool timeOut = false)
		{
			Destroying.Invoke(doubled, () =>
			{
				Destroy();
				Destroyed?.Invoke(this, timeOut);
			});
		}

		protected override void Initialize(Order source)
		{
			Ingredients = new List<BaseKitchenObject>(source.Ingredients.Base.Concat(source.Ingredients.SideDishes));
			Price = source.Price;

			m_price.text = $"{Price}$";
			m_subtitle.text = string.Join(", ", source.Ingredients.SideDishes);
			m_title.text = source.Title;

			foreach (var item in Ingredients)
			{
				m_orderIconTemplate.Create(item.Icon, m_ingredients);
			}

			var lifeTime = (source.LifeTime + Ingredients.Sum(item => item.LifeTime)) * User.Instance.Level.OrderLifeTimeFactor;
			m_progress.Begin(lifeTime);
			m_progress.Finished += () => Destroy(false, true);
		}
	}
}
