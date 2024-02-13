// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using Kitchen.Utils;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class CountdownManager : UnityObject
	{
		private const float DELAY = 0.8f;

		private int m_currentValue;
		[SerializeField]
		private AudioSource m_music;
		[SerializeField]
		private TextMeshProUGUI m_title;
		[SerializeField]
		private string[] m_values;

		public Action<Action> Changed { get; set; }

		public bool Finished
		{
			get => gameObject.activeSelf;
		}

		protected override void Start()
		{
			SoundManager.Instance.Play(Sounds.CountdownStart);
			Invoke(DELAY, Countdown);
		}

		private void Countdown()
		{
			m_title.text = m_values[m_currentValue++];

			if (m_currentValue < m_values.Length)
			{
				Process(Countdown, Sounds.Countdown);
				return;
			}

			Process(Finish, Sounds.CountdownFinish);
		}

		private void Finish()
		{
			m_music.volume = Options.Instance.MusicVolume;
			m_music.Play();

			Game.Manager.Time = User.Instance.Level.Time;
			gameObject.SetActive(false);
			Game.Instance.Starting = false;
		}

		private void Process(Action action, Sounds sound)
		{
			Changed?.Invoke(() => Invoke(DELAY, action));
			SoundManager.Instance.Play(sound);
		}
	}
}
