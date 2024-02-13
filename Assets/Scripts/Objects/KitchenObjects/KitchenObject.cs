// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Objects.KitchenObjects
{
	[CreateAssetMenu]
	public class KitchenObject : BaseKitchenObject
	{
		[SerializeField]
		private BaseKitchenObject m_nextStage;

		public KitchenObject Create(Transform parent)
		{
			var result = CreateInstance(GetType()) as KitchenObject;
			CopyTo(result);
			result.Instantiate();
			result.Parent = parent;

			return result;
		}

		public void NextStage()
		{
			Destroy();
			m_nextStage.CopyTo(this);
			Instantiate();
			ResetParent();
		}

		public override void CopyTo(BaseKitchenObject other)
		{
			base.CopyTo(other);

			if (other is KitchenObject kitchenObject)
			{
				kitchenObject.m_nextStage = m_nextStage;
			}
		}

		protected virtual void Instantiate()
		{
			m_visualObject = Instantiate(m_visualObject);
		}
	}
}
