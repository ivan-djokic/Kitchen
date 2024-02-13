// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Dispatchers;

namespace Kitchen.Animators
{
	public class MoneyAnimator : BaseAnimator
	{
		private const string INCREASE = "Increase";

		protected override void Start()
		{
			Game.Manager.Money.Changed += () => StartAnimation(INCREASE);
		}
	}
}
