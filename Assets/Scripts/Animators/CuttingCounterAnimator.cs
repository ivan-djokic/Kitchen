// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Counters;
using UnityEngine;

namespace Kitchen.Animators
{
	public class CuttingCounterAnimator : BaseAnimator
	{
		private const string CUT = "Cut";

		[SerializeField]
		private CuttingCounter m_cuttingCounter;

		protected override void Start()
		{
			m_cuttingCounter.OnCut += () => StartAnimation(CUT);
		}
	}
}
