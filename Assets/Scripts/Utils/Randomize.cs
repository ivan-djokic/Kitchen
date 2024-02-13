// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Kitchen.Utils
{
	public static class Randomize
	{
		private const float MAX_ANGLE = 360;

		public static float NextAngle
		{
			get => Next * MAX_ANGLE;
		}

		public static float Next
		{
			get => Random.value;
		}

		public static T NextItem<T>(IList<T> list)
		{
			return list[NextRange(0, list.Count - 1)];
		}

		public static int NextRange(int min, int max)
		{
			return Random.Range(min, max + 1);
		}

		public static float NextRange(float min, float max)
		{
			return Random.Range(min, max + 1);
		}
	}
}
