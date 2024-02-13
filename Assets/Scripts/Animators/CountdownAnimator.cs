// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Managers;
using UnityEngine;

namespace Kitchen.Animators
{
	public class CountdownAnimator : BaseAnimator
	{
		private const string EXPAND = "Expand";
		private const string FINISH_STATE = "Finish";

		[SerializeField]
		private CountdownManager m_countdownManager;

		protected override void Start()
		{
			m_countdownManager.Changed += (onAnimationFinished) => StartAnimation(EXPAND, null, FINISH_STATE, onAnimationFinished);
		}
	}
}
