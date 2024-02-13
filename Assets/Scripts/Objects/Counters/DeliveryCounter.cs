// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Audio;
using Kitchen.Management.Dispatchers;
using Kitchen.Objects.Characters;

namespace Kitchen.Objects.Counters
{
	public class DeliveryCounter : BaseCounter
	{
		public override bool CanBeSelected
		{
			get => !HasKitchenObject && Game.DeliveryManager.Contains(Player.Instance.KitchenObject);
		}

		public Action<Action> OnPut { get; set; }

		public override CounterPriority Priority
		{
			get => CounterPriority.High;
		}

		protected override Sounds Sound
		{
			get => Sounds.DeliverySuccess;
		}

		protected override void Interact()
		{
			base.Interact();
			Game.DeliveryManager.ProcessOrder(KitchenObject);

			OnPut?.Invoke(() => 
			{
				KitchenObject.NextStage();
				Waiter.Instance.PickDelivery(this);
			});
		}
	}
}
