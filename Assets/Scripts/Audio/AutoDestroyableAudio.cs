// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Kitchen.Audio
{
	public class AutoDestroyableAudio : UnityObject
	{
		private AudioSource m_audio;

		public static void Pause(AudioSource audio)
		{
			audio.Pause();
		}

		protected override void Start()
		{
			m_audio = transform.GetComponent<AudioSource>();
		}

		protected override void Updated()
		{
			if (Resume(m_audio))
			{
				return;
			}

			Destroy(transform.gameObject);
		}

		private static bool Resume(AudioSource audio)
		{
			if (audio.IsDestroyed())
			{
				return true;
			}

			audio.UnPause();
			return audio.isPlaying;
		}
	}
}
