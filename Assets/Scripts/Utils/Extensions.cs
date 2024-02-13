// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Utils
{
	public static class Extensions
	{
		public static int GetIndex<T>(this ICollection<T> collection, Func<T, bool> predicate)
		{
			var index = 0;

			foreach (var item in collection)
			{
				if (predicate.Invoke(item))
				{
					break;
				}

				index++;
			}

			return index;
		}

		public static T GetParent<T>(this Transform child)
		{
			if (child.transform.parent.TryGetComponent<T>(out var parent))
			{
				return parent;
			}

			return GetParent<T>(child.transform.parent);
		}

		public static float GetWidth(this Image image)
		{
			return image.GetComponent<RectTransform>().rect.width;
		}

		public static bool IsBetween(this float input, float start, float end)
		{
			return input >= start && input < end;
		}

		public static bool IsFinished(this AnimatorStateInfo stateInfo)
		{
			return stateInfo.normalizedTime >= 1;
		}

		public static string Name(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		public static void Normalize(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = new Quaternion(0, 0, 0, 1);
			transform.localScale = Vector3.one;
		}

		public static IList<TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			return dictionary.OrderBy(item => item.Key).Select(item => item.Value).ToList();
		}

		public static bool TryGetSingle<T>(this ICollection<T> list, out T item)
		{
			try
			{
				item = list.Single();
				return true;
			}
			catch
			{
				item = default;
				return false;
			}
		}
	}
}
