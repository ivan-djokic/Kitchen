// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Counters;
using UnityEngine;

namespace Kitchen.Animators
{
	public class ContainerCounterAnimator : BaseAnimator
	{
		private const string OPEN = "Open";

		[SerializeField]
		private ContainerCounter m_containerCounter;

		protected override void Start()
		{
			m_containerCounter.Changed += () => StartAnimation(OPEN);
		}
	}
}
