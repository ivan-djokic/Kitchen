// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Utils
{
	public class RoundRobinHelper<T> : RoundRobin where T : Component
	{
		private readonly Transform m_parent;

		public RoundRobinHelper(Transform parent, int defaultIndex = 0)
			: base(parent.childCount)
		{
			m_parent = parent;
			base.Current = defaultIndex;
		}

		public new T Current
		{
			get => GetChild(base.Current) ?? Next;
		}

		public new T Next
		{
			get => GetChild(base.Next) ?? Next;
		}

		public new T Previous
		{
			get => GetChild(base.Previous) ?? Previous;
		}

		private T GetChild(int index)
		{
			if (m_parent.GetChild(index).TryGetComponent<T>(out var current) && current.gameObject.activeSelf)
			{
				return current;
			}

			return default;
		}
	}
}
