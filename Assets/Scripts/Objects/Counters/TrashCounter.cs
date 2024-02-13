// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Linq;
using Kitchen.Audio;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;

namespace Kitchen.Objects.Counters
{
	public class TrashCounter : BaseCounter
	{
		public override bool CanBeSelected
		{
			get
			{
				if (Player.Instance.KitchenObject is Plate plate)
				{
					return plate.Ingredients.Any();
				}

				return Player.Instance.HasKitchenObject;
			}
		}

		protected override Sounds Sound
		{
			get => Sounds.Trash;
		}

		protected override void Interact()
		{
			SoundManager.Instance.Play(Sound, transform);

			if (Player.Instance.KitchenObject is Plate plate)
			{
				plate.Clear();
				return;
			}

			Player.Instance.DestroyKitchenObject();
		}
	}
}
