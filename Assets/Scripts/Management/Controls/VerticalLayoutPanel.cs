// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Commands;
using Kitchen.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Management.Controls
{
	public class VerticalLayoutPanel : VerticalLayoutGroup
	{
		private RoundRobinHelper<HorizontalLayoutPanel> m_children;
		[SerializeField]
		private int m_defaultIndex;

		public void SetActive(bool active)
		{
			transform.parent.gameObject.SetActive(active);
			m_children.Current.SetActive(active);
			GameInput.Instance.ControlUpdated -= ControlUpdated;

			if (!active)
			{
				return;
			}

			GameInput.Instance.ControlUpdated += ControlUpdated;
		}

		protected override void Awake()
		{
			m_children = new RoundRobinHelper<HorizontalLayoutPanel>(transform, m_defaultIndex);
		}

		private void ControlUpdated(Command control)
		{
			switch (control)
			{
				case Command.VerticalNext:
					ChangeSelectedChild(m_children.Current, m_children.Next);
					break;

				case Command.VerticalPrevious:
					ChangeSelectedChild(m_children.Current, m_children.Previous);
					break;
			}
		}

		private static void ChangeSelectedChild(HorizontalLayoutPanel previous, HorizontalLayoutPanel current)
		{
			if (previous == current)
			{
				return;
			}

			previous.SetActive(false);
			current.SetActive(true);
		}
	}
}
