// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Objects.ProgressBar;
using UnityEngine;

namespace Kitchen.Objects.Counters
{
	public abstract class ProgressCounter<T> : BaseCounter where T : KitchenObject
	{
		[SerializeField]
		protected Progress m_progress;

		public override bool CanBeSelected
		{
			get
			{
				if (!HasKitchenObject)
				{
					return CanBeSelectedIfEmpty;
				}

				if (KitchenObject.State == KitchenObjectState.Default)
				{
					return CanBeSelectedIfNotReady;
				}

				return CanCombineWith(Player.Instance.KitchenObject);
			}
		}

		public new T KitchenObject
		{
			get => base.KitchenObject as T;
			set => base.KitchenObject = value;
		}

		public override CounterPriority Priority
		{
			get => CounterPriority.High;
		}

		protected virtual bool CanBeSelectedIfEmpty
		{
			get => Player.Instance.KitchenObject?.GetType() == typeof(T) && Player.Instance.KitchenObject.State == KitchenObjectState.Default;
		}

		protected virtual bool CanBeSelectedIfNotReady
		{
			get => false;
		}

		protected virtual void ProgressFinished()
    	{
			KitchenObject.NextStage();
		}

		protected override void Start()
		{
			base.Start();
			m_progress.Finished += ProgressFinished;
		}
	}
}
