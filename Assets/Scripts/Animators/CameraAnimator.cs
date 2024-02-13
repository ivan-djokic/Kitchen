// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Dispatchers;

namespace Kitchen.Animators
{
	public class CameraAnimator : BaseAnimator
	{
		private const string DRIFT_OUT = "DriftOut";
		private const string FINISH_STATE = "Finish";

		protected override void Start()
		{
			MainMenu.Instance.GameStarting += (onAnimationFinished) => StartAnimation(DRIFT_OUT, null, FINISH_STATE, onAnimationFinished);
		}
	}
}
