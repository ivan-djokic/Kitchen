// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Collections.Generic;

namespace Kitchen.Utils
{
	public class PriorityAction
	{
		private readonly Dictionary<int, Action> m_actions = new();

		public void Invoke()
		{
			foreach (var action in m_actions.ToSortedList())
			{
				action.Invoke();
			}

			m_actions.Clear();
		}

		public void Subscribe(int priority, Action action)
		{
			m_actions[priority] = action;
		}
	}
}
