// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Management.Controls
{
	public class Button : UnityObject
	{
		[SerializeField]
		private ButtonAction m_action;
		[SerializeField]
		private GameObject m_selectedState;

		public ButtonAction Action
		{
			get => m_action;
		}

		public SelectHelper Selected { get; private set; }

		protected override void Awake()
		{
			Selected = new SelectHelper(m_selectedState);
		}
	}
}
