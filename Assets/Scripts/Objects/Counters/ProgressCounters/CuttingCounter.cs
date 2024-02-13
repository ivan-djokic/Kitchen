// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Audio;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;

namespace Kitchen.Objects.Counters
{
	[Serializable]
	public class CuttingCounter : ProgressCounter<ProgressStepKitchenObject>
	{
		public Action OnCut { get; set; }

		protected override bool CanBeSelectedIfNotReady
		{
			get => !Player.Instance.HasKitchenObject;
		}

		protected override Sounds Sound
		{
			get => Sounds.Cut;
		}

		protected override void Interact()
		{
			if (!HasKitchenObject)
			{
				base.Interact();
			}

			if (KitchenObject.State == KitchenObjectState.Default)
			{
				// Cut slice right after putting kitchen object on counter
				CutSlice();
				return;
			}

			base.Interact();
		}

		private void CutSlice()
		{
			PlaySound();
			m_progress.NextStep(KitchenObject.ProgressStep);
			OnCut.Invoke();
		}
	}
}
