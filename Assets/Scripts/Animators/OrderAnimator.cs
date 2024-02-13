// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Orders;
using UnityEngine;

namespace Kitchen.Animators
{
	public class OrderAnimator : BaseAnimator
	{
		private const string DOUBLED = "Doubled";
		private const string FLY_OUT = "FlyOut";

		[SerializeField]
		private OrderTemplate m_order;

		protected override void Start()
		{
			m_order.Destroying += (doubled, onAnimationFinished) => StartAnimation(doubled ? DOUBLED : FLY_OUT, null, FLY_OUT, onAnimationFinished);
		}
	}
}
