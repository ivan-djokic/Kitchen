// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Characters;

namespace Kitchen.Animators
{
	public abstract class BaseCharacterAnimator<T> : BaseAnimator where T : BaseCharacter<T>
	{
		private const string QUACK = "Quack";
		private const string OPEN_MOUTH = "OpenMouth";

		protected abstract T Character { get; }

		protected virtual void Quack()
		{
			StartAnimation(QUACK);
		}

		protected override void Start()
		{
			Character.KitchenObjectChanged += OpenMouth;
		}

		private void OpenMouth()
		{
			StartAnimation(OPEN_MOUTH, Character.HasKitchenObject);
		}
	}
}
