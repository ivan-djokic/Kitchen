// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Common;
using Kitchen.Management.Dispatchers;
using Kitchen.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Kitchen.Audio
{
	public class SoundManager : UnityObject
	{
		private const string AUDIO_NAME = "One shot audio";
		private const float BLEND_3D = 1;

		private readonly Dictionary<Sounds, AudioSource> m_playingSounds = new();
		[SerializeField]
		private SoundsCollection m_soundCollection;

		public static SoundManager Instance { get; private set; }

		public void Play(Sounds sound, Transform transform = null, bool inPause = false)
		{
			if (m_playingSounds.ContainsKey(sound))
			{
				return;
			}

			var audio = Play(m_soundCollection.GetAudio(sound), transform ?? Camera.main.transform);

			if (inPause)
			{
				return;
			}

			m_playingSounds[sound] = audio;
		}

		public void Stop(Sounds sound)
 		{
			if (!m_playingSounds.ContainsKey(sound))
			{
				return;
			}

			Destroy(m_playingSounds[sound]);
		}

		protected override void Awake()
		{
			Instance = this;
		}

		protected override void Update()
		{
			foreach (var sound in m_playingSounds.Keys.ToList())
			{
				if (m_playingSounds[sound].IsDestroyed())
				{
					m_playingSounds.Remove(sound);
					continue;
				}

				if (!Dispatcher.Instance.Paused)
				{
					continue;
				}

				AutoDestroyableAudio.Pause(m_playingSounds[sound]);
			}
		}

		private AudioSource Play(AudioClip clip, Transform transform)
		{
			var gameObject = new GameObject(AUDIO_NAME);
			gameObject.transform.position = transform.position;
			gameObject.AddComponent(typeof(AutoDestroyableAudio));

			var audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
			audio.clip = clip;
			audio.spatialBlend = BLEND_3D;
			audio.volume = Options.Instance.SoundVolume;
			audio.Play();

			return audio;
		}
	}
}
