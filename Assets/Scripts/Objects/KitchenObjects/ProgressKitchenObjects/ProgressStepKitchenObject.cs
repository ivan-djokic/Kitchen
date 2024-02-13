// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Objects.KitchenObjects
{
	[CreateAssetMenu]
	public class ProgressStepKitchenObject : PeriodKitchenObject
	{
		[SerializeField]
		private int m_slicesCount;

		public float ProgressStep
		{
			get => 1 / (float) m_slicesCount;
		}

		public override void CopyTo(BaseKitchenObject other)
		{
			base.CopyTo(other);

			if (other is ProgressStepKitchenObject progressStepKitchenObject)
			{
				progressStepKitchenObject.m_slicesCount = m_slicesCount;
			}
		}
	}
}
