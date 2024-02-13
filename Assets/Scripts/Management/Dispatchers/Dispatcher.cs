// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using Kitchen.Common;
using Kitchen.Management.Commands;
using Kitchen.Management.Controls;
using UnityEngine;

namespace Kitchen.Management.Dispatchers
{
	public abstract class Dispatcher : UnityObject
	{
		protected readonly Stack<VerticalLayoutPanel> m_controls = new();

		public static Dispatcher Instance
		{
			get => Game.Instance as Dispatcher ?? MainMenu.Instance as Dispatcher ?? Users.Instance;
		}

		public bool Paused
		{
			get => Time.timeScale == 0;
			protected set => Time.timeScale = value ? 0 : 1;
		}

		public bool Starting { get; set; } = true;

		public virtual void ProcessButton(Button button)
		{
		}

		protected override void Awake()
		{
			GameInput.Instance.ControlUpdated += ControlUpdated;
		}

		protected virtual void ActivateControl(VerticalLayoutPanel control)
		{
			if (m_controls.TryPeek(out var previous))
			{
				previous.SetActive(false);
			}

			control.SetActive(true);
			m_controls.Push(control);
		}

		protected abstract void ControlUpdated(Command control);

		protected virtual void DeactivateControl()
		{
			var control = m_controls.Pop();
			control.SetActive(false);

			if (!m_controls.TryPop(out control))
			{
				return;
			}

			ActivateControl(control);
		}

		protected abstract void OnDestroy();
	}
}
