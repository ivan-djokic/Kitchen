// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Concurrent;
using Kitchen.Objects.Counters;
using Kitchen.Utils;

namespace Kitchen.Objects.KitchenObjects
{
	public class PlatesStack : ConcurrentStack<Plate>
	{
		private const int MAX_PLATES_COUNT = 5;
		private const float VERTICAL_OFFSET = 0.1f;

		private readonly BaseCounter m_counter;

		public PlatesStack(BaseCounter counter)
		{
			m_counter = counter;
		}

		public bool CanAdd
		{
			get => Count < MAX_PLATES_COUNT;
		}

		public Plate Pop()
		{
			TryPop(out var plate);
			return plate;
		}

		public void Push(Plate plate, bool onTop)
		{
			if (onTop)
			{
				Push(plate);
				return;
			}

			var topPlate = Pop();
			Push(plate);
			Push(topPlate);
		}

		private new void Push(Plate plate)
		{
			base.Push(plate);

			SetKitchenObject();
			plate.SetVerticalOffset(Count * VERTICAL_OFFSET);
			plate.SetVerticalRotation(Randomize.NextAngle);
		}

		private void SetKitchenObject()
		{
			TryPeek(out var plate);
			m_counter.KitchenObject = plate;
		}

		private new Plate TryPop(out Plate plate)
		{
			if (base.TryPop(out plate))
			{
				SetKitchenObject();
			}

			return plate;
		}
	}
}
