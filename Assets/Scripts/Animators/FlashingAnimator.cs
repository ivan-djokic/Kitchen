// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.ProgressBar;
using UnityEngine;

namespace Kitchen.Animators
{
	public class FlashingAnimator : BaseAnimator
	{
		private const string SHOW = "Show";

		[SerializeField]
		private Progress m_progress;

		protected override void Start()
		{
			m_progress.Flush += (show) => StartAnimation(SHOW, show);
		}
	}
}
