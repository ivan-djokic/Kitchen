// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Commands;
using Kitchen.Management.Dispatchers;
using Kitchen.Utils;
using UnityEngine.UI;

namespace Kitchen.Management.Controls
{
	public class HorizontalLayoutPanel : HorizontalLayoutGroup
	{
		private RoundRobinHelper<Button> m_buttons;

		public void SetActive(bool active)
		{
			m_buttons.Current.Selected.Value = active;
			GameInput.Instance.ControlUpdated -= ControlUpdated;

			if (!active)
			{
				return;
			}

			GameInput.Instance.ControlUpdated += ControlUpdated;
		}

		protected override void Awake()
		{
			m_buttons = new RoundRobinHelper<Button>(transform);
		}

		private void ControlUpdated(Command control)
		{
			switch (control)
			{
				case Command.HorizontalNext:
					ChangeSelectedButton(m_buttons.Current, m_buttons.Next);
					break;

				case Command.HorizontalPrevious:
					ChangeSelectedButton(m_buttons.Current, m_buttons.Previous);
					break;

				case Command.Select:
					Dispatcher.Instance.ProcessButton(m_buttons.Current);
					break;
			}
		}

		private static void ChangeSelectedButton(Button previous, Button current)
		{
			if (previous == current)
			{
				return;
			}

			previous.Selected.Value = false;
			current.Selected.Value = true;
		}
	}
}
