// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Objects.Characters;

namespace Kitchen.Animators
{
	public class WaiterAnimator : BaseCharacterAnimator<Waiter>
	{
		protected override Waiter Character
		{
			get => Waiter.Instance;
		}

		protected override void Quack()
		{
			base.Quack();
			SoundManager.Instance.Play(Sounds.QuackWaiter, Waiter.Instance.transform);
		}

		protected override void Start()
		{
			base.Start();
			Waiter.Instance.ReadyToTakeOrder += Quack;
		}
	}
}
