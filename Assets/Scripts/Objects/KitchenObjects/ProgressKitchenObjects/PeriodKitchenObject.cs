// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Objects.KitchenObjects
{
	[CreateAssetMenu]
	public class PeriodKitchenObject : KitchenObject
	{
		[SerializeField]
		private int m_period;

		public float Period
		{
			get => m_period;
		}

		public override void CopyTo(BaseKitchenObject other)
		{
			base.CopyTo(other);

			if (other is PeriodKitchenObject periodKitchenObject)
			{
				periodKitchenObject.m_period = m_period;
			}
		}
	}
}
