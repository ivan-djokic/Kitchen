// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Common;
using Kitchen.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Objects.ProgressBar
{
	public class Progress : UnityObject
	{
		private const float FINISHED = 0.9999f;

		private RoundRobin m_currentFlushLimit;
		private float m_currentStep;
		[SerializeField]
		private float[] m_flushLimits;
		protected bool m_isPaused = true;
		[SerializeField]
		protected Image m_progress;

		public float Current
		{
			get => m_progress.fillAmount;
			protected set => m_progress.fillAmount = value;
		}

		public Action Finished { get; set; }

		public Action<bool> Flush { get; set; }

		public bool IsVisible
		{
			get => gameObject.activeSelf;
			protected set => gameObject.SetActive(value);
		}

		protected float FlushLimit { get; private set; }

		protected virtual bool ShowFlush
		{
			get => Current > FlushLimit;
		}

		public virtual void Begin(float period)
		{
			m_currentStep = ConvertToStep(period);
			m_isPaused = false;
			IsVisible = true;
		}

		public void Finish(bool notify = false)
		{
			IsVisible = false;
			Current = 0;
			FlushLimit = m_flushLimits[m_currentFlushLimit.Next];

			if (!notify)
			{
				return;
			}

			Finished?.Invoke();
		}

		public void NextStep(float step)
		{
			if (IsFinished(step))
			{
				Finish(true);
				return;
			}

			IsVisible = true;
			UpdateProgress(step);
			Flush?.Invoke(ShowFlush);
		}

		public void Pause()
		{
			m_isPaused = true;
		}

		protected virtual bool IsFinished(float step)
		{
			return Current + step > FINISHED;
		}

		protected override void Start()
		{
			m_currentFlushLimit = new RoundRobin(m_flushLimits.Length);
			FlushLimit = m_flushLimits[m_currentFlushLimit.Current];
		}

		protected override void Updated()
		{
			if (m_isPaused)
			{
				return;
			}

			NextStep(m_currentStep);
		}

		protected virtual void UpdateProgress(float step)
		{
			Current += step;
		}

		private static float ConvertToStep(float period)
		{
			return Time.deltaTime / period;
		}
	}
}
