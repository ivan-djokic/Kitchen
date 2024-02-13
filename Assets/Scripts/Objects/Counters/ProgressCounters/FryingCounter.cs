// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Counters
{
	public class FryingCounter : ProgressCounter<PeriodKitchenObject>
	{
		[SerializeField]
		private GameObject m_sizzlingParticles;
		[SerializeField]
		private GameObject m_stove;

		protected override bool CanBeSelectedIfEmpty
		{
			get
			{
				if (Player.Instance.KitchenObject is Plate plate)
				{
					if (!plate.Ingredients.TryGetSingle(out var potato))
					{
						return false;
					}

					return potato.State == KitchenObjectState.DefaultCutted;
				}

				return base.CanBeSelectedIfEmpty;
			}
		}

		protected override Sounds Sound
		{
			get => Sounds.StoveSizzle;
		}

		protected override void Interact()
		{
			InteractInternal();

			if (HasKitchenObject)
			{
				// Default => Ready
				Begin(m_stove);
				return;
			}

			Finish();
		}

		protected override void ProgressFinished()
		{
			base.ProgressFinished();

			if (KitchenObject.State == KitchenObjectState.Ready)
			{
				// Ready => Unusable
				Begin(m_sizzlingParticles);
				return;
			}

			Finish();
		}

		private void Begin(GameObject gameObject)
		{
			m_progress.Begin(KitchenObject.Period);
			gameObject.SetActive(true);
		}

		private void Finish()
		{
			SoundManager.Instance.Stop(Sounds.StoveSizzle);
			m_progress.Finish();
			m_stove.SetActive(false);
			m_sizzlingParticles.SetActive(false);
		}

		private void InteractInternal()
		{
			if (Player.Instance.KitchenObject is Plate plate 
				&& plate.Ingredients.TryGetSingle(out var potato) && potato.State == KitchenObjectState.DefaultCutted)
			{
				KitchenObject = potato as PeriodKitchenObject;
				KitchenObject.State = KitchenObjectState.Default;

				plate.Remove(potato);
				return;
			}

			base.Interact();
		}
	}
}
