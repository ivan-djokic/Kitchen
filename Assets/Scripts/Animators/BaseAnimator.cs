// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Common;
using Kitchen.Management.Dispatchers;
using UnityEngine;

namespace Kitchen.Animators
{
	public abstract class BaseAnimator : UnityObject
	{
		private const int LAYER_INDEX = 0;

		private Animator m_animator;
		private Action m_onAnimationFinished;
		private string m_finishState;

		private bool IsFinished
		{
			get => m_animator.GetCurrentAnimatorStateInfo(LAYER_INDEX).IsName(m_finishState);
		}

		protected override void Awake()
		{
			m_animator = GetComponent<Animator>();
		}

		protected void StartAnimation(string parameterName, bool? value = null, string finishState = null, Action onAnimationFinished = null)
		{
			if (Dispatcher.Instance.Paused)
			{
				return;
			}

			SetParameter(parameterName, value);
			m_finishState = finishState;
			m_onAnimationFinished = onAnimationFinished;
		}

		private void SetParameter(string parameterName, bool? value)
		{
			if (!value.HasValue)
			{
				m_animator.SetTrigger(parameterName);
				return;
			}

			m_animator.SetBool(parameterName, value.Value);
		}

		protected override void Updated()
		{
			if (m_onAnimationFinished == null || !IsFinished)
			{
				return;
			}

			var onAnimationFinished = m_onAnimationFinished;
			m_onAnimationFinished = null;
			onAnimationFinished.Invoke();
		}
	}
}
