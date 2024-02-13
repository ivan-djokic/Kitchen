// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Counters;
using UnityEngine;

namespace Kitchen.Animators
{
	public class DeliveryCounterAnimator : BaseAnimator
	{
		private const string FINISH_STATE = "Finish";
		private const string PUT = "Put";

		[SerializeField]
		private DeliveryCounter m_deliveryCounter;

		protected override void Start()
		{
			m_deliveryCounter.OnPut += (onAnimationFinished) => StartAnimation(PUT, null, FINISH_STATE, onAnimationFinished);
		}
	}
}
