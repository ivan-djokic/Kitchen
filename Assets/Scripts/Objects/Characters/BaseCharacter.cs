// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Objects.Common;
using Kitchen.Utils;

namespace Kitchen.Objects.Characters
{
    public abstract class BaseCharacter<T> : KitchenObjectParent where T : BaseCharacter<T>
    {
		protected MovementHelper m_movementHelper;

		public static T Instance { get; private set; }

		protected abstract float MovementSpeed { get; }

		protected override Sounds Sound
		{
			get => Sounds.ObjectPickup;
		}

		protected override void Awake()
		{
			Instance = this as T;
			m_movementHelper = new MovementHelper(transform, MovementSpeed);
		}
	}
}
