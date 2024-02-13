// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using UnityEngine;

namespace Kitchen.Utils
{
	public class SelectHelper
	{
		private readonly GameObject m_selected;

		public SelectHelper(GameObject selected)
		{
			m_selected = selected;
		}

		public Action<bool> Changed { get; set; }

		public bool Value
		{
			get => m_selected.activeSelf;
			set => SetActive(value);
		}

		private void SetActive(bool active)
		{
			if (active == m_selected.activeSelf)
			{
				return;
			}

			m_selected.SetActive(active);
			Changed?.Invoke(active);
		}
	}
}
