// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Management.Commands;
using Kitchen.Objects.Characters;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Animators
{
	public class PlayerAnimator : BaseCharacterAnimator<Player>
	{
		private const string IS_MOVING = "IsMoving";
		private const float MAX_PERIOD_BETWEEN_QUACKING = 10;
		private const float MIN_PERIOD_BETWEEN_QUACKING = 5;

		private bool m_isMoving;

		protected override Player Character
		{
			get => Player.Instance;
		}

		protected override void Quack()
		{
			if (m_isMoving || Player.Instance.HasKitchenObject)
			{
				return;
			}

			SoundManager.Instance.Play(Sounds.QuackPlayer, Player.Instance.transform);
			base.Quack();
		}

		protected override void Start()
		{
			base.Start();
			GameInput.Instance.MovementUpdated += MovementUpdated;
		}

		private void MovementUpdated(Vector3 movementVector)
		{
			m_isMoving = movementVector != Vector3.zero;
			StartAnimation(IS_MOVING, m_isMoving);

			if (m_isMoving)
			{
				SoundManager.Instance.Play(Sounds.Footstep, Player.Instance.transform);
			}

			Invoke(Randomize.NextRange(MIN_PERIOD_BETWEEN_QUACKING, MAX_PERIOD_BETWEEN_QUACKING), Quack);
		}
	}
}
