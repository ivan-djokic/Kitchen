// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Management.Dispatchers;
using UnityEngine;

namespace Kitchen.Common
{
	public abstract class UnityObject : MonoBehaviour
	{
		private const float RESUME_CHECK_PERIOD = 1;

		private Action m_action;

		public void InvokeRepeating(float delay, Action action)
		{
			m_action = action;
			InvokeRepeating(nameof(ExecuteActionRepeating), delay, delay);
		}

		protected virtual void Awake()
		{
		}

		protected void Invoke(float delay, Action action)
		{
			if (IsInvoking(nameof(ExecuteAction)))
			{
				return;
			}

			m_action = action;
			Invoke(nameof(ExecuteAction), delay);
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
			if (Dispatcher.Instance.Paused)
			{
				return;
			}

			Updated();
		}

		protected virtual void Updated()
		{
		}

		private void ExecuteAction()
		{
			if (Dispatcher.Instance.Paused)
			{
				Invoke(nameof(ExecuteAction), RESUME_CHECK_PERIOD);
				return;
			}

			m_action.Invoke();
		}

		private void ExecuteActionRepeating()
		{
			if (Dispatcher.Instance.Paused)
			{
				return;
			}

			m_action.Invoke();
		}
	}
}
